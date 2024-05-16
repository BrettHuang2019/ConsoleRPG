using AdancedCSharp.Model;

namespace AdancedCSharp.Abstract;

public interface IComponent
{
    Entity Parent { get; set; }
}