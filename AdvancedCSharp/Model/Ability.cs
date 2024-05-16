using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model;

public class Ability : IAbility
{
    private readonly int _damage;
    public string Name { get; }

    public Ability(string name, int damage)
    {
        Name = name;
        _damage = damage;
    }
    public Damage GetDamage(ICombatEntity entity)
    {
        return new Damage(Name, _damage);
    }
}