using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model;

public class Player
{
    private readonly List<IItem> _equippedItems;
    private readonly List<IItem> _inventory;
    private readonly List<IAbility> _abilities;

    public IEnumerable<IItem> EquippedItems => _equippedItems;
    public IEnumerable<IItem> Inventory => _inventory;
    public IEnumerable<IAbility> Abilities => _abilities;
    public int HP { get; private set; }
    public Player()
    {
        HP = 200;
        _equippedItems = new List<IItem>();
        _inventory = new List<IItem>();
        _abilities = new List<IAbility>();
    }

    public void AddItem(IItem item)
    {
        _inventory.Add(item);
    }

    public void EquipItem(IItem item)
    {
        _inventory.Remove(item);
        _equippedItems.Add(item);
    }
    public void UnequipItem(IItem item)
    {
        _equippedItems.Remove(item);
        _inventory.Add(item);
    }

    public void AddAbility(IAbility ability)
    {
        _abilities.Add(ability);
    }

    public void TakeDamage(Damage damage)
    {
        damage = _equippedItems.Aggregate(damage, (a, i) => i.ModifyDamage(a));
        int damageAmount = Math.Max(damage.Amount, 0);
        HP -= damageAmount;
    }
}