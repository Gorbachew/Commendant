
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
            _builds[i].name = OperationsHelper.CloneClearing(build[i].gameObject.name);
            _builds[i].id = build[i].id;
            _builds[i].hp = build[i].hp;
            _builds[i].level = build[i].level;
            _builds[i].items = build[i].items.ToArray();
            _builds[i].progress = build[i].progress;

            _builds[i].isBouild = build[i].isBuild;
            _builds[i].isWork = build[i].isWork;
            _builds[i].isReady = build[i].isReady;
            _builds[i].isProdStart = build[i].isProdStart;
            _builds[i].isProdOver = build[i].isProdOver;

            _builds[i].pos = new SVec3(
                build[i].transform.position.x,
                build[i].transform.position.y,
                build[i].transform.position.z
                );
            _builds[i].rot = new SVec3(
                build[i].transform.eulerAngles.x,
                build[i].transform.eulerAngles.y,
                build[i].transform.eulerAngles.z
                );
        }

    }
}
