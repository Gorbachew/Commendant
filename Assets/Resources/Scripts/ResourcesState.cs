using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesState : MonoBehaviour
{
    [SerializeField] private Transform _buildingsFolder;
    [SerializeField] private Text _foodText, _woodText, _stoneText, _ironText, _toolsText, _WeaponText, _GoldText;
    [SerializeField] private int _foodCount, _woodCount, _stoneCount, _ironCount, _toolsCount, _WeaponCount, _GoldCount;
    [SerializeField] private List<BuildingState> _drovnitsy = new List<BuildingState>();
    [SerializeField] private List<BuildingState> _stoneStocks = new List<BuildingState>();


    private void Start()
    {
        FindStocksBuildings();
        UpdateResouces("all");
    }
    
    public void UpdateResouces(string who)
    {
        switch (who)
        {
            case "food":
                break;
            case "wood":
                UpdateWood();
                break;
            case "stone":
                UpdateStone();
                break;
            case "iron":
                break;
            case "tools":
                break;
            case "weapon":
                break;
            case "gold":
                break;
            case "all":
                UpdateWood();
                UpdateStone();
                break;
        }
    }

    public void FindStocksBuildings()
    {
        BuildingState[] sArr = _buildingsFolder.GetComponentsInChildren<BuildingState>();
        _drovnitsy.Clear();
        _stoneStocks.Clear();
        foreach (BuildingState s in sArr)
        {

            switch (s.resources)
            {
                case "wood":
                    _drovnitsy.Add(s);
                    break;
                case "stone":
                    _stoneStocks.Add(s);
                    break;
            }
        }
    }

    private void UpdateWood ()
    {
        _woodCount = 0;
        foreach (BuildingState item in _drovnitsy)
        {
            _woodCount += item.items.Count;
        }
        string maxItems = (_drovnitsy.Count * GlobalConstants.drownitsaMaxItems).ToString();

        _woodText.text = _woodCount.ToString() + "/" + maxItems;
    }

    private void UpdateStone()
    {
        _stoneCount = 0;
        foreach (BuildingState item in _stoneStocks)
        {
            _stoneCount += item.items.Count;
        }
        string maxItems = (_stoneStocks.Count * GlobalConstants.stoneStockMaxItems).ToString();

        _stoneText.text = _stoneCount.ToString() + "/" + maxItems;
    }







}
