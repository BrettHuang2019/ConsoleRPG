using AdancedCSharp.Abstract;
using AdancedCSharp.Model;

namespace AdancedCSharp.States;

public class MainMenuState: IEngineState
{
    private readonly Player _player;

    public MainMenuState(Player player)
    {
        _player = player;
    }
    public void Dispose()
    {
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        if (key.Key == ConsoleKey.Z)
        {
            Engine.Instance.PopState(this);
        }else if (key.Key == ConsoleKey.Enter)
        {
            Engine.Instance.PushState(new InventoryState(_player));
        }
    }

    public void Activate()
    {
        Console.Clear();
        Console.WriteLine("Main Menu");
        Console.WriteLine("Press enter for inventory, press z to return to game.");
    }

    public void Deactivate()
    {
        
    }
}