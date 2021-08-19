using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class PlayerRepository : IPlayerRepository
    {
        public Player GetPlayerByToken(string Token)
        {
            throw new ApplicationException("Need to mock this");
        }

        public void SavePlayer(Player player)
        {
            throw new ApplicationException("Need to mock this");
        }
    }
}
