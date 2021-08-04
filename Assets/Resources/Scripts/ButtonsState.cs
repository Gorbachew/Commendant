using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsState : MonoBehaviour
{
    [SerializeField] private RectTransform _Categories, _unitsType, _btnRollUp, _BuildingInfo;
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private Button[] _btnsBuildingsControl = new Button[3];
    [SerializeField] private Button[] _btnsBuildingInfo = new Button[1];
    [SerializeField] private ResourcesState _resourcesState;

    private RectTransform _openedCategory;
    private Unit _unit;
    private bool _btnsOpened;

    private void Awake()
    {
        _btnsBuildingsControl[0].onClick.AddListener(_buildingsGrid.PlaceFlyingBuilding);
        _btnsBuildingsControl[1].onClick.AddListener(_buildingsGrid.RotateFlyingBuilding);
        _btnsBuildingsControl[2].onClick.AddListener(_buildingsGrid.CancelFlyingBuilding);
    }
    private void Start()
    {
        _btnRollUp.gameObject.SetActive(false);
    }

    public void ChangeBtnsControll(bool state)
    {
        foreach (Button btn in _btnsBuildingsControl)
        {
            btn.gameObject.SetActive(state);
        }
    }

    public void OpenBuildingInfo(GameObject obj)
    {
        if (_btnsOpened)
        {
            RollUpCategory();
        }

        _openedCategory = _BuildingInfo;
        _btnsBuildingInfo[0].onClick.RemoveAllListeners();
        _btnsBuildingInfo[0].onClick.AddListener(() => DestroyBuilding(obj));
        StartCoroutine(Transformation(_openedCategory, new Vector2(0, 0)));

    }

    public void OpenUnitsType(Unit unit)
    {

        if (_btnsOpened)
        {
            RollUpCategory();
        }

        _unit = unit;
        _openedCategory = _unitsType;
        StartCoroutine(Transformation(_openedCategory, new Vector2(0, 0)));
    }

    public void OpenCategory(RectTransform category)
    {
        _openedCategory = category;
        StartCoroutine(Transformation(category, new Vector2(0, 0)));
    }

    public void RollUpCategory()
    {
        StartCoroutine(Transformation(_openedCategory, new Vector2(0, -300)));
        _openedCategory = null;
        if (_buildingsGrid._flyingBuilding)
        {
            _buildingsGrid.CancelFlyingBuilding();
        }
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        _buildingsGrid.StartPlacingBuilding(buildingPrefab);
    }

    public void ChangeUnitType(string type)
    {
        _unit.ChangeType(type);
        RollUpCategory();
    }

    IEnumerator Transformation(RectTransform category, Vector2 to)
    {
        if (!_btnsOpened || to.y == -300)
        {
            _btnRollUp.gameObject.SetActive(to.y == 0);
            while (category.offsetMin != to)
            {
                category.offsetMin = Vector3.MoveTowards(category.offsetMin, to, 6f);
                yield return null;
            }
            _Categories.gameObject.SetActive(to.y == -300);
            _btnsOpened = to.y == 0;
        } 
    }

    private void DestroyBuilding(GameObject obj)
    {
        Destroy(obj);
        RollUpCategory();
        StartCoroutine(WaitDestroy());
    }

    IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        _resourcesState.FindBuildings("all");
        _resourcesState.UpdateResouces("all");
    }
}
