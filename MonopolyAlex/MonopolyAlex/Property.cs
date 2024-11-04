namespace MonopolyAlex;

public class Property
{
    public string Name { get; set; }
    public string Colour { get; set; }
    public int Houses { get; set; }
    public int HouseCost { get; set; }
    public bool HasMaxHouses
    {
        get
        {
            return Houses == 4;
        }
    }
}