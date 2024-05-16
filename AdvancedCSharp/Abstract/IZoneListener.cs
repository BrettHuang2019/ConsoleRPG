using AdancedCSharp.Model;

namespace AdancedCSharp.Abstract;

public interface IZoneListener
{
    void EntityMoved(Entity entity, Vector3 newPosition);
    void EntityAdded(Entity entity);
    void EntityRemoved(Entity entity);
}