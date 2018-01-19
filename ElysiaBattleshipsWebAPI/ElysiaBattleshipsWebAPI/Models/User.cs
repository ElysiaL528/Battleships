using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElysiaBattleshipsWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public bool IsTestData { get; set; }

        public User(int id, string username, bool isTestData)
        {
            Id = id;
            Username = username;
            IsTestData = isTestData;
        }
    }
}