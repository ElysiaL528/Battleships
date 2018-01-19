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
        [TestMethod]
        public void RegisterTest()
        {
            #region connectionstring
             string connectionString = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = qdc28p24";
            #endregion

             SqlConnection sqlConnection = new SqlConnection(connectionString);
             SqlCommand command = new SqlCommand();
            var roomController = new RoomController();

            Random random = new Random();

            var user = new User(1, "TestUser" + random.Next(9999, 99999), true);

            var result = roomController.CreateUser(user);

            Assert.AreNotEqual(0, result);

            //Delete test data
            command.CommandText = "usp_DeleteTestData";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.ExecuteScalar();
        }

        [TestMethod]
        public void CreateRoomTest()
        {
            var roomController = new RoomController();

            Random random = new Random();

            Room room = new Room(1, "TestRoom" + random.Next(9999, 99999), "TestUsername", 3, true);

            var result = roomController.CreateRoom(room);

            Assert.AreNotEqual(0, result);
        }

        [TestMethod]
        public void JoinRoomTest()
        {
            var roomController = new RoomController();

            
        }

    }
}
