using BlackJack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{   
    public class Player
    {
        public int Money { get; set; }
        public List<PlayingCards> Hand { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        private Manage manage = new Manage();

        public Player(string name, Random rnd, List<PlayingCards> startingCards)
        {
            Money = 1000;
            Name = name;
            Hand = startingCards;
            Points = manage.AddPoints(startingCards);
        }
    } 
}