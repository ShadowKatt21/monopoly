using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class PlayerRepositoryStub : IPlayerRepository
    {
        public Player PlayerToReturn;
        public bool WasPlayerSaved;

        public Player GetPlayerByToken(string Token)
        {
            if (PlayerToReturn == null)
                PlayerToReturn = TestHelper.CreatePlayer();

            return PlayerToReturn;
        }

        public void SavePlayer(Player player)
        {
            WasPlayerSaved = true;
        }
    }
}
