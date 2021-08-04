
using System.Collections.Generic;
using UnityEngine;

public class Drovnitsa : MonoBehaviour, IBuilding
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private BuildingState _buildingState;
    [SerializeField] private ResourcesState _resourcesState;


    private void Awake()
    {
        if (_buildingState.resources != null)
        {
            _resourcesState = GameObject.Find("Canvas/Resources").GetComponent<ResourcesState>();
        }
    }

    public void RenderItems()
    {
        int filteredItems = CountFilteredItems();
        for (int i = 0; i < _items.Length; i++)
        {
            _items[i].SetActive(i < filteredItems);
        }
    }

    public void AddItems(int id, int count)
    {
       for (int i = 0; i < count; i++)
        {
            if (_buildingState.items.Count < GlobalConstants.drownitsaMaxItems)
            {
                _buildingState.items.Add(id);
            } else if (_buildingState.items.Count > GlobalConstants.drownitsaMaxItems)
            {
                _buildingState.items.RemoveRange(
                    GlobalConstants.drownitsaMaxItems, 
                    _buildingState.items.Count - GlobalConstants.drownitsaMaxItems);
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
            if (item == GlobalConstants.woodId)
            {
                filteredList.Add(GlobalConstants.woodId);
            }
        }
        return filteredList.Count;
    }

    public void Damage(IUnit damager, int count){}
    
}
