using ElysiaBattleshipsWebAPI.Controllers;
using ElysiaBattleshipsWebAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElysiaBattleshipsWebAPI.Tests
{
    [TestClass]
    public class RoomControllerTests 
    {
        #region connectionstring
        static string connectionString = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = qdc28p24";
        #endregion

        public static SqlConnection sqlConnection = new SqlConnection(connectionString);
        SqlCommand command = new SqlCommand();

        [TestMethod]
        public void RegisterTest()
        {
            var roomController = new RoomController();
            Random random = new Random();

            var user = new User(1, "TestUser" + random.Next(9999, 99999), true);

            var result = roomController.CreateUser(user);

            Assert.AreNotEqual(0, result);

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = sqlConnection;
            sqlConnection.Open();
            command.ExecuteScalar();
            sqlConnection.Close();
        }

        [TestMethod]
        public void CreateRoomTest()
        {
            var roomController = new RoomController();

            Random random = new Random();

            command.Connection = sqlConnection;
            command.CommandType = System.Data.CommandType.Text;

            var username = "TestUser" + random.Next(999, 9999);
            sqlConnection.Open();

            command.CommandText = $"INSERT INTO Users VALUES ('{username}', 1)";
            command.ExecuteScalar();

            command.CommandText = "SELECT TOP 1 UserID FROM Users ORDER BY UserID DESC";
            var userID = Convert.ToInt32(command.ExecuteScalar());

            Room room = new Room(1, "TestRoom" + random.Next(9999, 99999), username, userID);
            sqlConnection.Close();

            var result = roomController.CreateRoom(room);

            Assert.AreNotEqual(0, result);

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Connection = sqlConnection;
            sqlConnection.Open();
            command.ExecuteScalar();
            sqlConnection.Close();
        }

        [TestMethod]
        public void JoinRoomTest()
        {
            var roomController = new RoomController();

            Room room = new Room(2, "", "", 11);
            var result = roomController.JoinRoom(room);

            Assert.AreNotEqual(null, result);
        }

    }
}
