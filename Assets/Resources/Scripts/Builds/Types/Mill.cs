using System.Collections.Generic;
using UnityEngine;
using static IBuilding;

public class Mill : MonoBehaviour, IBuilding
{
    [SerializeField] private BuildingState _buildingState;
    private Animator _animator;
    private Building _building;

    private void Awake()
    {
        _building = GetComponent<Building>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        _building.RenderItems();
    }

    public SBuildingReturndUsing Using(SBuildingUsing sBuildingUsing)
    {
        SBuildingReturndUsing sEndUsing = new SBuildingReturndUsing();

        if (sBuildingUsing.start)
        {
            switch (sBuildingUsing.action)
            {
                case GlobalConstants.makeFlourAction:
                    _animator.SetBool("isWork", sBuildingUsing.start);
                    break;
            }
        }


        if (!sBuildingUsing.start)
        {
            switch (sBuildingUsing.action)
            {
                case GlobalConstants.makeFlourAction:
                    _animator.SetBool("isWork", sBuildingUsing.start);
                    _building.IncreaseProgress(GlobalConstants.flourBagValue);
                    _building.RenderItems();
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        spm = GlobalConstants.makeFlourSpm,
                    };
                    break;
                case GlobalConstants.takeFlourAction:
                    _buildingState.progress--;
                    _building.RenderItems();
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        items = new List<int>(new int[] {GlobalConstants.flourId}),
                        spm = GlobalConstants.makeFlourSpm,
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

        _buildingState.isBusy = sBuildingUsing.start;
        _buildingState.isProdStart = sBuildingUsing.start;
        _buildingState.isProdOver = !sBuildingUsing.start;

        return sEndUsing;
    }

    public void Build(int count){}

    public void Damage(IUnit damager, int count){}

    public void Destroy(){}

    public void NextDay(){}
}
