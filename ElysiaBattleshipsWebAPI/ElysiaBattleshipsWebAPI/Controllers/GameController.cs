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
    [RoutePrefix("api/Game")]

    public class GameController :   ApiController
    {
        
        #region connectionstring
        static string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = qdc28p24 ";
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
        /// <param name="ship"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("PlaceShip")]

        public string placeShip([FromBody]Ship ship)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_PlaceShip";
            command.Connection = connection;
            command.Parameters.Add(new SqlParameter("ShipTypeID", ship.ShipTypeID));
            command.Parameters.Add(new SqlParameter("UserID", ship.UserID));
            command.Parameters.Add(new SqlParameter("RoomID", ship.RoomID));
            command.Parameters.Add(new SqlParameter("X", ship.StartX));
            command.Parameters.Add(new SqlParameter("Y", ship.StartY));
            command.Parameters.Add(new SqlParameter("ShipOrientationID", ship.ShipOrientationID));

            string message = "";
            connection.Open();
            message = command.ExecuteScalar().ToString();
            connection.Close();

            return message;
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="shot"></param>
        /// <returns></returns>
        /// 

        [HttpPost]
        [Route("Shoot")]
        

        public string shotIsHit([FromBody]Shot shot)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_FireShot";

            command.Parameters.Add(new SqlParameter("UserID", shot.UserID));
            command.Parameters.Add(new SqlParameter("RoomID", shot.RoomID));
            command.Parameters.Add(new SqlParameter("ShotX", shot.X));
            command.Parameters.Add(new SqlParameter("ShotY", shot.Y));

            connection.Open();
            var result = command.ExecuteScalar().ToString();
            connection.Close();

            return result;
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
            command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));

            var table = new DataTable();
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            return table;
        }

        [HttpPost]
        [Route("NewShots")]

        public bool checkForShots([FromBody]Room room)
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

            bool newShots = true;

            if (table.Rows.Count == 0)
            {
                newShots = false;
            }

            return newShots;
        }

        [HttpPost]
        [Route("CheckReady")]

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

        [HttpPost]
        [Route("SetReady")]

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