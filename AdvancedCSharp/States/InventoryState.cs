using AdancedCSharp.Abstract;
using AdancedCSharp.Model;

namespace AdancedCSharp.States;

public class InventoryState : IEngineState
{
    private Player _player;
    private int _selectedIndex;

    public InventoryState(Player player)
    {
        _player = player;
    }
    public void Dispose()
    {
        
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        int equippedCount = _player.EquippedItems.Count();
        int inventoryCount = _player.Inventory.Count();
        int totalItemCount = equippedCount + inventoryCount;

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
            if (_selectedIndex<equippedCount)
            {
                var itemToUnequip = _player.EquippedItems.ElementAt(_selectedIndex);
                _player.UnequipItem(itemToUnequip);
            }
            else
            {
                var itemToEquip = _player.Inventory.ElementAt(_selectedIndex - equippedCount);
                _player.EquipItem(itemToEquip);
            }
        }
        Render();
    }

    public void Activate()
    {
        Render();
    }

    public void Deactivate()
    {
    }

    private void Render()
    {
        Console.Clear();
        Console.WriteLine("Player inventory");
        Console.WriteLine("-----------------");

        int itemIndex = 0;
        foreach (IItem equippedItem in _player.EquippedItems)
        {
            if (itemIndex == _selectedIndex)
                ColorConsole(true);                
            Console.WriteLine("[*] {0}",equippedItem.Name);
            ColorConsole(false);                

            itemIndex++;
        }
        Console.WriteLine("Inventory");
        itemIndex = 0;
        foreach (IItem item in _player.Inventory)
        {
            if (itemIndex == _selectedIndex-_player.EquippedItems.Count())
                ColorConsole(true);   
            Console.WriteLine("[ ] {0}",item.Name);
            ColorConsole(false);                
            itemIndex++;
        }
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