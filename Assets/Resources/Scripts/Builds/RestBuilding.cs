using UnityEngine;

public class RestBuilding : MonoBehaviour, IBuilding
{
    private IUnit _iunit;

  

    public void BusyBuilding (IUnit iunit)
    {
        _iunit = iunit;
    }

    public void Damage(IUnit damager, int count)
    {
    }


    public void Build(int count)
    {
    }

    public void Destroy()
    {
        if (_iunit != null)
        {
            _iunit.CalculateLogic();
        }
    }

    public void NextDay()
    {

    }

    public void Using(IUnit unit)
    {

    }
}
