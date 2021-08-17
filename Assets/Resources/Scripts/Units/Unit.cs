using UnityEngine;
using static MainData;

public class Unit : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    private UnitState _unitState;

    private void Awake()
    {
        _unitState = GetComponent<UnitState>();
    }

    private void Start()
    {
        name = _unitState.type;
    }

    public void ChangeType(string type)
    {
        Destroy(_unitState.model);
        GameObject newModel = Instantiate(Resources.Load("Prefabs/Units/Models/" + type), transform) as GameObject;
        _unitState.model = newModel;
        _unitState.type = type;
        if (_playerController)
        {
            _playerController.UpdateModel(newModel);
        }
            
    }

    public void Enrichment(SUnit unit)
    {
        _unitState.id = unit.id;
        _unitState.hp = unit.hp;
        _unitState.type = unit.type;
        ChangeType(unit.type);
        _unitState.level = unit.level;
        _unitState.transform.position = new Vector3(
                unit.pos.x,
                unit.pos.y,
                unit.pos.z
            );
        _unitState.model.transform.localPosition = new Vector3(
                unit.posObj.x,
                unit.posObj.y,
                unit.posObj.z
            );

        _unitState.model.transform.localRotation = Quaternion.Euler(
                unit.rotObj.x,
                unit.rotObj.y,
                unit.rotObj.z
            );
    }
}
