using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElysiaBattleshipsWebAPI.Controllers;
using System.Data;
using ElysiaBattleshipsWebAPI.Models;
using System.Data.SqlClient;

namespace ElysiaBattleshipsWebAPI.Tests
{
    [TestClass]
    public class GameControllerTests
    {

        #region connectionstring
        static string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = qdc28p24";
        #endregion
        static SqlConnection connection = new SqlConnection(connectionstring);
        static SqlCommand command = new SqlCommand();
        //[TestMethod]
        //public void HitTest() //Work on adding more tests and fixing FireShot stored proc
        //{
        //    var gameController = new GameController();

        //    //Expecting a hit
        //    int isHit = gameController.shotIsHit(1, 1, 2, 9);
        //    Assert.AreEqual(true, isHit, "Shot should have hit, but did not");

        //    isHit = gameController.shotIsHit(1, 1, 2, 9);
        //    Assert.AreEqual(false, isHit, "Shot should have missed, but it hit something...");
        //}
        [TestMethod]
        public void PlaceShipTest()
        {
            var gameController = new GameController();

            command.Connection = connection;
            command.CommandType = CommandType.Text;

            Random random = new Random();

            string randomUsername = "TestUser" + random.Next(999, 9999);
            command.CommandText = $"INSERT into USERS VALUES ('{randomUsername}', 1)";

            connection.Open();
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var userID = Convert.ToInt32(command.ExecuteScalar());
 

            string randomRoomName = "TestRoom" + random.Next(999, 9999);
            command.CommandText = $"INSERT INTO Rooms VALUES ('{randomRoomName}', {userID}, null, 1, 1, 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 RoomID FROM Rooms ORDER BY RoomID DESC";
            var roomID = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            int x = random.Next(1, 10);
            int y = random.Next(1, 10);

            int randomShipTypeID = random.Next(1, 5);

            //command.CommandText = $"INSERT into UserShips VALUES ({randomShipTypeID}, {userID}, {roomID}, {x}, {y}, 1, 0, 0, 1)";

            Ship ship = new Ship(randomShipTypeID, userID, roomID, x, y, Ship.ShipNames.Battleship, Ship.ShipOrientations.Up, true);

            string isValid = gameController.placeShip(ship);
            Assert.AreEqual(false, isValid, "Ship placement should've been invalid, but it returned valid");
        }

        [TestMethod]
        public void ShootTest()
        {
            var gameController = new GameController();

            command.Connection = connection;
            command.CommandType = CommandType.Text;

            var random = new Random();

            connection.Open();

            string randomUsername = "TestUser" + random.Next(999, 9999);
            command.CommandText = $"INSERT INTO Users VALUES('{randomUsername}', 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var userID = Convert.ToInt32(command.ExecuteScalar());

            string randomRoomName = "TestRoom" + random.Next(999, 9999);
            command.CommandText = $"INSERT INTO Rooms VALUES('{randomRoomName}', '{userID}', 4, 0, 0, 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 RoomID FROM Rooms ORDER BY RoomID DESC";
            var roomID = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            int x = random.Next(1, 10);
            int y = random.Next(1, 10);

            int randomShipTypeID = random.Next(1, 5);

            Shot shot = new Shot(x, y, 1, userID, roomID, true);

            string isValid = gameController.shotIsHit(shot);

//            Assert.AreEqual("Hit", isValid, "Shot was supposed to hit, but didn't.");
            Assert.AreEqual("Miss", isValid, "Shot was supposed to miss, but didn't.");

        }
        
        [TestMethod]
        public void GetShipsTest()
        {
            var gameController = new GameController();

            command.Connection = connection;
            command.CommandType = CommandType.Text;

            var random = new Random();

            connection.Open();

            string randomUsername = "TestUser" + random.Next(999, 9999);
            command.CommandText = $"INSERT INTO Users VALUES('{randomUsername}', 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var userID = Convert.ToInt32(command.ExecuteScalar());

            string randomRoomName = "TestRoom" + random.Next(999, 9999);
            command.CommandText = $"INSERT INTO Rooms VALUES('{randomRoomName}', '{userID}', 4, 0, 0, 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 RoomID FROM Rooms ORDER BY RoomID DESC";
            var roomID = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            Room room = new Room(roomID, "", "", userID, true);

            DataTable shipTable = gameController.getShips(room);

            Assert.AreNotEqual(null, shipTable, "Table is empty");
        }
    }
}