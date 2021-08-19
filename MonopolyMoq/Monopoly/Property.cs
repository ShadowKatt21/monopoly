using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
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
    
}
