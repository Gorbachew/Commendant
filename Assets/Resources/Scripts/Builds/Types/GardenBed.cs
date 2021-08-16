using System.Collections.Generic;
using UnityEngine;
using static IBuilding;

public class GardenBed : MonoBehaviour, IBuilding
{
    [SerializeField] private BuildingState _buildingState;
    [SerializeField] private Transform _hayField;
    [SerializeField] private GameObject _Saplings;
    private Building _building;

    private void Awake()
    {
        _building = GetComponent<Building>();
    }

    private void Start()
    {
        CheckProgressState();
    }
    public SBuildingReturndUsing Using(SBuildingUsing sBuildingUsing)
    {
        SBuildingReturndUsing sEndUsing = new SBuildingReturndUsing();
        _buildingState.isBusy = sBuildingUsing.start;
        if (!sBuildingUsing.start)
        {
            switch (sBuildingUsing.action)
            {
                case GlobalConstants.plantSeedsAction:
                    _buildingState.isProdStart = true;
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        spm = GlobalConstants.plantSeedSpm,
                    };
                    CheckProgressState();
                    break;
                case GlobalConstants.harwestAction:
                    _buildingState.progress--;

                    if (_buildingState.progress <= 0)
                    {
                        _buildingState.isProdStart = false;
                        _buildingState.isProdOver = false;
                    }
                    CheckProgressState();
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        items = new List<int>(new int[] { _buildingState.items[0] }),
                        spm = GlobalConstants.plantSeedSpm,
                    };
                    break;
                case GlobalConstants.buildAction:
                    _building.Build(sBuildingUsing.value);
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        spm = GlobalConstants.buildSpm,
                    };
                    break;
            }
        }
        
        return sEndUsing;
    }

    public void Damage(IUnit damager, int count)
    {

    }

    public void Destroy()
    {

    }

    public void NextDay()
    {
        _building.IncreaseProgress(GlobalConstants.milletHayValue);
        CheckProgressState();
    }

    private void CheckProgressState()
    {
        _Saplings.SetActive(_buildingState.isProdStart);
        _hayField.transform.rotation = Quaternion.Euler(0, 90, 0);
        switch (_buildingState.progress)
        {
            case 0:
                _hayField.transform.localPosition = new Vector3(0.5f, -0.5f, 0.5f);
                break;
            case 1:
                _hayField.transform.localPosition = new Vector3(0.5f, -0.2f, 0.5f);
                break;
            case 2:
                _hayField.transform.localPosition = new Vector3(0.5f, 0f, 0.5f);
                break;
            case 3:
                _hayField.transform.localPosition = new Vector3(0.5f, 0.15f, 0.5f);
                break;
        }
    }

    
}
