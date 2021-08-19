using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Monopoly
{
    [TestClass]
    public class TestMonopoly
    {
        private Player _player;
        private Property _property;
        private HouseService _houseService;
        private Mock<IPlayerRepository> _playerRespositoryStub;
        private Mock<IPropertyRepository> _propertyRepositoryMock;
        
        [TestInitialize]
        public void Setup()
        {
            _player = TestHelper.CreatePlayer();
            _property = TestHelper.CreateProperty("Vine Street", "Orange");
            _playerRespositoryStub = new Mock<IPlayerRepository>();
            _propertyRepositoryMock = new Mock<IPropertyRepository>();
            _houseService = new HouseService(_playerRespositoryStub.Object, _propertyRepositoryMock.Object);
        }
        [TestMethod]
        public void ShouldnotBeAbleToBuyAHouseIfPropertyHasMaxHouses()
        {
           _property.Houses = 4;
           _houseService.BuyHouse(_property, "Battle Ship");

            Assert.AreEqual(_property.Houses, 4);
        }

        [TestMethod]
        public void ShouldNotBeAbleToBuyAHouseIfAPlayerDoesNotOwnAllThePropertiesOfTheColour()
        {
            _player.Properties.RemoveAt(0);
            ////_playerRespositoryStub.PlayerToReturn = _player;
            //_playerRespositoryStub.Stub(x => x.GetPlayerByToken("Battle Ship")).Return(_player);
            _playerRespositoryStub.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
            _houseService.BuyHouse(_property, "Battle Ship");

            Assert.AreEqual(_property.Houses, 0);
        }

        [TestMethod]
        public void ShouldBeAbleToBuyAHouseIfAPlayerOwnsAllThePropertiesOfTheColour()
        {
            //_playerRespositoryStub.PlayerToReturn = _player;
            //_playerRespositoryStub.Stub(x => x.GetPlayerByToken("Battle Ship")).Return(_player);
            _playerRespositoryStub.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);

            _houseService.BuyHouse(_property, "Battle Ship");

            Assert.AreEqual(_property.Houses, 1);
        }

        [TestMethod]
        public void ShouldNotBeAbleToBuyAHouseIfThebankRollIsLessThanTheCostOfTheHouse()
        {
            _player.BankRoll = 0;
            ////_playerRespositoryStub.PlayerToReturn = _player;
            //_playerRespositoryStub.Stub(x => x.GetPlayerByToken("Battle Ship")).Return(_player);
            _playerRespositoryStub.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
            _houseService.BuyHouse(_property, "Battle Ship");

            Assert.AreEqual(_property.Houses, 0);
        }

        [TestMethod]
        public void ShouldDeductHouseCostFromThePlayersMoney()
        {
            //_playerRespositoryStub.PlayerToReturn = _player;
            //_playerRespositoryStub.Stub(x => x.GetPlayerByToken("Battle Ship")).Return(_player);
            _playerRespositoryStub.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
            _houseService.BuyHouse(_property, "Battle Ship");
            Assert.AreEqual(1820, _player.BankRoll);
        }

        [TestMethod]
        public void ShouldSaveProperty()
        {
            _playerRespositoryStub.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);
            
            _houseService.BuyHouse(_property, "Battle Ship");

           _propertyRepositoryMock.Verify(rcv => rcv.SaveProperty(_property),Times.Exactly(1));
        }

        [TestMethod]
        public void ShouldSavePlayer()
        {
            _playerRespositoryStub.Setup(x => x.GetPlayerByToken("Battle Ship")).Returns(_player);

            _houseService.BuyHouse(_property, "Battle Ship");

            _playerRespositoryStub.Verify(rcv => rcv.SavePlayer(_player));
        }

    }
}
