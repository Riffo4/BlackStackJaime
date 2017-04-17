using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Data
{
    public class Manage
    {
        List<string> Icons { get; set; }

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

        public void AddCard(Random rnd, Game currentGame, string playerOrDealer)
        {
            var CardsOnTable = new HashSet<PlayingCards>();

            foreach (var item in currentGame.Dealer.Hand)
            {
                CardsOnTable.Add(item);
            }

            foreach (var item in currentGame.Player.Hand)
            {
                CardsOnTable.Add(item);
            }

            GetRandomCard(rnd, currentGame, CardsOnTable, playerOrDealer);

            if (playerOrDealer == "player")
            {
                currentGame.Player.Points = AddPoints(currentGame.Player.Hand);
            }
            else
            {
                currentGame.Dealer.Points = AddPoints(currentGame.Dealer.Hand);
            }
        }

        public GameResult CheckIfBust(Game currentGame)
        { 
            if (currentGame.Player.Points > 21)
            { 
                if (currentGame.Result == null)
                {
                    currentGame.Result = new GameResult();
                }
                currentGame.Result.PlayerBust = true;
                currentGame.Result.DealerWin = true;
                currentGame.Result.RoundOver = true;
            }

            if (currentGame.Dealer.Points > 21)
            {
                if (currentGame.Result == null)
                {
                    currentGame.Result = new GameResult();
                }
                currentGame.Result.DealerBust = true;
                currentGame.Result.PlayerWin = true;
                currentGame.Result.RoundOver = true;
            }

            if (currentGame.Result != null)
            {
                return currentGame.Result;
            }

            return null;
        }

        public GameResult CompareDealerAndPlayer(Game currentGame)
        {
            if (currentGame.Dealer.Points < 20 && currentGame.Dealer.Points > 16 && currentGame.Dealer.Points == currentGame.Player.Points)
            {
                if (currentGame.Result == null)
                {
                    currentGame.Result = new GameResult();
                }
                currentGame.Result.DealerWin = true;
                currentGame.Result.RoundOver = true;
            }
            else if (currentGame.Dealer.Points < 22 && currentGame.Dealer.Points > 19 && currentGame.Dealer.Points == currentGame.Player.Points)
            {
                if (currentGame.Result == null)
                {
                    currentGame.Result = new GameResult();
                }

                currentGame.Result.Tie = true;
                currentGame.Result.RoundOver = true;
            }
            else if (currentGame.Player.Points > currentGame.Dealer.Points)
            {
                if (currentGame.Result == null)
                {
                    currentGame.Result = new GameResult();
                }
                currentGame.Result.PlayerWin = true;                
                currentGame.Result.RoundOver = true;
            }
            else if(currentGame.Player.Points < currentGame.Dealer.Points)
            {
                if (currentGame.Result == null)
                {
                    currentGame.Result = new GameResult();
                }
                currentGame.Result.DealerWin = true;
                currentGame.Result.RoundOver = true;
            }

            if (currentGame.Result != null)
            {
                return currentGame.Result;
            }

            return null;
        }

        public void MoneyLeft(Game currentGame)
        {
            if (currentGame.Player.Money < 1 && currentGame.Result != null)
            {
                if (!currentGame.Result.PlayerWin)
                {
                    currentGame.Lost = true;
                }
            }
        }

        public bool ManageWinOrLoseOrTie(Game currentGame)
        {
            var result = currentGame.Result;
            if (result.RoundOver)
            {
                if (result.Tie)
                {
                    int moneyBack = currentGame.Bet;
                    currentGame.Player.Money += moneyBack;
                    currentGame.Result.Win = new int();
                    currentGame.Result.Win = 0;
                }
                else if (result.DealerWin)
                {
                    currentGame.Bet = 0;
                }
                else if (result.PlayerWin)
                {
                    int moneyWon = currentGame.Bet * 2;
                    currentGame.Player.Money += (currentGame.Bet + moneyWon);
                    currentGame.Result.Win = moneyWon;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void DealerAlgorithm(Random rnd, Game currentGame)
        {
            if (currentGame.Dealer.Points < 17)
            {
                var at17Yet = true;
                while (at17Yet)
                {
                    AddCard(rnd, currentGame, "dealer");

                    UpdateHandAces(currentGame);

                    if (currentGame.Dealer.Points > 16)
                    {
                        at17Yet = false;
                    }
                }
            }
        }

        public void UpdateHandAces(Game currentGame)
        {
            // Player
            if (currentGame.Player.Hand.Select(x => x.Card).Contains("Ace"))
            {
                if (currentGame.Player.Points > 21)
                {
                    int howManyAces = currentGame.Player.Hand.Where(x => x.Card == "Ace").Count();

                    for (int i = 0; i < howManyAces; i++)
                    {
                        if (currentGame.Player.Points > 21)
                        {
                            var handsWithAcesList = currentGame.Player.Hand.Where(x => x.Card == "Ace").ToList();

                            if (handsWithAcesList[i].Points == 1)
                            {
                                handsWithAcesList[i].Points = 1;

                                currentGame.Player.Points = AddPoints(currentGame.Player.Hand);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            // Dealer
            if (currentGame.Dealer.Hand.Select(x => x.Card).Contains("Ace"))
            {
                if (currentGame.Dealer.Points > 21)
                {
                    int howManyAces = currentGame.Dealer.Hand.Where(x => x.Card == "Ace").Count();

                    for (int i = 0; i < howManyAces; i++)
                    {
                        if (currentGame.Dealer.Points > 21)
                        {
                            var handsWithAcesList = currentGame.Dealer.Hand.Where(x => x.Card == "Ace").ToList();

                            handsWithAcesList[i].Points = 1;

                            currentGame.Dealer.Points = AddPoints(currentGame.Dealer.Hand);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        public void CacheGame(Game currentGame)
        {
            var existingGame = (HttpContext.Current.Cache["CurrentGame"] as Game);
            if (existingGame != null)
            {
                ClearList();
            }
            HttpContext.Current.Cache.Insert("CurrentGame", currentGame);
        }

        public Game CachedGame()
        {
            return (HttpContext.Current.Cache["CurrentGame"] as Game);
        }

        public void ClearList()
        {
            HttpContext.Current.Cache.Remove("CurrentGame");
        }

        public int AddPoints(List<PlayingCards> cards)
        {
            int totalPoints = 0;
            foreach (var card in cards)
            {
                if (card.Show)
                {
                    totalPoints += card.Points;
                }
            }

            return totalPoints;
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
                // Setup för test
                //int first = 12;
                //int second = 25;
                //int third = 38;
                //int forth = 4;

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