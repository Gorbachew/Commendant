using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsState : MonoBehaviour
{
    
    [SerializeField] private BuildingsGrid _buildingsGrid;
    [SerializeField] private Button[] _btnsBuildingsControl = new Button[3];
    [SerializeField] private Button[] _btnsBuildingInfo = new Button[1];
    [SerializeField] private ResourcesState _resourcesState;

    private RectTransform _categories, _unitsTypes, _buildingInfo;
    
    private RectTransform _btnRollUp;
    private RectTransform _openedCategory, _openedBuildingsTypes;
    private Unit _unit;
    private bool _isBtnsOpened, _isBuildingsTypeOpened;

    private void Awake()
    {
        _categories = transform.Find("Categories").GetComponent<RectTransform>();
        _unitsTypes = transform.Find("Units").GetComponent<RectTransform>();
        _btnRollUp = transform.Find("RollUp").GetComponent<RectTransform>();
        _buildingInfo = transform.Find("BuildingInfo").GetComponent<RectTransform>();

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

    public void RollUpCategory()
    {
        StartCoroutine(Transformation(_openedCategory, new Vector2(0, -300)));
        _openedCategory = null;
        if (_buildingsGrid._flyingBuilding)
        {
            _buildingsGrid.CancelFlyingBuilding();
        }
        if (_openedBuildingsTypes)
        {
            CloseBuildingTypes();
        }
    }

    public void StartPlacingBuilding(Building buildingPrefab)
    {
        _buildingsGrid.StartPlacingBuilding(buildingPrefab);
        CloseBuildingTypes();
    }

    public void ChangeUnitType(string type)
    {
        _unit.ChangeType(type);
        RollUpCategory();
    }

    public void OpenCategory(RectTransform transform)
    {
        _openedCategory = transform;
        StartCoroutine(Transformation(_openedCategory, new Vector2(0, 0)));
    }

    public void OpenBuildingInfo(GameObject obj)
    {
        if (_isBtnsOpened)
        {
            RollUpCategory();
        }

        _openedCategory = _buildingInfo;
        _btnsBuildingInfo[0].onClick.RemoveAllListeners();
        _btnsBuildingInfo[0].onClick.AddListener(() => DestroyBuilding(obj));
        StartCoroutine(Transformation(_openedCategory, new Vector2(0, 0)));

    }

    public void OpenUnitsType(Unit unit)
    {

        if (_isBtnsOpened)
        {
            RollUpCategory();
        }

        _unit = unit;
        _openedCategory = _unitsTypes;
        StartCoroutine(Transformation(_openedCategory, new Vector2(0, 0)));
    }

    public void SwitchBuildingTypes(RectTransform transform)
    {
        if (!_isBuildingsTypeOpened)
        {
            _isBuildingsTypeOpened = true;
            _openedBuildingsTypes = transform;
            StartCoroutine(TransformationBuildings(transform, new Vector2(0, 0)));
        } else
        {
            CloseBuildingTypes();
        }
    }

    private void CloseBuildingTypes()
    {
        _isBuildingsTypeOpened = false;
        StartCoroutine(TransformationBuildings(_openedBuildingsTypes, new Vector2(0, -1000)));
    }


    private void DestroyBuilding(GameObject obj)
    {
        obj.GetComponent<IBuilding>().Destroy();
        Destroy(obj);
        RollUpCategory();
        StartCoroutine(WaitDestroy());
    }

    private IEnumerator WaitDestroy()
    {
        yield return new WaitForSeconds(0.5f);
        _resourcesState.FindStocksBuildings();
        _resourcesState.UpdateResouces("all");
    }

    private IEnumerator Transformation(RectTransform category, Vector2 to)
    {
        if (!_isBtnsOpened || to.y == -300)
        {
            _btnRollUp.gameObject.SetActive(to.y == 0);
            while (category.offsetMin != to)
            {
                category.offsetMin = Vector3.MoveTowards(category.offsetMin, to, 25f);
                yield return null;
            }
            _categories.gameObject.SetActive(to.y == -300);
            _isBtnsOpened = to.y == 0;
        }
    }

    private IEnumerator TransformationBuildings(RectTransform category, Vector2 to)
    {
        while (category.offsetMin != to)
        {
            category.offsetMin = Vector3.MoveTowards(category.offsetMin, to, 40f);
            yield return null;
        } 
    }
}
