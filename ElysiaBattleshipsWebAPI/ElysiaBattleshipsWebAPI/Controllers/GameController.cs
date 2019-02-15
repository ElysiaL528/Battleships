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

    public class GameController : ApiController
    {
        #region connectionstring
        static readonly string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = qdc28p24 ";
        #endregion

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
        /// 
        [HttpPost]
        [Route("PlaceShip")]

        public string placeShip([FromBody]Ship ship)
        {
            string message = "";
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_PlaceShip";
                    command.Connection = connection;
                    command.Parameters.Add(new SqlParameter("ShipTypeID", ship.ShipTypeID));
                    command.Parameters.Add(new SqlParameter("UserID", ship.UserID));
                    command.Parameters.Add(new SqlParameter("RoomID", ship.RoomID));
                    command.Parameters.Add(new SqlParameter("X", ship.StartX));
                    command.Parameters.Add(new SqlParameter("Y", ship.StartY));
                    command.Parameters.Add(new SqlParameter("ShipOrientationID", ship.ShipOrientationID));

                    connection.Open();
                    message = command.ExecuteScalar().ToString();
                    connection.Close();
                }
            }

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


        public string ShotIsHit([FromBody]Shot shot)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_FireShot";

                    command.Parameters.Add(new SqlParameter("ShooterID", shot.UserID));
                    command.Parameters.Add(new SqlParameter("RoomID", shot.RoomID));
                    command.Parameters.Add(new SqlParameter("ShotX", shot.X));
                    command.Parameters.Add(new SqlParameter("ShotY", shot.Y));

                    connection.Open();
                    var result = command.ExecuteScalar().ToString();
                    connection.Close();

                    return result;
                }
            }
        }

        [HttpPost]
        [Route("GetShips")]


        public List<Ship> getShips([FromBody]Room room)
        {
            var shipsList = new List<Ship>();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_getShips";

                    command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));
                    command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));

                    var table = new DataTable();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);

                    foreach (DataRow playerShip in table.Rows)
                    {
                        int shipX = Convert.ToInt32(playerShip["X"]);
                        int shipY = Convert.ToInt32(playerShip["Y"]);

                        int shipOrientationID = Convert.ToInt32(playerShip["ShipOrientationID"]);
                        int shipTypeID = Convert.ToInt32(playerShip["ShipTypeID"]);

                        int hitCount = Convert.ToInt32(playerShip["HitCount"]);

                        Ship ship = new Ship(room.PlayerID, room.RoomID, shipX, shipY, shipTypeID, shipOrientationID);
                        shipsList.Add(ship);
                    }
                }
            }
            return shipsList;
        }

        /// <summary>
        /// Checks if/where the opponents attacked
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NewShots")]

        public List<int> checkForShots([FromBody]Room room)
        {
            var shotInfo = new List<int>();
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_CheckForNewShots";

                    command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));
                    command.Parameters.Add(new SqlParameter("LastShotID", room.LastShotID));

                    connection.Open();
                    var shot = command.ExecuteScalar();
                    connection.Close();

                    var table = new DataTable();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);

                    if (table.Rows.Count > 0)
                    {

                        int x = Convert.ToInt32(table.Rows[0]["X"]);
                        int y = Convert.ToInt32(table.Rows[0]["Y"]);
                        int lastShotID = Convert.ToInt32(table.Rows[0]["ShotID"]);

                        /*
                        bool newShots = true;

                        if (table.Rows.Count == 0)
                        {
                            newShots = false;
                        }

                        return newShots;
                        */

                        shotInfo.Add(x);
                        shotInfo.Add(y);
                        shotInfo.Add(lastShotID);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            return shotInfo;
        }

        [HttpPost]
        [Route("CheckReady")]

        public string checkPlayersReady([FromBody]Room room)
        {
            string result;
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_CheckUsersReady";

                    command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));
                    command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));

                    connection.Open();
                    result = command.ExecuteScalar().ToString();
                    connection.Close();
                }
            }
            return result;

        }

        [HttpPost]
        [Route("SetReady")]

        public DataTable setPlayerReady([FromBody]Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "usp_SetUserReady";

                    command.Parameters.Add(new SqlParameter("UserID", room.PlayerID));
                    command.Parameters.Add(new SqlParameter("RoomID", room.RoomID));

                    connection.Open();
                    var table = new DataTable();
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);
                    connection.Close();

                    return table;
                }
            }
        }

    }
}