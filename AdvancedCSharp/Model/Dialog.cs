using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model;

public class Dialog : IDialog
{
    public IDialogScreen FirstScreen { get; }

    public Dialog(IDialogScreen firstScreen)
    {
        FirstScreen = firstScreen;
    }
}