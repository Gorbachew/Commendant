using System;
using UnityEngine;

[Serializable]
public class MainData
{
    [Serializable]
    public struct Unit
    {
        public int _id, _level, _hp;
        public string _type;
        public Vec3 _pos, _posObj, _rotObj;
    }

    [Serializable]
    public struct Build
    {
        public int _id, _level, _hp, _items;
        public string _type; 
        public Vec3 _pos, _posObj, _rotObj;
    }

    [Serializable]
    public struct Vec3
    {
        public float _x;
        public float _y;
        public float _z;

        public Vec3(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
    }

    public Unit[] _units;

    public Build[] _builds;


    public MainData(BuildingsData bd, UnitsData ud)
    {
        _units = ud._units;
        _builds = bd._builds;
    }
}
