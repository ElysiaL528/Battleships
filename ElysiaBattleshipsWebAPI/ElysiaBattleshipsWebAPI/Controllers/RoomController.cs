using ElysiaBattleshipsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ElysiaBattleshipsWebAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/Room")]
    public class RoomController : ApiController 
    {
            
        #region connectionstring
        static string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = qdc28p24";
        #endregion
        static SqlConnection connection = new SqlConnection(connectionstring);
        static SqlCommand command = new SqlCommand();

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="Username"></param>


        [HttpPost]
        [Route("Register")]
        public int CreateUser([FromBody]User user)
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateUser";
            command.Parameters.Add(new SqlParameter("Username", user.Username));
            command.Parameters.Add(new SqlParameter("IsTestData", user.IsTestData));
            command.Connection = connection;

            connection.Open();
            var result = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return result;
        }

        /// <summary>
        /// Creates a new room
        /// </summary>
        /// <param name="RoomName"></param>
        /// <param name="UserID"></param>
        /// 

        [HttpPost]
        [Route("CreateRoom")]
        public int CreateRoom([FromBody]Room room)
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CreateRoom";
            command.Parameters.Add(new SqlParameter("RoomName", room.RoomName));
            command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));
            command.Parameters.Add(new SqlParameter("IsTestData", room.IsTestData));
            command.Connection = connection;

            connection.Open();
            var result = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            return result;
        }

        /// <summary>
        /// Gets a list of available rooms
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetRooms")]
        public List<Room> GetRooms()
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetRooms";
            command.Connection = connection;
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            var roomsList = new List<Room>();

            foreach(DataRow room in table.Rows)
            {
                string roomName = room["RoomName"].ToString();
                string hostName = room["Username"].ToString();

                int hostID = Convert.ToInt32(room["HostPlayerID"]);
                int roomID = Convert.ToInt32(room["RoomID"]);

                Room newRoom = new Room(roomID, roomName, hostName, hostID, false);
                roomsList.Add(newRoom);
            }

            return roomsList;
        }

        /// <summary>
        /// Joins a room
        /// </summary>
        /// <param name="RoomID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("JoinRoom")]
        public DataTable JoinRoom([FromBody]Room room)
        {
            command.Parameters.Clear();
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));
            command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));
            command.CommandText = "usp_JoinRoom";
            command.Connection = connection;
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
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