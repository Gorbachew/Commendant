
public interface IBuilding
{
    void Using(IUnit unit);
    void Destroy();
    void Build(int count);
    void Damage(IUnit damager, int count);
    void NextDay();
}
