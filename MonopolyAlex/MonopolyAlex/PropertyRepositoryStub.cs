namespace MonopolyAlex;

public class PropertyRepositoryStub : IPropertyRepository
{
    public bool HasSavePropertyBeenCalled = false;

    public Property GetPropertyByName(string name)
    {
        var property = new Property();
        property.Name = name;
        return property;
    }
        
    public Property GetPropertiesByColour(string color)
    {
        var property = new Property();
        property.Colour = color;
        return property;
    }

    public void SaveProperty(Property property)
    {
        HasSavePropertyBeenCalled = true;
    }
}