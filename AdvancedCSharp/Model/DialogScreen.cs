using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model;

public class DialogScreen : IDialogScreen
{
    private Action<Entity> _enterAction;
    
    public bool FinalScreen => NextScreens.Count == 0;
    public string Text { get; }
    public Dictionary<string, IDialogScreen> NextScreens { get; }

    public DialogScreen(string text, Action<Entity> enterAction = null, Dictionary<string, IDialogScreen> nextScreens = null)
    {
        Text = text;
        _enterAction = enterAction;
        NextScreens = nextScreens ?? new Dictionary<string, IDialogScreen>();
    }
    
    public void EnterScreen(Entity entity)
    {
        if (_enterAction!=null)
        {
            _enterAction(entity);
        }
    }
}