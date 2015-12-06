using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boardgames.Models
{
    public class Game
    {
        public long GameID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public long Count { get; set; }
    }
}