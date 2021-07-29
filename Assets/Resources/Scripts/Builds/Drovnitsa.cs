
using System.Collections.Generic;
using UnityEngine;

public class Drovnitsa : MonoBehaviour, IBuilding
{
    [SerializeField] private GameObject[] _items;
    [SerializeField] private BuildingState _buildingState;

    public void ChangeItems()
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
        ChangeItems();
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
}
