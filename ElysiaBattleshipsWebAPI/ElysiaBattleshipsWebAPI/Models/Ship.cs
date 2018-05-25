using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Models
{
    public class Ship
    {
        public enum ShipNames
        {
            AircraftCarrier = 1,
            Battleship = 2,
            Cruiser = 3,
            Submarine = 4,
            Destroyer = 5
        }

        public enum ShipOrientations
        {
            Up,
            Down,
            Left,
            Right
        }

        public int ShipTypeID { get; set; }
        public ShipOrientations ShipOrientation { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public int ShipID { get; set; }
        public int ShipOrientationID { get; set; }
        public int ShipOrientationValueX { get; set; }
        public int ShipOrientationValueY { get; set; }

        public int ShipLength { get; set; }

        public bool IsTestData { get; set; }

        public Ship(int userID, int roomID, int x, int y, int shipTypeID, int shipOrientationID)
        {
            UserID = userID;
            RoomID = roomID;
            StartX = x;
            StartY = y;

            ShipTypeID = shipTypeID;
            ShipOrientationID = shipOrientationID;

            switch (ShipTypeID)
            {
                case 1:
                    ShipLength = 5;
                    break;
                case 2:
                    ShipLength = 4;
                    break;
                case 3:
                    ShipLength = 3;
                    break;
                case 4:
                    ShipLength = 3;
                    break;
                case 5:
                    ShipLength = 2;
                    break;
            }

        }
    }
}