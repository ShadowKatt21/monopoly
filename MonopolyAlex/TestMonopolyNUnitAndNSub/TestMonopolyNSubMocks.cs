using MonopolyAlex;
using NSubstitute;

namespace TestMonopolyNUnitAndNSub;

[TestFixture]
public class TestMonopolyNSubMocks
{
    private Player _player;
    private Property _property;
    private HouseService _houseService;
    private IPlayerRepository _playerRepositoryMock;
    private IPropertyRepository _propertyRepositoryMock;
    
    [SetUp]
    public void Setup()
    {
        _player = TestHelper.CreatePlayer();
        _property = TestHelper.CreateProperty("Vine Street", "Orange");
        _playerRepositoryMock = Substitute.For<IPlayerRepository>();
        _propertyRepositoryMock = Substitute.For<IPropertyRepository>();
        _houseService = new HouseService(_playerRepositoryMock, _propertyRepositoryMock);
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
        _playerRepositoryMock.GetPlayerByToken("Battle Ship").Returns(_player);
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_property.Houses, Is.EqualTo(0));
    }

    [Test]
    public void BuyHouse_PlayerHasNoFunds_ReturnHousesAsIs()
    {
        _player.BankRoll = 50;
        _playerRepositoryMock.GetPlayerByToken("Battle Ship").Returns(_player);

        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.That(_property.Houses, Is.EqualTo(0));
    }
    
    [Test]
    public void BuyHouse_PlayerMatchesRequirements_ReturnHousesPlusOne()
    {
        _playerRepositoryMock.GetPlayerByToken("Battle Ship").Returns(_player);
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_property.Houses, Is.EqualTo(1));
    }

    [Test]
    public void BuyHouse_PlayerMatchesRequirements_DeductHouseCostFromPlayer()
    {
        _playerRepositoryMock.GetPlayerByToken("Battle Ship").Returns(_player);
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        Assert.That(_player.BankRoll, Is.EqualTo(1900));
    }
    
    [Test]
    public void BuyHouse_PlayerMatchesRequirements_SaveProperty()
    {
        _playerRepositoryMock.GetPlayerByToken("Battle Ship").Returns(_player);
        _propertyRepositoryMock.SaveProperty(Arg.Any<Property>());
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        _propertyRepositoryMock.Received().SaveProperty(Arg.Any<Property>());
    }
    
    [Test]
    public void BuyHouse_PlayerMatchesRequirements_SavePlayer()
    {
        _playerRepositoryMock.GetPlayerByToken("Battle Ship").Returns(_player);
        _playerRepositoryMock.SavePlayer(_player);
        
        _houseService.BuyHouse(_property, "Battle Ship");
        
        _playerRepositoryMock.Received().SavePlayer(_player);
    }
}