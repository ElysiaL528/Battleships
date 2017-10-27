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

        ShipNames ShipName { get; set; }
        ShipOrientations ShipOrientation { get; set; }
        int startX { get; set; }
        int startY { get; set; }
    }
}