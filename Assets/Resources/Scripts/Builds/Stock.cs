using System.Collections.Generic;
using UnityEngine;

public class Stock : MonoBehaviour
{
    [SerializeField] private GameObject[] _items;
    private Building _building;
    private BuildingState _buildingState;
    private ResourcesState _resourcesState;

    private int _maxItems, _itemId;

    private void Awake()
    {
        _buildingState = GetComponent<BuildingState>();
        _building = GetComponent<Building>();
        switch (_buildingState.resources) 
        {
            case "wood":
                _maxItems = GlobalConstants.drownitsaMaxItems;
                _itemId = GlobalConstants.woodId;
                break;
            case "stone":
                _maxItems = GlobalConstants.stoneStockMaxItems;
                _itemId = GlobalConstants.stoneId;
                break;
        }

        if (_buildingState.resources != null)
        {
            _resourcesState = GameObject.Find("Canvas/Resources").GetComponent<ResourcesState>();
        }
    }

    private void Start()
    {
        RenderItems();
    }

    public void RenderItems()
    {
        if (gameObject)
        {
            int filteredItems = CountFilteredItems();
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i].SetActive(i < filteredItems);
            }
        }
        
    }

    public void AddItems(int id, int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_buildingState.items.Count < _maxItems)
            {
                _buildingState.items.Add(id);
            }
            else if (_buildingState.items.Count > _maxItems)
            {
                _buildingState.items.RemoveRange(
                    _maxItems,
                    _buildingState.items.Count - _maxItems);
                break;
            }

        }
        RenderItems();
        _resourcesState.UpdateResouces(_buildingState.resources);

    }

    private int CountFilteredItems()
    {
        List<int> filteredList = new List<int>();
        foreach (int item in _buildingState.items)
        {
            if (item == _itemId)
            {
                filteredList.Add(_itemId);
            }
        }
        return filteredList.Count;
    }

    
}
