namespace AdancedCSharp.Abstract;

public interface IItem
{
    string Name { get; }
    bool CanEquip { get; }
    bool CanUse { get; }

    Damage GetDamage(ICombatEntity entity);
    Damage ModifyDamage(Damage damage);
}