using AdancedCSharp.Abstract;
using AdancedCSharp.Model;

namespace AdancedCSharp.States;

public class DialogState: IEngineState
{
    private readonly IDialog _dialog;
    private readonly Entity _instigator;
    private int _dialogHeight;
    private IDialogScreen _currentScreen;
    private int _selectedOption = 0;
    private List<(string, IDialogScreen)> _optionList;

    public DialogState(Entity instigator, IDialog dialog)
    {
        _dialog = dialog;
        _instigator = instigator;
        SwitchScreen(dialog.FirstScreen);
    }
    public void Dispose()
    {
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        if (_optionList.Count != 0)
        {
            ColorConsole(false);
            Console.SetCursorPosition(0, _dialogHeight + _selectedOption);
            Console.WriteLine(_optionList[_selectedOption].Item1);
        }

        switch (key.Key)
        {
            case ConsoleKey.W:
            {
                if (_selectedOption > 0)
                    _selectedOption--;
                break;
            }
            case ConsoleKey.S:
            {
                if (_selectedOption < _optionList.Count - 1)
                    _selectedOption++;
                break;
            }
            case ConsoleKey.Enter:
            {
                if (_currentScreen.FinalScreen)
                    Engine.Instance.PopState(this);
                else
                {
                    IDialogScreen nextScreen = _optionList[_selectedOption].Item2;
                    SwitchScreen(nextScreen);
                    RenderScreen();
                }

                break;
            }
        }

        if (_optionList.Count!=0)
        {
            ColorConsole(true);
            Console.SetCursorPosition(0, _dialogHeight + _selectedOption);
            Console.WriteLine(_optionList[_selectedOption].Item1);
        }

    }

    public void Activate()
    {
        RenderScreen();
    }

    private void RenderScreen()
    {
        Console.Clear();
        Console.WriteLine("Dialog");
        Console.WriteLine("-------------------");
        Console.WriteLine(_currentScreen.Text);
        Console.WriteLine("-------------------");

        _dialogHeight = Console.CursorTop;

        int index = 0;
        foreach (var kv in _optionList)
        {
            if (index == 0)
                ColorConsole(true);
            Console.WriteLine(kv.Item1);
            if (index++ ==0)
                ColorConsole(false);
        }

        if (_currentScreen.FinalScreen)
        {
            ColorConsole(true);
            Console.WriteLine("Exit Dialog");
            ColorConsole(false);
        }
    }

    public void Deactivate()
    {
    }

    private void SwitchScreen(IDialogScreen screen)
    {
        _currentScreen = screen;
        _optionList = _currentScreen.NextScreens.Select(kv => (kv.Key, kv.Value)).ToList();
        _selectedOption = 0;
        
        _currentScreen.EnterScreen(_instigator);
    }

    private void ColorConsole(bool selected)
    {
        if (selected)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black; 
        }
    }
    
}