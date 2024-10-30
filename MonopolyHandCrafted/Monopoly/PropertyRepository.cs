using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class PropertyRepository : IPropertyRepository
    {
        public Property GetPropertyByName(string name)
        {
            throw new ApplicationException("Need to mock this");
        }
        public Property GetPropertiesByColour(string name)
        {
            throw new ApplicationException("Need to mock this");
        }
        public void SaveProperty(Property property)
        {
            throw new ApplicationException("Need to mock this");
        }
    }
}
