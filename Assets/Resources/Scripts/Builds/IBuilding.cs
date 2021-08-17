
using System.Collections.Generic;
using UnityEngine;

public interface IBuilding
{
    struct SBuildingUsing
    {
        public IUnit iunit;
        public string action;
        public int value;
        public bool start;
    }
    struct SBuildingReturndUsing
    {
        public Transform building;
        public int spm;
        public List<int> items;
    }

    SBuildingReturndUsing Using(SBuildingUsing sBuildingUsing);
    void Destroy();
    void NextDay();
}
