using System;

[Serializable]
public class MainData
{
    [Serializable]
    public struct SUnit
    {
        public int id, level, hp;
        public string type;
        public SVec3 pos, posObj, rotObj;
    }

    [Serializable]
    public struct SBuild
    {
        public int id, level, hp;
        public string name;
        public int[] items;
        public bool isBouild, isWork, isReady;
        public SVec3 pos, rot;
    }

    [Serializable]
    public struct SVec3
    {
        public float x;
        public float y;
        public float z;

        public SVec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public SUnit[] _units;

    public SBuild[] _builds;


    public MainData(BuildingsData bd, UnitsData ud)
    {
        _units = ud._units;
        _builds = bd._builds;
    }
}
