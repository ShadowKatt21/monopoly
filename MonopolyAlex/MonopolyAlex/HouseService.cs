namespace MonopolyAlex;

public class HouseService
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

    }
}