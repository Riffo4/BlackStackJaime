using BlackJack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Dealer
    {
        public List<PlayingCards> Hand { get; set; }
        public int Points { get; set; }
        private Manage manage = new Manage();

        public Dealer(Random rnd, List<PlayingCards> startingCards)
        {
            startingCards[1].Show = false;
            Hand = startingCards;
            Points = manage.AddPoints(startingCards);
        }
    }
}