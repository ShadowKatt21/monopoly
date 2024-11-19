using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace TestMonopolyStubsAndMocks;

[TestClass]
public class TestMonopolyMocks
{
    private Player _player;
    private Property _property;
    private HouseService _houseService;
    private Mock<IPlayerRepository> _playerRepositoryMock;
    private Mock<IPropertyRepository> _propertyRepositoryMock;

    [TestInitialize]
    public void Setup()
    {
        _player = TestHelper.CreatePlayer();
        _property = TestHelper.CreateProperty("Vine Street", "Orange");
        _playerRepositoryMock = new Mock<IPlayerRepository>();
        _propertyRepositoryMock = new Mock<IPropertyRepository>();
        _houseService = new HouseService(_playerRepositoryMock.Object, _propertyRepositoryMock.Object);
    }
    
    [TestMethod]
    public void NotAbleToBuyAHouseIfPropertyHasMaxHouses()
    {
       _property.Houses = 4;
       _houseService.BuyHouse(_property, "Battle Ship");

        Assert.AreEqual(_property.Houses, 4);
    }

    [TestMethod]
    public void NotAbleToBuyAHouseIfPlayerDoesNotOwnAllPropertiesOfSameColour()
    {
        _player.Properties.RemoveAt(0);
        _playerRepositoryMock.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.AreEqual(_property.Houses, 0);
    }

    [TestMethod]
    public void AbleToBuyAHouseIfPlayerOwnsAllPropertiesOfSameColour()
    {
        _playerRepositoryMock.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
        
        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.AreEqual(_property.Houses, 1);
    }

    [TestMethod]
    public void NotBeAbleToBuyAHouseIfBankRollIsLessThanCostOfHouse()
    {
        _player.BankRoll = 0;
        _playerRepositoryMock.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);

        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.AreEqual(_property.Houses, 0);
    }

    [TestMethod]
    public void DeductHouseCostFromPlayersMoney()
    {
        _playerRepositoryMock.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
        _houseService.BuyHouse(_property, "Battle Ship");
        Assert.AreEqual(1900, _player.BankRoll);
    }

    [TestMethod]
    public void SaveProperty()
    {
        _playerRepositoryMock.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
            
        _houseService.BuyHouse(_property, "Battle Ship");

        _propertyRepositoryMock.Verify(rcv => rcv.SaveProperty(_property),Times.Exactly(1));
    }

    [TestMethod]
    public void SavePlayer()
    {
        _playerRepositoryMock.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);

        _houseService.BuyHouse(_property, "Battle Ship");

        _playerRepositoryMock.Verify(rcv => rcv.SavePlayer(_player));
    }
}