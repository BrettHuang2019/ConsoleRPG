using AdancedCSharp.Model;

namespace AdancedCSharp.Abstract;

public interface ICombatEntity
{
    string Name { get; }
    int HP { get; }
    Damage GetDamage(Player player);
    void TakeDamage(Damage damage);
}