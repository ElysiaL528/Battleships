using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Controllers
{
    public class UserController
    {
        
        #region connectionstring
        static string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = pleasant1 ";
        #endregion
        static SqlConnection connection = new SqlConnection(connectionstring);

        public enum shipOrientations
        {
            Up = -1,
            Down = 1,
            Left = -1, 
            Right = 1
        }
        public enum ShipNames
        {
            AircraftCarrier = 1,
            Battleship = 2, 
            Cruiser = 3,
            Submarine = 4,
            Destroyer = 5
        }

        public bool canPlaceShip(int roomID, int userID, int startX, int startY, ShipNames shipName, int orientation)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetShips";
            command.Parameters.Add(new SqlParameter("RoomID", roomID));
            command.Parameters.Add(new SqlParameter("UserID", userID));

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            for(int i = 0; i < table.Rows.Count; i++)
            {
                if(startX > Convert.ToInt32(table.Rows[i]["X"]) && )
                {

                }
            }

        }



    }
}