using AdancedCSharp.Abstract;
using AdancedCSharp.States;

namespace AdancedCSharp.Model.Components;

public class CombatComponent : Component, IEntityEntranceComponent
{
    private readonly Func<Combat> _combatFactory;

    public CombatComponent(Func<Combat> combatFactory)
    {
        _combatFactory = combatFactory;
    }
    public bool CanEnter(Entity entity)
    {
        return true;
    }

    public void Enter(Entity entity)
    {
        Engine.Instance.PushState(new CombatState(_combatFactory()));
    }
}