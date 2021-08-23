using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    class HouseService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPlayerRepository _playerRepository;

        public HouseService(IPlayerRepository playerRepository, IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;
            _playerRepository = playerRepository;
        }

        public void BuyHouse(Property property, string token)
        {
            var player = _playerRepository.GetPlayerByToken(token);

            if (property.HasMaxHouses) return;

            if (!player.OwnsAllProperties(property.Colour)) return;

            if (player.BankRoll < property.HouseCost) return;

            player.BankRoll -= property.HouseCost;
            property.Houses++;

            _playerRepository.SavePlayer(player);
            _propertyRepository.SaveProperty(property);

            RubbishMethod(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
        }

        public void RubbishMethod(int q, int w, int e, int r, int t, int y, int u, int i, int o, int p)
         {
            var password = "Password";
            int ghg; //unused

            if (password is null)
                return;

            for (int j = 0; j < 10; j++)
            {
                ;
            }

            return;
         }

    }
}
