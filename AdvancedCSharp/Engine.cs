using AdancedCSharp.Abstract;

namespace AdancedCSharp;

public class Engine
{
    public static Engine Instance { get; private set; }
    
    private Stack<IEngineState> states = new Stack<IEngineState>();

    public bool IsRunning { get; private set; }
    public IEngineState CurrentState => states.Peek();

    public Engine()
    {
        states = new Stack<IEngineState>();
        IsRunning = true;
        Instance = this;
    }

    public void Quit()
    {
        IsRunning = false;
        states.Clear();
    }

    public void PushState(IEngineState state)
    {
        if (states.Count>0)
            states.Peek().Deactivate();
        
        state.Activate();
        states.Push(state);
    }


    public void PopState(IEngineState state)
    {
        if (states.Count==0 && state != states.Peek())
            throw new InvalidOperationException("No states left of stack, or trying to pop an invalid state");

        states.Pop(); 
        state.Deactivate();
        state.Dispose();
        if (states.Count>0)
        {
            states.Peek().Activate();
        }
    }

    public void SwitchState(IEngineState state)
    {
        if (states.Count > 0)
        {
            var oldState = states.Pop();
            oldState.Deactivate();
            oldState.Dispose();
        }

        PushState(state);
    }

    public void ProcessInput(ConsoleKeyInfo key)
    {
        if (states.Count>0)
        {
            states.Peek().ProcessInput(key);
        }
    }
    
    
    
}
