
using static MainData;

[System.Serializable]
public class UnitsData
{
    public SUnit[] _units;

    public UnitsData() { }

    public UnitsData(UnitState[] units)
    {

        _units = new SUnit[units.Length];

        for (int i = 0; i < units.Length; i++)
        {

            _units[i].id = units[i].id;
            _units[i].hp = units[i].hp;
            _units[i].type = units[i].type;
            _units[i].level = units[i].level;

            _units[i].pos =  new SVec3 (
                units[i].transform.position.x,
                units[i].transform.position.y,
                units[i].transform.position.z
                );
            _units[i].posObj = new SVec3(
                units[i].model.transform.localPosition.x,
                units[i].model.transform.localPosition.y,
                units[i].model.transform.localPosition.z
                );
            _units[i].rotObj = new SVec3(
                units[i].model.transform.eulerAngles.x,
                units[i].model.transform.eulerAngles.y,
                units[i].model.transform.eulerAngles.z
                );

        }

    }
}
