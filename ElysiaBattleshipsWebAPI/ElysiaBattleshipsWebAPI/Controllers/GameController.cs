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
    [RoutePrefix("api/Game")]

    public class GameController :   ApiController 
    {
        
        #region connectionstring
        static string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = pleasant1 ";
        #endregion
        static SqlConnection connection = new SqlConnection(connectionstring);

        /// <summary>
        /// An enum of the five ship types
        /// </summary>
        public enum ShipTypes
        {
            AircraftCarrier,
            Battleship,
            Cruiser,
            Submarine,
            Destroyer
        }

        /// <summary>
        /// Returns a bool indicating whether or not the ship was placed.
        /// </summary>
        /// <param name="roomID"></param>
        /// <param name="userID"></param>
        /// <param name="startX"></param>
        /// <param name="startY"></param>
        /// <param name="shipType"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("PlaceShip")]

        public DataTable placeShip(int roomID, int userID, int startX, int startY, ShipTypes shipType, int orientation)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_PlaceShip";
            command.Connection = connection;
            command.Parameters.Add(new SqlParameter("ShipID", shipType));
            command.Parameters.Add(new SqlParameter("UserID", userID));
            command.Parameters.Add(new SqlParameter("RoomID", roomID));
            command.Parameters.Add(new SqlParameter("X", startX));
            command.Parameters.Add(new SqlParameter("Y", startY));
            command.Parameters.Add(new SqlParameter("ShipOrientationID", orientation));

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roomID"></param>
        /// <param name="inputX"></param>
        /// <param name="inputY"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("Shoot")]
        

        public int shotIsHit(int userID, int roomID, int inputX, int inputY)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_FireShot";

            command.Parameters.Add(new SqlParameter("UserID", userID));
            command.Parameters.Add(new SqlParameter("RoomID", roomID));
            command.Parameters.Add(new SqlParameter("ShotX", inputX));
            command.Parameters.Add(new SqlParameter("ShotY", inputY));

            connection.Open();
            var result = command.ExecuteScalar();
            connection.Close();

            return (int)result;
        }

        [HttpPost]
        [Route("GetShips")]


        public DataTable getShips([FromBody]Room room)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_getShips";

            command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));
            command.Parameters.Add(new SqlParameter("RoomID", room.PlayerID));

            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;
        }

        public DataTable checkForShots([FromBody]Room room)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CheckForNewShots";

            command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));
            command.Parameters.Add(new SqlParameter("LastShotID", room.LastShotID));

            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;
        }

        public DataTable checkPlayersReady([FromBody]Room room)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_CheckUsersReady";

            command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));

            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;

        }

        public DataTable setPlayerReady([FromBody]Room room)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_SetUserReady";

            command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));
            command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));

            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;
        }

    }
}