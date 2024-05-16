using AdancedCSharp.Abstract;
using AdancedCSharp.Model;
using AdancedCSharp.Model.Components;

namespace AdancedCSharp.States;

public class ZoneRenderer: IZoneListener
{
    private readonly Zone _zone;
    private readonly SpriteComponent[,,] _spriteBuffer;
    public bool IsActive { get; set; }

    public ZoneRenderer(Zone zone)
    {
        IsActive = true;
        _zone = zone;
        _spriteBuffer = new SpriteComponent[_zone.Size.X, _zone.Size.Y, _zone.Size.Z];
        foreach (Entity entity in _zone.Entities)
        {
            SpriteComponent component = entity.GetComponent<SpriteComponent>();
            if (component == null)
                continue;
            
            _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = component;
        }
    }
    public void RenderAll()
    {
        if (!IsActive)
            return;
        
        Console.Clear();
        Console.WriteLine($"Zone {_zone.Name.ToUpper()}");
        foreach (SpriteComponent sprite in _spriteBuffer)
        {
            if (sprite==null)
                continue;
            WriteCharacter(sprite.Parent.Position, sprite.Sprite);
        }
    }

    public void EntityMoved(Entity entity, Vector3 newPosition)
    {
        SpriteComponent sprite = entity.GetComponent<SpriteComponent>();
        if (sprite == null)
            return;

        // Previous Position
        _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;
        
        // new position
        _spriteBuffer[newPosition.X, newPosition.Y, newPosition.Z] = sprite;
        
        if (!IsActive)
            return;
        
        // Render old position
        SpriteComponent nextEntity = GetTopMostSprite(entity.Position);
        if (nextEntity != null)
            WriteCharacter(entity.Position, nextEntity.Sprite);
        else
            WriteCharacter(entity.Position, ' ');

        // Render new Posistion
        SpriteComponent topEntity = GetTopMostSprite(newPosition);
        if (topEntity != null && topEntity.Parent.Position.Z > newPosition.Z)
            WriteCharacter(newPosition, topEntity.Sprite);
        else
            WriteCharacter(newPosition, sprite.Sprite);
    }

    public void EntityAdded(Entity entity)
    {
        var sprite = entity.GetComponent<SpriteComponent>();
        if (sprite == null)
            return;

        _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = sprite;

        if (!IsActive)
            return;

        SpriteComponent topEntity = GetTopMostSprite(entity.Position);
        if (topEntity != null && topEntity.Parent.Position.Z > entity.Position.Z)
            WriteCharacter(entity.Position, topEntity.Sprite);

    }

    public void EntityRemoved(Entity entity)
    {
        var sprite = entity.GetComponent<SpriteComponent>();
        if (sprite == null)
            return;
        _spriteBuffer[entity.Position.X, entity.Position.Y, entity.Position.Z] = null;

        if (!IsActive)
            return;
        
        SpriteComponent nextEntity = GetTopMostSprite(entity.Position);
        if (nextEntity != null)
            WriteCharacter(entity.Position, nextEntity.Sprite);
        else
            WriteCharacter(entity.Position, ' ');

    }

    private SpriteComponent GetTopMostSprite(Vector3 position)
    {
        SpriteComponent topEntity = null;
        for (int i = _zone.Size.Z - 1; i >=0; i--)
        {
            topEntity = _spriteBuffer[position.X, position.Y, i];
            if (topEntity == null)
                continue;
            break;
        }

        return topEntity;
    }

    private void WriteCharacter(Vector3 position, char character)
    {
        SetCursorPosition(position);
        Console.Write(character);
    }

    private void SetCursorPosition(Vector3 position)
    {
        Console.SetCursorPosition(position.X, position.Y + 1);
    }
}