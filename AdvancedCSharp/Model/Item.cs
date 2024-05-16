using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model;

public class Item : IItem
{
    private int _damageModifier;
    private int _totalDamage;
    public string Name { get; }
    public bool CanEquip { get; }
    public bool CanUse { get; }
    
    public Item(string name, bool canEquip, bool canUse, int? damageModifier = null, int? totoalDamage = null)
    {
        Name = name;
        CanEquip = canEquip;
        CanUse = canUse;
        _damageModifier = damageModifier ?? 0;
        _totalDamage = totoalDamage ?? 0;
    }
    public Damage GetDamage(ICombatEntity entity)
    {
        return new Damage(Name, _totalDamage);
    }

    public Damage ModifyDamage(Damage damage)
    {
        return damage.ModifyAmount(_damageModifier);
    }
}


