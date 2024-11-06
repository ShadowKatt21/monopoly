using MonopolyAlex;

namespace TestMonopolyNUnitAndNSub;

[TestFixture]
public class TestMonopolyNUnitMocks
{
    private Player _player;
    private Property _property;
    private HouseService _houseService;
    private PlayerRepositoryStub _playerRepositoryStub;
    private PropertyRepositoryStub _propertyRepositoryStub;
    
    [SetUp]
    public void Setup()
    {
        _player = TestHelper.CreatePlayer();
        _property = TestHelper.CreateProperty("Vine Street", "Orange");
        _playerRepositoryStub = new PlayerRepositoryStub();
        _propertyRepositoryStub = new PropertyRepositoryStub();
        _houseService = new HouseService(_playerRepositoryStub, _propertyRepositoryStub);
    }
    
    [Test]
    public void BuyHouse_PropertyHasMaxHouses_ReturnsMaxHouse()
    {
        _property.Houses = 4;
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_property.Houses, Is.EqualTo(4));
    }
    
    [Test]
    public void BuyHouse_PlayerDoesNotOwnAllPropertiesOfSameColour_ReturnsNoHouses()
    {
        _player.Properties.RemoveAt(0);
        _playerRepositoryStub.PlayerToReturn = _player;
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_property.Houses, Is.EqualTo(0));
    }

    [Test]
    public void BuyHouse_PlayerHasNoFunds_ReturnHousesAsIs()
    {
        _player.BankRoll = 50;
        _playerRepositoryStub.PlayerToReturn = _player;

        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.That(_property.Houses, Is.EqualTo(0));
    }
    
    [Test]
    public void BuyHouse_PlayerMatchesRequirements_ReturnHousesPlusOne()
    {
        _playerRepositoryStub.PlayerToReturn = _player;
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_property.Houses, Is.EqualTo(1));
    }

    [Test]
    public void BuyHouse_PlayerMatchesRequirements_DeductHouseCostFromPlayer()
    {
        _playerRepositoryStub.PlayerToReturn = _player;
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_player.BankRoll, Is.EqualTo(1900));
    }
    
    [Test]
    public void BuyHouse_PlayerMatchesRequirements_SaveProperty()
    {
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_propertyRepositoryStub.HasSavePropertyBeenCalled, Is.True);
    }
    
    [Test]
    public void BuyHouse_PlayerMatchesRequirements_SavePlayer()
    {
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_playerRepositoryStub.WasPlayerSaved, Is.True);
    }
}