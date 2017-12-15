using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElysiaBattleshipsWebAPI.Controllers;
using System.Data;

namespace ElysiaBattleshipsWebAPI.Tests
{
    [TestClass]
    public class GameControllerTests
    {
        [TestMethod]
        public void HitTest()
        {
            var gameController = new GameController();



            //Expecting a hit
            int isHit = gameController.shotIsHit(1, 1, 2, 9);
            Assert.AreEqual(true, isHit, "Shot should have hit, but did not");

            isHit = gameController.shotIsHit(1, 1, 2, 9);
            Assert.AreEqual(false, isHit, "Shot should have missed, but it hit something...");
        }
        [TestMethod]
        public void PlaceShipTest()
        {
            var gameController = new GameController();

            DataTable isValid = gameController.placeShip(1, 1, 5, 7, GameController.ShipTypes.Cruiser, 3);
            Assert.AreEqual(false, isValid, "Ship placement should've been invalid, but it returned valid");
        }

    }
}