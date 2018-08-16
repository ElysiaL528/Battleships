using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Models
{
    public class Room
    {
        public string RoomName { get; set; }
        public string PlayerID { get; set; }

        public int RoomID { get; set; }
    }
}