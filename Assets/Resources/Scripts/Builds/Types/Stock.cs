using UnityEngine;
using static IBuilding;

public class Stock : MonoBehaviour, IBuilding
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
        
        _resourcesState = GameObject.Find("Canvas/Resources").GetComponent<ResourcesState>();
        switch (_buildingState.name)
        {
            case GlobalConstants.drovnitsa + "(Clone)":
                _buildingState.name = GlobalConstants.drovnitsa;
                _itemId = GlobalConstants.woodId;
                _buildingState.resourceId = GlobalConstants.woodId;
                break;
            case GlobalConstants.stoneStock + "(Clone)":
                _buildingState.name = GlobalConstants.stoneStock;
                _itemId = GlobalConstants.stoneId;
                _buildingState.resourceId = GlobalConstants.stoneId;
                break;
        }
    }

    private void Start()
    {
        _building.RenderItems();
        _buildingState.isProdStart = true;
        
    }

    public SBuildingReturndUsing Using(SBuildingUsing sBuildingUsing)
    {
        SBuildingReturndUsing sEndUsing = new SBuildingReturndUsing
        {
            building = transform
        };

        if (!sBuildingUsing.start)
        {
            switch (sBuildingUsing.action)
            {
                case GlobalConstants.putWoodAction:
                    _building.IncreaseProgress(GlobalConstants.putWoodValue);
                    _building.RenderItems();
                    _resourcesState.UpdateResouces(GlobalConstants.woodId);
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        spm = GlobalConstants.putWoodSpm,
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

    public void Destroy()
    {
    }

    public void Build(int count)
    {
    }

    public void NextDay()
    {

    }

  
}
