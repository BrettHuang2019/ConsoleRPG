namespace AdancedCSharp;

public class Damage
{
    public string Text { get; }
    public int Amount { get; }

    public Damage(string text, int amount)
    {
        Text = text;
        Amount = amount;
    }

    public Damage ModifyAmount(int delta)
    {
        return new Damage(Text, Amount + delta);
    }
}