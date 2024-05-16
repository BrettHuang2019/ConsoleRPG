using AdancedCSharp.Abstract;
using AdancedCSharp.States;

namespace AdancedCSharp.Model.Components;

public class DialogComponent: Component, IEntityEntranceComponent
{
    private readonly IDialog _dialog;

    public DialogComponent(IDialog dialog)
    {
        _dialog = dialog;
    }
    public bool CanEnter(Entity entity)
    {
        return true;
    }

    public void Enter(Entity entity)
    {
        Engine.Instance.PushState(new DialogState(entity, _dialog));
    }
}