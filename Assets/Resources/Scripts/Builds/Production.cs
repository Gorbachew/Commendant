using UnityEngine;
using static IBuilding;

public class Production : MonoBehaviour, IBuilding
{
    public void Build(int count)
    {
    }

    public void Damage(IUnit damager, int count)
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
