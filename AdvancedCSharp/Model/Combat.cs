using AdancedCSharp.Abstract;
using AdancedCSharp.Extensions;

namespace AdancedCSharp.Model;

public class Combat
{
    private readonly List<ICombatListener> _listeners;
    public Player Player { get; private set; }
    public ICombatEntity CombatEntity { get; private set; }

    
    public Combat(Player player, ICombatEntity entity)
    {
        _listeners = new List<ICombatListener>();
        Player = player;
        CombatEntity = entity;
    }

    public void UseItem(IItem item)
    {
        if (!item.CanUse)
        {
            _listeners.ForEach(f => f.DisplayMessage("Can't use" + item.Name));
            return;
        }

        PerformAction(item.GetDamage(CombatEntity));
    }
    
    
    public void UseAbility(IAbility ability)
    {
        PerformAction(ability.GetDamage(CombatEntity));
    }

    private void PerformAction(Damage damage)
    {
        _listeners.ForEach(f => f.DisplayMessage($"{CombatEntity.Name} took {damage.Amount} damage from {damage.Text}"));

        CombatEntity.TakeDamage(damage);
        if (CombatEntity.HP<=0)
        {
            _listeners.ForEach(f => f.DisplayMessage($"{CombatEntity.Name} died."));
            for (int i = 0; i < _listeners.Count; i++)
                _listeners[i].EndCombat();
            return;
        }

        damage = CombatEntity.GetDamage(Player);
        _listeners.ForEach(f => f.DisplayMessage($"Player took {damage.Amount} damage from {damage.Text}"));
        Player.TakeDamage(damage);
        if (Player.HP <=0)
        {
            _listeners.ForEach(f => f.PlayerDied());
        }
    }


    public void AddListener(ICombatListener listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(ICombatListener listener)
    {
        _listeners.Remove(listener);
    }
    
    
}