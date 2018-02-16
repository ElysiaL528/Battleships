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

        public ShipNames ShipType { get; set; }
        public ShipOrientations ShipOrientation { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public int ShipID { get; set; }
        public int ShipOrientationID { get; set; }

        public bool IsTestData { get; set; }

        public Ship(int shipID, int userID, int roomID, int x, int y, ShipNames shipType, ShipOrientations shipOrientation, bool isTestData)
        {
            ShipID = shipID;
            UserID = userID;
            RoomID = roomID;
            StartX = x;
            StartY = y;
            ShipType = shipType;
            IsTestData = isTestData;
            switch (ShipOrientation)
            {
                case ShipOrientations.Up:
                    ShipOrientationID = 1;
                    break;
                case ShipOrientations.Down:
                    ShipOrientationID = 2;
                    break;
                case ShipOrientations.Left:
                    ShipOrientationID = 3;
                    break;
                case ShipOrientations.Right:
                    ShipOrientationID = 4;
                    break;
            }
        }
    }
}