namespace MonopolyAlex;

public class PlayerRepository : IPlayerRepository
{
    public Player GetPlayerByToken(string token)
    {
        throw new ApplicationException("Need to mock this");
    }

    public void SavePlayer(Player player)
    {
        throw new ApplicationException("Need to mock this");
    }
}