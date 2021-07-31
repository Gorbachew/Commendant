using System;

[Serializable]
public class MainData
{
    [Serializable]
    public struct SUnit
    {
        public int _id, _level, _hp;
        public string _type;
        public SVec3 _pos, _posObj, _rotObj;
    }

    [Serializable]
    public struct SBuild
    {
        public int _id, _level, _hp;
        public int[] _items;
        public bool _isBouild, _isWork;
        public string _type; 
        public SVec3 _pos, _rot;
    }

    [Serializable]
    public struct SVec3
    {
        public float _x;
        public float _y;
        public float _z;

        public SVec3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
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
