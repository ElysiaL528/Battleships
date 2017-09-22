using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Controllers
{
    public class RoomController
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

        public void CreateUser(string Username)
        {
            command.Parameters.Clear();
            command.CommandText = "usp_CreateUser";
            command.Parameters.Add(new SqlParameter("Username", Username));
            connection.Open();
            command.ExecuteScalar();
            connection.Close();

        }

        /// <summary>
        /// Creates a new room
        /// </summary>
        /// <param name="RoomName"></param>
        /// <param name="UserID"></param>
        public void CreateRoom(string RoomName, int UserID)
        {
            command.Parameters.Clear();
            command.CommandText = "usp_CreateRoom";
            command.Parameters.Add(new SqlParameter("RoomName", RoomName));
            command.Parameters.Add(new SqlParameter("UserID", UserID));
            connection.Open();
            command.ExecuteScalar();
            connection.Close();
        }

        /// <summary>
        /// Gets a list of available rooms
        /// </summary>
        /// <returns></returns>

        public DataTable GetRooms()
        {
            command.Parameters.Clear();
            command.CommandText = "usp_GetRooms";
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
        public DataTable JoinRoom(int RoomID, int UserID)
        {
            command.Parameters.Clear();
            command.CommandText = "usp_JoinRoom";
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
        public DataTable GetRoomInfo(int RoomID)
        {
            command.Parameters.Clear();
            command.Parameters.Add(new SqlParameter("RoomID", RoomID));
            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;
        }
    }
}