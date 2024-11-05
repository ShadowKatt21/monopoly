using MonopolyAlex;

namespace TestMonopolyNUnitAndNSub;

[TestFixture]
public class Tests
{
    private Player _player;
    private Property _property;
    private HouseService _houseService;
    private PlayerRepositoryStub _playerRepositoryStub;
    private PropertyRepositoryStub _propertyRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        _player = TestHelper.CreatePlayer();
        _property = TestHelper.CreateProperty("Vine Street", "Orange");
        _playerRepositoryStub = new PlayerRepositoryStub();
        _propertyRepositoryMock = new PropertyRepositoryStub();
        _houseService = new HouseService(_playerRepositoryStub, _propertyRepositoryMock);
    }

    
    [Test]
    public void BuyHouse_PropertyHasHousesPlayerHasNoFunds_ReturnHousesAsIs()
    {
        _property.Houses = 1;
        _player.BankRoll = 50;
        // this is how you should write unit tests - avoid adding code like if statements
        // in tests as you are testing the logic only 
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_property.Houses, Is.EqualTo(1));
    }
    
    [Test]
    public void BuyHouse_PropertyHasHouses_ReturnHousesPlusOne()
    {
        _property.Houses = 1;
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_property.Houses, Is.EqualTo(2));
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
}