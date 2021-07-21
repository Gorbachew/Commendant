using System.Collections;
using UnityEngine;

public class ButtonsState : MonoBehaviour
{
    [SerializeField] private RectTransform _btnsCategories, _btnRollUp;
    [SerializeField] private BuildingsGrid _buildingsGrid;

    private RectTransform _openedCategory;
    private IUnit _unit;
    private bool _btnsOpened;

    private void Start()
    {
        _btnRollUp.gameObject.SetActive(false);
    }

    public void OpenCategory(RectTransform category)
    {
        _openedCategory = category;
        StartCoroutine(Transformation(category, new Vector2(0, 0)));
    }

    public void OpenCategory(RectTransform category, IUnit unit)
    {
        _unit = unit;
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
    }

    IEnumerator Transformation(RectTransform category, Vector2 to)
    {
        if (!_btnsOpened || to.y == -300)
        {
            while (category.offsetMin != to)
            {
                category.offsetMin = Vector3.MoveTowards(category.offsetMin, to, 6f);
                yield return null;
            }
            _btnRollUp.gameObject.SetActive(to.y == 0);
            _btnsCategories.gameObject.SetActive(to.y == -300);
            _btnsOpened = to.y == 0;
        }
       
    }
}
