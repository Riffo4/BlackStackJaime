using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class PlayingCards
    {
        public string Card { get; set; }
        public int Points { get; set; }
        public string Picture { get; set; }
        public bool Show { get; set; }

        public int Id { get; set; }
    }
}