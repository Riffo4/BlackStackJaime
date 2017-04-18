using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Data
{
    public class Manage
    {    
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

        // GameRules

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

            var cardManage = new CardManage();
            cardManage.GetRandomCard(rnd, currentGame, CardsOnTable, playerOrDealer);

            if (playerOrDealer == "player")
            {
                currentGame.Player.Points = AddPoints(currentGame.Player.Hand);
            }
            else
            {
                currentGame.Dealer.Points = AddPoints(currentGame.Dealer.Hand);
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
    }
}