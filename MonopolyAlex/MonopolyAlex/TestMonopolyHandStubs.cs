using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MonopolyAlex;

[TestClass]
public class TestMonopolyHandStubs
{
    private Player _player;
    private Property _property;
    private HouseService _houseService;
    private PlayerRepositoryStub _playerRespositoryStub;
    private PropertyRepositoryStub _propertyRepositoryMock;

    [TestInitialize]
    public void Setup()
    {
        _player = TestHelper.CreatePlayer();
        _property = TestHelper.CreateProperty("Vine Street", "Orange");

        _playerRespositoryStub = new PlayerRepositoryStub();
        _propertyRepositoryMock = new PropertyRepositoryStub();

        _houseService = new HouseService(_playerRespositoryStub, _propertyRepositoryMock);
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
        _playerRespositoryStub.PlayerToReturn = _player;
        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.AreEqual(_property.Houses, 0);
    }

    [TestMethod]
    public void AbleToBuyAHouseIfPlayerOwnsAllPropertiesOfSameColour()
    {
        _playerRespositoryStub.PlayerToReturn = _player;


        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.AreEqual(_property.Houses, 1);
    }

    [TestMethod]
    public void NotBeAbleToBuyAHouseIfBankRollIsLessThanCostOfHouse()
    {
        _player.BankRoll = 0;
        _playerRespositoryStub.PlayerToReturn = _player;

        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.AreEqual(_property.Houses, 0);
    }

    [TestMethod]
    public void DeductHouseCostFromPlayersMoney()
    {
        _playerRespositoryStub.PlayerToReturn = _player;

        _houseService.BuyHouse(_property, "Battle Ship");
        Assert.AreEqual(1900, _player.BankRoll);
    }

    [TestMethod]
    public void SaveProperty()
    {
        
        _houseService.BuyHouse(_property, "Battle Ship");

        Assert.IsTrue(_propertyRepositoryMock.HasSavePropertyBeenCalled);
    }

    [TestMethod]
    public void SavePlayer()
    {
    

        _houseService.BuyHouse(_property, "Battle Ship");
        Assert.IsTrue(_playerRespositoryStub.WasPlayerSaved);
    }
}