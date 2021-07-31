
public interface IBuilding
{
    void RenderItems();

    void AddItems(int id, int count);

    void Damage(IUnit damager, int count);
}
