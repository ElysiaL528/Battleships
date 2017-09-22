using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Controllers
{
    public class GameController
    {
        
        #region connectionstring
        static string connectionstring = "Server = GMRSKYBASE; Database = ElysiaLopezBattleships2017; User id = ElysiaLopez; Password = pleasant1 ";
        #endregion
        static SqlConnection connection = new SqlConnection(connectionstring);

        public enum ShipNames
        {
            AircraftCarrier = 1,
            Battleship = 2, 
            Cruiser = 3,
            Submarine = 4,
            Destroyer = 5
        }

        public bool placeShip(int roomID, int userID, int startX, int startY, ShipNames shipName, int orientation)
        {
            bool isValid = true;

            SqlCommand command = new SqlCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetShips";
            command.Parameters.Add(new SqlParameter("RoomID", roomID));
            command.Parameters.Add(new SqlParameter("UserID", userID));

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            for (int i = 1; i < table.Rows.Count + 1; i++)
            {
                int x = Convert.ToInt32(table.Rows[i]["X"]);
                int y = Convert.ToInt32(table.Rows[i]["Y"]);
                int shipLength = Convert.ToInt32(table.Rows[i]["ShipLength"]);
                int orientationValueX = Convert.ToInt32(table.Rows[i]["OrientationValueX"]);
                int orientationValueY = Convert.ToInt32(table.Rows[i]["OrientationValueY"]);

                for (int j = 0; j < shipLength + 1; j++)
                {
                    if (startX >= x && startX <= x + shipLength - 1 && startY >= y && startY - 1 <= y + shipLength - 1)
                    {
                        isValid = false;
                    }
                    else if(startX <= x && startX > x - shipLength + 1 && startY < y && startY > y - shipLength + 1)
                    {
                        isValid = false;
                    }
                }
            }
            if(!isValid)
            {
                return false;
            }
            command.CommandText = "usp_PlaceShip";
            command.Parameters.Add(new SqlParameter("ShipID", shipName));
            command.Parameters.Add(new SqlParameter("UserID", userID));
            command.Parameters.Add(new SqlParameter("RoomID", roomID));
            command.Parameters.Add(new SqlParameter("X", startX));
            command.Parameters.Add(new SqlParameter("Y", startY));
            command.Parameters.Add(new SqlParameter("ShipOrientationID", orientation));

            connection.Open();
            command.ExecuteScalar();
            connection.Close();
            return true;
        }

        public bool isHit(int userID, int roomID, int inputX, int inputY)
        {

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "usp_GetShips";
            command.Parameters.Add(new SqlParameter("RoomID", roomID));
            command.Parameters.Add(new SqlParameter("UserID", userID));

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(table);

            for (int i = 0; i < table.Rows.Count; i++)
            {
                int shipX = Convert.ToInt32(table.Rows[i]["X"]);
                int shipY = Convert.ToInt32(table.Rows[i]["Y"]);
                int shipLength = Convert.ToInt32(table.Rows[i]["ShipLength"]);
                int orientationValueX = Convert.ToInt32(table.Rows[i]["OrientationValueX"]);
                int orientationValueY = Convert.ToInt32(table.Rows[i]["OrientationValueY"]);

                if (orientationValueX == 1)
                {
                    if (inputX >= shipX && inputX <= shipX + shipLength)
                    {
                        return true;
                    }
                }
                else if (orientationValueX == -1)
                {
                    if(inputX <= shipX && inputX >= shipX - shipLength)
                    {
                        return true;
                    }
                }
                else if(orientationValueY == -1)
                {
                    if(inputY >= shipY && inputY <= shipY + shipLength)
                    {
                        return true;
                    }
                }
                else if(orientationValueY == 1)
                {
                    if(inputY <= shipY && inputY >= shipY - shipLength)
                    {
                        return true;
                    }
                }
            }
            return false;
        }



    }
}