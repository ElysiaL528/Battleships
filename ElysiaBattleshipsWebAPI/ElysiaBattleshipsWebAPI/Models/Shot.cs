using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Models
{
    public class Shot
    {
        public int ShotID { get; }
        public int X { get; set; }
        public int Y { get; set; }
        public int UserID { get; set; }
        public int RoomID { get; set; }
        public bool IsTestData { get; set; }

        public Shot(int x, int y, int shotID, int userID, int roomID, bool isTestData)
        {
            X = x;
            Y = y;
            ShotID = shotID;
            UserID = userID;
            RoomID = roomID;
            IsTestData = isTestData;
        }
    }
}