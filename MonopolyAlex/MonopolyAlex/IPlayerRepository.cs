namespace MonopolyAlex;

public interface IPlayerRepository
{
    Player GetPlayerByToken(string token);
    void SavePlayer(Player player);
}