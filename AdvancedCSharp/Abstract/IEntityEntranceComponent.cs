using AdancedCSharp.Model;

namespace AdancedCSharp.Abstract;

public interface IEntityEntranceComponent: IComponent
{
    bool CanEnter(Entity entity);
    void Enter(Entity entity);
}