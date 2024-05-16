namespace AdancedCSharp.Abstract;

public interface IEngineState : IDisposable
{
    void ProcessInput(ConsoleKeyInfo key);
    void Activate();
    void Deactivate();
}