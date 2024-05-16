using AdancedCSharp.Abstract;
using AdancedCSharp.Model;
using AdancedCSharp.Model.Components;

namespace AdancedCSharp.States;

public class ZoneState : IEngineState
{
    private readonly Entity _player;
    private readonly Zone _zone;
    private readonly ZoneRenderer _renderer;

    public ZoneState(Entity player, Zone zone)
    {
        _player = player;
        _zone = zone;
        _renderer = new ZoneRenderer(zone);
        
        _zone.AddListener(_renderer);
    }
    
    public void Dispose()
    {
        _zone.RemoveListener(_renderer);
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        Vector3 pos = _player.Position;
        
        switch (key.Key)
        {
            case ConsoleKey.Z: 
                Engine.Instance.PushState(new MainMenuState(_player.GetComponent<PlayerComponent>().Player));
                break;
            case ConsoleKey.W:
                _zone.MoveEntity(_player, new Vector3(pos.X, pos.Y-1, pos.Z));
                break;
            case ConsoleKey.A:
                _zone.MoveEntity(_player, new Vector3(pos.X-1, pos.Y, pos.Z));
                break;
            case ConsoleKey.S:
                _zone.MoveEntity(_player, new Vector3(pos.X, pos.Y+1, pos.Z));
                break;
            case ConsoleKey.D:
                _zone.MoveEntity(_player, new Vector3(pos.X+1, pos.Y, pos.Z));
                break;
        }

    }

    public void Activate()
    {
        _renderer.IsActive = true;
        _renderer.RenderAll();
    }

    public void Deactivate()
    {
        _renderer.IsActive = false;

    }
}