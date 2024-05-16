using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model;

public class BasicMob : ICombatEntity
{
    public string Name => "Basic Mob";
    public int HP { get; private set; }

    public BasicMob()
    {
        HP = 100;
    }
    public Damage GetDamage(Player player)
    {
        return new Damage("Rainbow Kick!", 10);
    }

    public void TakeDamage(Damage damage)
    {
        HP -= damage.Amount;
    }
}