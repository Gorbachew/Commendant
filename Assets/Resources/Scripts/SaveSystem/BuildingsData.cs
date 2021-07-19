
using static MainData;

[System.Serializable]
public class BuildingsData
{
    public Build[] _builds;

    public BuildingsData() { }

    public BuildingsData(BuildingState[] build)
    {

        _builds = new Build[build.Length];

        for (int i = 0; i < _builds.Length; i++)
        {

            _builds[i]._id = build[i].id;
            _builds[i]._hp = build[i].hp;
            _builds[i]._type = build[i].type;
            _builds[i]._level = build[i].level;
            _builds[i]._items = build[i].items;

            _builds[i]._pos = new Vec3(
                build[i].transform.position.x,
                build[i].transform.position.y,
                build[i].transform.position.z
                );
            _builds[i]._posObj = new Vec3(
                build[i].obj.transform.localPosition.x,
                build[i].obj.transform.localPosition.y,
                build[i].obj.transform.localPosition.z
                );
            _builds[i]._rotObj = new Vec3(
                build[i].obj.transform.eulerAngles.x,
                build[i].obj.transform.eulerAngles.y,
                build[i].obj.transform.eulerAngles.z
                );

        }

    }
}
