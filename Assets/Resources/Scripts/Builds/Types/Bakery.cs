using UnityEngine;
using static IBuilding;

public class Bakery : MonoBehaviour, IBuilding
{
    [SerializeField] private BuildingState _buildingState;
    [SerializeField] private ParticleSystem FireEffect, SmokeEffect;
    private Building _building;
    private void Awake()
    {
        _building = GetComponent<Building>();
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
                case GlobalConstants.makeBreadAction:
                    FireEffect.Play();
                    SmokeEffect.Play();
                    break;
            }
        }


        if (!sBuildingUsing.start)
        {
            switch (sBuildingUsing.action)
            {
                case GlobalConstants.makeBreadAction:
                    FireEffect.Stop();
                    SmokeEffect.Stop();
                    _building.IncreaseProgress(GlobalConstants.makeBreadValue);
                    _building.RenderItems();
                    sEndUsing = new SBuildingReturndUsing()
                    {
                        spm = GlobalConstants.makeBreadSpm,
                    };
                    break;
                case GlobalConstants.takeBreadAction:

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

    public void Build(int count) { }

    public void Damage(IUnit damager, int count) { }

    public void Destroy() { }

    public void NextDay() { }

    
}
