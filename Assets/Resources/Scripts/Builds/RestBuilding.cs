using UnityEngine;

public class RestBuilding : MonoBehaviour
{
    private IUnit _iunit;

    public void BusyBuilding (IUnit iunit)
    {
        _iunit = iunit;
    }

    private void OnDestroy()
    {
        if (_iunit != null)
        {
            _iunit.CalculateLogic();
        }
    }
}
