
using static MainData;

[System.Serializable]
public class BuildingsData
{
    public SBuild[] _builds;

    public BuildingsData() { }

    public BuildingsData(BuildingState[] build)
    {

        _builds = new SBuild[build.Length];

        for (int i = 0; i < _builds.Length; i++)
        {

            _builds[i]._id = build[i].id;
            _builds[i]._hp = build[i].hp;
            _builds[i]._type = build[i].type;
            _builds[i]._level = build[i].level;
            _builds[i]._items = build[i].items.ToArray();

            _builds[i]._isBouild = build[i].isBuild;
            _builds[i]._isWork = build[i].isWork;

            _builds[i]._pos = new SVec3(
                build[i].transform.position.x,
                build[i].transform.position.y,
                build[i].transform.position.z
                );
            _builds[i]._rot = new SVec3(
                build[i].transform.eulerAngles.x,
                build[i].transform.eulerAngles.y,
                build[i].transform.eulerAngles.z
                );
        }

    }
}
