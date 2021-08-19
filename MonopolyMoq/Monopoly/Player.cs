using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Player
    {
        public IList<Property> Properties { get; set; }
        public int BankRoll { get; set; }
        public string Token { get; set; }
        
        public bool OwnsAllProperties(string Colour)
        {
            var ownedProperties = Properties.Select(x => x.Colour == Colour).Count();
            return ownedProperties == 3;
        }

    }
}
