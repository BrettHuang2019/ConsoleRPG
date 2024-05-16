namespace AdancedCSharp.Abstract;

public interface ICombatListener
{
    void DisplayMessage(string message);
    void EndCombat();
    void PlayerDied(); 
}
