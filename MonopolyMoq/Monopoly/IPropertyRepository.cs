namespace Monopoly
{
    public interface IPropertyRepository
    {
         Property GetPropertyByName(string name);
         Property GetPropertiesByColour(string name);
         void SaveProperty(Property property);
    }
}