using BlackJack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Game
    {
        private Manage manage = new Manage();
        public Deck Deck { get; set; }
        public Player Player { get; set; }
        public Dealer Dealer { get; set; }
        public int Bet { get; set; }
        public bool startOfGame { get; set; }

        public GameResult Result { get; set; }

        public bool Lost { get; set; }


        public Game(string name, Random rnd, bool startofGame)
        {
            Deck = new Deck();
            var fourCards = manage.GetStartingCards(rnd, Deck.deck);
            Player = new Player(name, rnd, fourCards.GetRange(0, 2));
            
            Dealer = new Dealer(rnd, fourCards.GetRange(2, 2));
            Bet = 0;
            startOfGame = startofGame;
            Lost = false;
        }             
    }
}