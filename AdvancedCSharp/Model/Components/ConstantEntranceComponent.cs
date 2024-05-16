using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model.Components;

public class ConstantEntranceComponent : Component, IEntityEntranceComponent
{
    private bool _canEnter;

    public ConstantEntranceComponent(bool canEnter)
    {
        _canEnter = canEnter;
    }
    public bool CanEnter(Entity entity)
    {
        return _canEnter;
    }

    public void Enter(Entity entity)
    {
        throw new NotImplementedException();
    }
}