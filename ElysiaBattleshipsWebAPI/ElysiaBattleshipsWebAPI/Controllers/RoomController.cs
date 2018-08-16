using ElysiaBattleshipsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ElysiaBattleshipsWebAPI.Controllers
{
    [RoutePrefix("api/Room")]
    public class RoomController : ApiController 
    {

        #region connectionstring
        static string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = pleasant1 ";
        #endregion
        static SqlConnection connection = new SqlConnection(connectionstring);
        static SqlCommand command = new SqlCommand();

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="Username"></param>


        [HttpPost]
        [Route("Register")]
        public void CreateUser([FromBody]User user)
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateUser";
            command.Parameters.Add(new SqlParameter("Username", user.Username));
            command.Connection = connection;

            connection.Open();
            command.ExecuteScalar();
            connection.Close();
        }

        /// <summary>
        /// Creates a new room
        /// </summary>
        /// <param name="RoomName"></param>
        /// <param name="UserID"></param>
        /// 

        [HttpPost]
        [Route("CreateRoom")]
        public void CreateRoom([FromBody]Room room)
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateRoom";
            command.Parameters.Add(new SqlParameter("RoomName", room.RoomName));
            command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));
            command.Connection = connection;

            connection.Open();
            command.ExecuteScalar();
            connection.Close();
        }

        /// <summary>
        /// Gets a list of available rooms
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetRooms")]
        public DataTable GetRooms()
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetRooms";
            command.Connection = connection;
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;
        }

        /// <summary>
        /// Joins a room
        /// </summary>
        /// <param name="RoomID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("Join")]
        public DataTable JoinRoom([FromBody]Room room)
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));
            command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));
            command.CommandText = "usp_JoinRoom";
            command.Connection = connection;
            connection.Open();
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            connection.Close();
            return table;
        }
        /// <summary>
        /// Gets the RoomID, RoomName, HostPlayerID, JoinedPlayerID, and both user ready statuses of a room
        /// </summary>
        /// <param name="RoomID"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("GetRoomInfo")]
        public DataTable GetRoomInfo([FromBody]Room room)
        {
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetRoomInfo";
            command.Connection = connection;
            connection.Open();
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            connection.Close();

            return table;
        }
    }
}