using AdancedCSharp.Abstract;

namespace AdancedCSharp.Model.Components;

public class PlayerComponent : Component
{
    public Player Player { get; }

    public PlayerComponent(Player player)
    {
        Player = player;
    }

}