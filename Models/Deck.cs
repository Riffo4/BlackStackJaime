using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class Deck
    {
        public List<PlayingCards> deck { get; set; }

        public Deck()
        {
            GetDeck();
        }

        private void GetDeck()
        {
            List<PlayingCards> _deck = new List<PlayingCards>() {
                new PlayingCards { Card = "Two", Points = 2, Picture = "/Content/Cards/2_of_hearts.png", Show = true, Id = 1 },
                new PlayingCards { Card = "Three", Points = 3, Picture = "/Content/Cards/3_of_hearts.png", Show = true, Id = 2 },
                new PlayingCards { Card = "Four", Points = 4, Picture = "/Content/Cards/4_of_hearts.png", Show = true, Id = 3 },
                new PlayingCards { Card = "Five", Points = 5, Picture = "/Content/Cards/5_of_hearts.png", Show = true, Id = 4 },
                new PlayingCards { Card = "Six", Points = 6, Picture = "/Content/Cards/6_of_hearts.png", Show = true, Id = 5 },
                new PlayingCards { Card = "Seven", Points = 7, Picture = "/Content/Cards/7_of_hearts.png", Show = true, Id = 6 },
                new PlayingCards { Card = "Eight", Points = 8, Picture = "/Content/Cards/8_of_hearts.png", Show = true, Id = 7 },
                new PlayingCards { Card = "Nine", Points = 9, Picture = "/Content/Cards/9_of_hearts.png", Show = true, Id = 8 },
                new PlayingCards { Card = "Ten", Points = 10, Picture = "/Content/Cards/10_of_hearts.png", Show = true, Id = 9 },
                new PlayingCards { Card = "Jack", Points = 10, Picture = "/Content/Cards/jack_of_hearts.png", Show = true, Id = 10 },
                new PlayingCards { Card = "Queen", Points = 10, Picture = "/Content/Cards/queen_of_hearts.png", Show = true, Id = 11 },
                new PlayingCards { Card = "King", Points = 10, Picture = "/Content/Cards/king_of_hearts.png", Show = true, Id = 12 },
                new PlayingCards { Card = "Ace", Points = 11, Picture = "/Content/Cards/ace_of_hearts.png", Show = true, Id = 13 },

                new PlayingCards { Card = "Two", Points = 2, Picture = "/Content/Cards/2_of_clubs.png", Show = true, Id = 14 },
                new PlayingCards { Card = "Three", Points = 3, Picture = "/Content/Cards/3_of_clubs.png", Show = true, Id = 15 },
                new PlayingCards { Card = "Four", Points = 4, Picture = "/Content/Cards/4_of_clubs.png", Show = true, Id = 16 },
                new PlayingCards { Card = "Five", Points = 5, Picture = "/Content/Cards/5_of_clubs.png", Show = true, Id = 17 },
                new PlayingCards { Card = "Six", Points = 6, Picture = "/Content/Cards/6_of_clubs.png", Show = true, Id = 18 },
                new PlayingCards { Card = "Seven", Points = 7, Picture = "/Content/Cards/7_of_clubs.png", Show = true, Id = 19 },
                new PlayingCards { Card = "Eight", Points = 8, Picture = "/Content/Cards/8_of_clubs.png", Show = true, Id = 20 },
                new PlayingCards { Card = "Nine", Points = 9, Picture = "/Content/Cards/9_of_clubs.png", Show = true, Id = 21 },
                new PlayingCards { Card = "Ten", Points = 10, Picture = "/Content/Cards/10_of_clubs.png", Show = true, Id = 22 },
                new PlayingCards { Card = "Jack", Points = 10, Picture = "/Content/Cards/jack_of_clubs.png", Show = true, Id = 23 },
                new PlayingCards { Card = "Queen", Points = 10, Picture = "/Content/Cards/queen_of_clubs.png", Show = true, Id = 24 },
                new PlayingCards { Card = "King", Points = 10, Picture = "/Content/Cards/king_of_clubs.png", Show = true, Id = 25 },
                new PlayingCards { Card = "Ace", Points = 11, Picture = "/Content/Cards/ace_of_clubs.png", Show = true, Id = 26 },

                new PlayingCards { Card = "Two", Points = 2, Picture = "/Content/Cards/2_of_diamonds.png", Show = true, Id = 26 },
                new PlayingCards { Card = "Three", Points = 3, Picture = "/Content/Cards/3_of_diamonds.png", Show = true, Id = 28 },
                new PlayingCards { Card = "Four", Points = 4, Picture = "/Content/Cards/4_of_diamonds.png", Show = true, Id = 29 },
                new PlayingCards { Card = "Five", Points = 5, Picture = "/Content/Cards/5_of_diamonds.png", Show = true, Id = 30},
                new PlayingCards { Card = "Six", Points = 6, Picture = "/Content/Cards/6_of_diamonds.png", Show = true, Id = 31 },
                new PlayingCards { Card = "Seven", Points = 7, Picture = "/Content/Cards/7_of_diamonds.png", Show = true, Id = 32 },
                new PlayingCards { Card = "Eight", Points = 8, Picture = "/Content/Cards/8_of_diamonds.png", Show = true, Id = 33 },
                new PlayingCards { Card = "Nine", Points = 9, Picture = "/Content/Cards/9_of_diamonds.png", Show = true, Id = 34 },
                new PlayingCards { Card = "Ten", Points = 10, Picture = "/Content/Cards/10_of_diamonds.png", Show = true, Id = 35 },
                new PlayingCards { Card = "Jack", Points = 10, Picture = "/Content/Cards/jack_of_diamonds.png", Show = true, Id = 36 },
                new PlayingCards { Card = "Queen", Points = 10, Picture = "/Content/Cards/queen_of_diamonds.png", Show = true, Id = 37 },
                new PlayingCards { Card = "King", Points = 10, Picture = "/Content/Cards/king_of_diamonds.png", Show = true, Id = 38 },
                new PlayingCards { Card = "Ace", Points = 11, Picture = "/Content/Cards/ace_of_diamonds.png", Show = true, Id = 39 },

                new PlayingCards { Card = "Two", Points = 2, Picture = "/Content/Cards/2_of_spades.png", Show = true, Id = 40 },
                new PlayingCards { Card = "Three", Points = 3, Picture = "/Content/Cards/3_of_spades.png", Show = true, Id = 41 },
                new PlayingCards { Card = "Four", Points = 4, Picture = "/Content/Cards/4_of_spades.png", Show = true, Id = 42 },
                new PlayingCards { Card = "Five", Points = 5, Picture = "/Content/Cards/5_of_spades.png", Show = true, Id = 43 },
                new PlayingCards { Card = "Six", Points = 6, Picture = "/Content/Cards/6_of_spades.png", Show = true, Id = 44 },
                new PlayingCards { Card = "Seven", Points = 7, Picture = "/Content/Cards/7_of_spades.png", Show = true, Id = 45 },
                new PlayingCards { Card = "Eight", Points = 8, Picture = "/Content/Cards/8_of_spades.png", Show = true, Id = 46 },
                new PlayingCards { Card = "Nine", Points = 9, Picture = "/Content/Cards/9_of_spades.png", Show = true, Id = 47 },
                new PlayingCards { Card = "Ten", Points = 10, Picture = "/Content/Cards/10_of_spades.png", Show = true, Id = 48 },
                new PlayingCards { Card = "Jack", Points = 10, Picture = "/Content/Cards/jack_of_spades.png", Show = true, Id = 49 },
                new PlayingCards { Card = "Queen", Points = 10, Picture = "/Content/Cards/queen_of_spades.png", Show = true, Id = 50 },
                new PlayingCards { Card = "King", Points = 10, Picture = "/Content/Cards/king_of_spades.png", Show = true, Id = 51 },
                new PlayingCards { Card = "Ace", Points = 11, Picture = "/Content/Cards/ace_of_spades.png", Show = true, Id = 52 }
            };

            deck = _deck;
        }
    }
}