namespace MonopolyAlex;

public class Player
{
    public IList<Property> Properties { get; set; }
    public int BankRoll { get; set; }
    public string Token { get; set; }
        
    public bool OwnsAllProperties(string colour)
    {
        var ownedProperties = Properties.Select(x => x.Colour == colour).Count();
        return ownedProperties == 3;
    }
}