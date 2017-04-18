using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Data
{
    public class CardManage
    {
        public List<PlayingCards> GetStartingCards(Random rnd, List<PlayingCards> deck)
        {
            var fourCards = new HashSet<PlayingCards>();
            List<int> cardsUsed = new List<int>();

            for (int i = 0; i < 4; i++)
            {
                GetRandomCard(rnd, deck, fourCards);
            }

            var fourCardsList = new List<PlayingCards>();
            fourCardsList = fourCards.ToList();

            return fourCardsList;
        }
               

        public void GetRandomCard(Random rnd, Game currentGame, HashSet<PlayingCards> cardsOntable, string playerOrDealer)
        {
            bool tryAgain = true;
            while (tryAgain)
            {
                int r = rnd.Next(currentGame.Deck.deck.Count());

                var newInstanceOftheCard = new PlayingCards() { Card = currentGame.Deck.deck[r].Card, Points = currentGame.Deck.deck[r].Points, Show = currentGame.Deck.deck[r].Show, Picture = currentGame.Deck.deck[r].Picture, Id = currentGame.Deck.deck[r].Id };

                var didNotExist = cardsOntable.Add(newInstanceOftheCard);

                if (didNotExist == false)
                {
                    tryAgain = true;
                }
                else
                {
                    if (playerOrDealer == "player")
                    {
                        currentGame.Player.Hand.Add(newInstanceOftheCard);
                    }
                    else
                    {
                        currentGame.Dealer.Hand.Add(newInstanceOftheCard);
                    }
                    tryAgain = false;
                }
            }
        }

        public void GetRandomCard(Random rnd, List<PlayingCards> deck, HashSet<PlayingCards> fourCards)
        {
            bool tryAgain = true;
            while (tryAgain)
            {
                int r = rnd.Next(deck.Count());

                var newInstanceOftheCard = new PlayingCards() { Card = deck[r].Card, Points = deck[r].Points, Show = deck[r].Show, Picture = deck[r].Picture, Id = deck[r].Id };

                var fittingname = fourCards.Add(newInstanceOftheCard);

                if (fittingname == false)
                {
                    tryAgain = true;
                }
                else
                {
                    tryAgain = false;
                }
            }
        }
    }
}