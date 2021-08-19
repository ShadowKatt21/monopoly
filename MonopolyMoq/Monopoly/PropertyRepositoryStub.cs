using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class PropertyRepositoryStub: IPropertyRepository
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
}
