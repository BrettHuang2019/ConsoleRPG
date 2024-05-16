using AdancedCSharp.Abstract;
using AdancedCSharp.Model;

namespace AdancedCSharp.States;

public class CombatState: IEngineState, ICombatListener
{
    private readonly Combat _combat;
    private int _selectedIndex;
    private bool _combatEnded;
    public CombatState(Combat combat)
    {
        _combat = combat;
        _combat.AddListener(this);
    }
    
    public void Dispose()
    {
        _combat.RemoveListener(this);
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        int abilityCount = _combat.Player.Abilities.Count();
        int inventoryCount = _combat.Player.Inventory.Count(i => i.CanUse);
        int totalItemCount = abilityCount + inventoryCount;

        if (key.Key == ConsoleKey.Z)
        {
            Engine.Instance.PopState(this);
            return;
        }
        else if (key.Key == ConsoleKey.W)
        {
            if (_selectedIndex > 0)
                _selectedIndex--;
        }else if (key.Key== ConsoleKey.S)
        {
            if (_selectedIndex < totalItemCount - 1)
                _selectedIndex++;
        }else if (key.Key == ConsoleKey.Spacebar)
        {
            if (_selectedIndex < abilityCount)
                _combat.UseAbility(_combat.Player.Abilities.ElementAt(_selectedIndex));
            else
                _combat.UseItem(_combat.Player.Inventory.Where(i => i.CanUse).ElementAt(_selectedIndex - abilityCount));
        }

        if (!_combatEnded)
            Render();
    }

    public void Activate()
    {
        Render();
    }

    public void Deactivate()
    {
    }

    public void DisplayMessage(string message)
    {
        RenderHeader();
        Console.WriteLine(message);
        Console.WriteLine("Press any key..");
        Console.ReadKey();
    }

    public void EndCombat()
    {
        _combatEnded = true;
        Engine.Instance.PopState(this);
    }

    public void PlayerDied()
    {
        Console.WriteLine("You Died.");
        Console.ReadKey();
        Engine.Instance.Quit();
    }

    private void Render()
    {
        RenderHeader();
        var index = 0;
        foreach (IAbility ability in _combat.Player.Abilities)
        {
            if (_selectedIndex == index)
                ColorConsole(true);
            Console.WriteLine(ability.Name);
            ColorConsole(false);
            index++;
        }
        Console.WriteLine("--------------------");
        foreach (IItem item in _combat.Player.Inventory.Where(i=>i.CanUse))
        {
            if (_selectedIndex == index)
                ColorConsole(true);
            Console.WriteLine(item.Name);
            ColorConsole(false);
            index++;
        }

        
    }

    private void RenderHeader()
    {
        Console.Clear();
        Console.WriteLine($"You (hp: {_combat.Player.HP}) vs {_combat.CombatEntity.Name} (hp: {_combat.CombatEntity.HP})");
        Console.WriteLine("--------------------");

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