namespace MonopolyAlex;

public class HouseService(IPlayerRepository playerRepository, IPropertyRepository propertyRepository)
{
    public void BuyHouse(Property property, string token)
    {
        var player = playerRepository.GetPlayerByToken(token);

        if (property.HasMaxHouses) return;

        if (!player.OwnsAllProperties(property.Colour)) return;

        if (player.BankRoll < property.HouseCost) return;

        player.BankRoll -= property.HouseCost;
        property.Houses++;

        playerRepository.SavePlayer(player);
        propertyRepository.SaveProperty(property);

    }
}