using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Models
{
    public class Room
    {   
        public string RoomName { get; set; }
        public int HostPlayerID { get; set; }
        public int JoinedPlayerID { get; set; }
        public int RoomID { get; set; }
        public int LastShotID { get; set; }
        public int PlayerID { get; set; }
        public string HostPlayerName { get; set; }
        public bool IsTestData { get; set; }

        public Room(int roomID, string roomName, string hostPlayerName, int playerID)
        {
            RoomName = roomName;
            PlayerID = playerID;
            HostPlayerName = hostPlayerName;
            RoomID = roomID;
        }
    }
}