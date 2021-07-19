
using static MainData;

[System.Serializable]
public class UnitsData
{
    public Unit[] _units;

    public UnitsData() { }

    public UnitsData(UnitState[] units)
    {

        _units = new Unit[units.Length];

        for (int i = 0; i < units.Length; i++)
        {

            _units[i]._id = units[i].id;
            _units[i]._hp = units[i].hp;
            _units[i]._type = units[i].type;
            _units[i]._level = units[i].level;

            _units[i]._pos =  new Vec3 (
                units[i].transform.position.x,
                units[i].transform.position.y,
                units[i].transform.position.z
                );
            _units[i]._posObj = new Vec3(
                units[i].obj.transform.localPosition.x,
                units[i].obj.transform.localPosition.y,
                units[i].obj.transform.localPosition.z
                );
            _units[i]._rotObj = new Vec3(
                units[i].obj.transform.eulerAngles.x,
                units[i].obj.transform.eulerAngles.y,
                units[i].obj.transform.eulerAngles.z
                );

        }

    }
}
