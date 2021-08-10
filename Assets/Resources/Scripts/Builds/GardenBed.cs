using UnityEngine;

public class GardenBed : MonoBehaviour, IBuilding
{
    [SerializeField] private BuildingState _buildingState;
    [SerializeField] private Transform _hayField;

    private void Start()
    {
        CheckGrownState();
    }

    public void SownSeed()
    {
        _buildingState.isProdStart = true;
    }

    public void Build(int count)
    {
        throw new System.NotImplementedException();
    }

    public void Damage(IUnit damager, int count)
    {
        throw new System.NotImplementedException();
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void NextDay()
    {
        IncreaseProgress();
        CheckGrownState();
    }

    private void IncreaseProgress()
    {
        if (_buildingState.isProdStart && _buildingState.progress < _buildingState.maxProgress)
        {
            _buildingState.progress++;
            if (_buildingState.progress == _buildingState.maxProgress)
            {
                _buildingState.isProdOver = true;
            }
        } else if (_buildingState.progress >= _buildingState.maxProgress)
        {
            _buildingState.progress = _buildingState.maxProgress;
        }
    }

    private void CheckGrownState()
    {
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

    public void Using(IUnit unit)
    {
       
    }
}
