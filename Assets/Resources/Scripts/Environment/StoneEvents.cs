using UnityEngine;
using static IBuilding;

public class StoneEvents : MonoBehaviour, IBuilding
{
    private ParticleSystem _particleSystem;
    private BuildingState _buildingState;

    private void Awake()
    {
        _buildingState = GetComponent<BuildingState>();
        _buildingState.name = GlobalConstants.stone;
        _particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void Damage(IUnit damager, int count)
    {
        _particleSystem.Play();
    }

    public void Build(int count)
    {
        
    }

    public void Destroy()
    {

    }

    public void NextDay()
    {
    }

    public SBuildingReturndUsing Using(SBuildingUsing sStartUsing)
    {

        return new SBuildingReturndUsing() { };
    }

}
