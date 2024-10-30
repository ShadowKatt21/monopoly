using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class TestHelper
    {
        public static Player CreatePlayer()
        {
            var player = new Player();
            player.BankRoll = 2000;
            player.Token = "Battle Ship";
            player.Properties = GetProperties();

            return player;
        }

        private static IList<Property> GetProperties()
        {
            var properties = new List<Property>();
            properties.Add(CreateProperty("Bow Street", "Orange"));
            properties.Add(CreateProperty("Malboro", "Orange"));
            properties.Add(CreateProperty("Vine Street", "Orange"));

            return properties;
        }

        public static Property CreateProperty(string name, string colour)
        {
            var property = new Property();
            property.Colour = colour;
            property.HouseCost = 100;
            property.Houses = 0;
            property.Name = name;

            return property;
        }
    }
}
