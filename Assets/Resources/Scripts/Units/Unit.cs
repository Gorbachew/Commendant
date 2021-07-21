using UnityEngine;
using static MainData;

public class Unit : MonoBehaviour, IUnit
{
    [SerializeField] PlayerController _playerController;
    private UnitState _unitState;

    private void Awake()
    {
        _unitState = GetComponent<UnitState>();
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
        _unitState.id = unit._id;
        _unitState.hp = unit._hp;
        _unitState.type = unit._type;
        ChangeType(unit._type);
        _unitState.level = unit._level;
        _unitState.transform.position = new Vector3(
                unit._pos._x,
                unit._pos._y,
                unit._pos._z
            );
        _unitState.model.transform.localPosition = new Vector3(
                unit._posObj._x,
                unit._posObj._y,
                unit._posObj._z
            );

        _unitState.model.transform.localRotation = Quaternion.Euler(
                unit._rotObj._x,
                unit._rotObj._y,
                unit._rotObj._z
            );
    }
}
