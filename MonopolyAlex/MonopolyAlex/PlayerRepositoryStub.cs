namespace MonopolyAlex;

public class PlayerRepositoryStub : IPlayerRepository
{
    public Player PlayerToReturn;
    public bool WasPlayerSaved;

    public Player GetPlayerByToken(string token)
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