
using System.Collections.Generic;

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
        public int spm;
        public List<int> items;
    }

    SBuildingReturndUsing Using(SBuildingUsing sBuildingUsing);
    void Destroy();
    void Damage(IUnit damager, int count);
    void NextDay();
}
