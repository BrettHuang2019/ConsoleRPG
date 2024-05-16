namespace AdancedCSharp.Abstract;

public interface IAbility
{
    string Name { get; }
    Damage GetDamage(ICombatEntity entity);

}