using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MutualFriend.Models
{
    public class FriendModel
    {
        public int Id { get; set; }
        public int Person { get; set; }
        public string PersonName { get; set; }
        public int MutualCount { get; set; }
        public int Count { get; set; }
    }
}