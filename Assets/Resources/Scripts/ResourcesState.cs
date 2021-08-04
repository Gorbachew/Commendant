using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesState : MonoBehaviour
{
    [SerializeField] private Transform _buildingsFolder;
    [SerializeField] private Text _foodText, _woodText, _stoneText, _ironText, _toolsText, _WeaponText, _GoldText;
    [SerializeField] private int _foodCount, _woodCount, _stoneCount, _ironCount, _toolsCount, _WeaponCount, _GoldCount;
    private List<BuildingState> _drovnitsy = new List<BuildingState>();



    private void Start()
    {
        FindBuildings("all");
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
                break;
        }
    }

    public void FindBuildings(string who)
    {

        switch (who)
        {
            case "food":
                break;
            case "wood":
                FindWoodBuildings();
                break;
            case "stone":
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
                FindWoodBuildings();
                break;
        }


    }

    private void UpdateWood ()
    {
        _woodCount = 0;
        foreach (BuildingState d in _drovnitsy)
        {
            _woodCount += d.items.Count;
        }
        string maxItems = (_drovnitsy.Count * GlobalConstants.drownitsaMaxItems).ToString();

        _woodText.text = _woodCount.ToString() + "/" + maxItems;
    }


    

    private void FindWoodBuildings()
    {
        Drovnitsa[] dArr = _buildingsFolder.GetComponentsInChildren<Drovnitsa>();
        _drovnitsy.Clear();
        foreach (Drovnitsa d in dArr)
        {
            _drovnitsy.Add(d.GetComponent<BuildingState>());
        }
    }

}
