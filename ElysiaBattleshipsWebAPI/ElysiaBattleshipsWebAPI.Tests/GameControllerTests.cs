using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElysiaBattleshipsWebAPI.Controllers;

namespace ElysiaBattleshipsWebAPI.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        [TestMethod]
        public void HitTest()
        {
/*            var gameController = new GameController();

            //Expecting a hit
            bool isHit = gameController.isHit(1, 1, 2, 9);
            Assert.AreEqual(true, isHit, "Shot should have hit, but did not");

            isHit = gameController.isHit(1, 1, 2, 9);
            Assert.AreEqual(false, isHit, "Shot should have missed, but it hit something...");*/
        }
        [TestMethod]
        public void PlaceShipTest()
        {
            var gameController = new GameController();

            bool isValid = gameController.placeShip(1, 1, 5, 7, GameController.ShipTypes.Cruiser, 3);
            Assert.AreEqual(false, isValid, "Ship placement should've been invalid, but it returned valid");
        }

    }
}