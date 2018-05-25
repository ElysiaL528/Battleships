using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElysiaBattleshipsWebAPI.Controllers;
using System.Data;
using ElysiaBattleshipsWebAPI.Models;
using System.Data.SqlClient;
using static ElysiaBattleshipsWebAPI.Models.Ship;
using System.Collections.Generic;

namespace ElysiaBattleshipsWebAPI.Tests
{
    [TestClass]
    public class GameControllerTests //Fix PlaceShipTest & finish CheckNewShots test & debug any issues that resulted from new DeletetTestData proc
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

            Ship ship = new Ship(userID, roomID, x, y, 2, 1);

            string isValid = gameController.placeShip(ship);
            Assert.AreNotEqual(false, isValid, "Ship placement should've been valid, but it returned invalid");

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = connection;
            connection.Open();
            command.ExecuteScalar();
            connection.Close();
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

            int shipTypeID = 1;

            int shipX = 1;
            int shipY = 1;

            Ship ship = new Ship(userID, roomID, shipX, shipY, 1, 4);

            command.CommandText = $"INSERT INTO Ships VALUES ({userID}, {roomID}, {ship.ShipTypeID}, {shipX}, {shipY}, {ship.ShipOrientationID}, 0, 0, 1)";
            connection.Close();

            int shotX = random.Next(6, 10);
            int shotY = random.Next(6, 10);

            int randomShipTypeID = random.Next(1, 5);

            Shot shot = new Shot(shotX, shotY, 1, userID, roomID, true);

            string isValid = gameController.ShotIsHit(shot);

            Assert.AreEqual("Miss", isValid, "Shot was supposed to miss, but didn't.");

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = connection;
            connection.Open();
            command.ExecuteScalar();
            connection.Close();

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

            Room room = new Room(roomID, "", "", userID);

            List<Ship> shipTable = gameController.getShips(room);

            Assert.AreNotEqual(null, shipTable, "Table is empty");

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = connection;
            connection.Open();
            command.ExecuteScalar();
            connection.Close();
        }

        [TestMethod]
        public void CheckNewShots()
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

            int x = random.Next(1, 10);
            int y = random.Next(1, 10);

            command.CommandText = "SELECT TOP 1 ShotID FROM Shots ORDER BY ShotID DESC";
            int lastShotID = Convert.ToInt32(command.ExecuteScalar());

            command.CommandText = $"INSERT INTO Shots VALUES ({userID}, {roomID}, {x}, {y}, 0, 1)";
            command.ExecuteScalar();

            //int randomShipTypeID = random.Next(1, 5);

            var room = new Room(roomID, "", "", userID);
            room.LastShotID = lastShotID;
            connection.Close();

            bool newShots = gameController.checkForShots(room);

            Assert.AreEqual(true, newShots, "Test should have returned true, but returned false.");

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = connection;
            connection.Open();
            command.ExecuteScalar();
            connection.Close();
        }

        [TestMethod]
        public void CheckUsersReady()
        {
            var gameController = new GameController();

            Random random = new Random();

            command.Connection = connection;
            command.CommandType = CommandType.Text;
            connection.Open();

            string username = "TestUser" + random.Next(999, 9999);

            command.CommandText = $"INSERT INTO Users VALUES ('{username}', 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var hostPlayerID = Convert.ToInt32(command.ExecuteScalar());

            username = "TestUser" + random.Next(999, 9999);

            command.CommandText = $"INSERT INTO Users VALUES ('{username}', 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var joinedPlayerID = Convert.ToInt32(command.ExecuteScalar());

            string roomName = "TestRoom" + random.Next(999, 9999);

            command.CommandText = $"INSERT INTO Rooms VALUES ('{roomName}', {hostPlayerID}, {joinedPlayerID}, 0, 0, 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 RoomID FROM Rooms ORDER BY RoomID DESC";
            var roomID = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            var room = new Room(roomID, roomName, "", hostPlayerID);

            DataTable usersReadyStatus = gameController.checkPlayersReady(room);

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = connection;
            connection.Open();
            command.ExecuteScalar();
            connection.Close();

        }

        [TestMethod]
        public void SetUsersReadyTest()
        {
            var gameController = new GameController();

            command.Connection = connection;
            command.CommandType = CommandType.Text;
            connection.Open();

            var random = new Random();
            var username = "TestUser" + random.Next(999, 9999);

            command.CommandText = $"INSERT INTO Users VALUES ('{username}', 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var userID = Convert.ToInt32(command.ExecuteScalar());

            var secondUsername = "TestUser" + random.Next(999, 9999);

            command.CommandText = $"INSERT INTO Users VALUES ('{secondUsername}', 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var secondUserID = Convert.ToInt32(command.ExecuteScalar());

            string roomName = "TestRoom" + random.Next(999, 9999);

            command.CommandText = $"INSERT INTO Rooms VALUES ('{roomName}', {userID}, {secondUserID}, 1, 1, 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 RoomID FROM Rooms ORDER BY RoomID DESC";
            var roomID = Convert.ToInt32(command.ExecuteScalar());

            int x = 1;
            int shipTypeID = 1;
            
            for(int i = 0; i < 5; i++)
            {
                command.CommandText = $"INSERT INTO UserShips VALUES ({userID}, {roomID}, {shipTypeID}, {x}, 1, 2, 0, 0, 1)";
                command.ExecuteScalar();
                shipTypeID++;
                x++;
            }

            connection.Close();

            var room = new Room(roomID, roomName, username, userID);

            DataTable table = gameController.setPlayerReady(room);

            bool playerIsReady = false;
            if (Convert.ToInt32(table.Rows[0][4]) == 1)
            {
                playerIsReady = true;
            }

            Assert.AreEqual(true, playerIsReady, "Player should have been set to ready status, but was not.");

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = connection;
            connection.Open();
            command.ExecuteScalar();
            connection.Close();

        }
    }
}