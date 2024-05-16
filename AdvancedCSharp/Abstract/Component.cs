using AdancedCSharp.Model;

namespace AdancedCSharp.Abstract;

public abstract class Component : IComponent
{
    public Entity Parent { get; set; }
}