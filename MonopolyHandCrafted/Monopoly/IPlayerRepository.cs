namespace Monopoly
{
    public interface IPlayerRepository
    {
        Player GetPlayerByToken(string Token);
        void SavePlayer(Player player);
    }
}