﻿using BlackJack.Data;
using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackJack.Controllers
{
    public class HomeController : Controller
    {
        private Manage manage = new Manage();
        static Random rnd = new Random();
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult NewRound()
        {
            var currentGame = manage.CachedGame();

            var fourCards = manage.GetStartingCards(rnd, currentGame.Deck.deck);
            currentGame.Player.Hand = new List<PlayingCards>();
            currentGame.Dealer.Hand = new List<PlayingCards>();

            currentGame.Player.Hand = fourCards.GetRange(0, 2);
            currentGame.Player.Points = manage.AddPoints(currentGame.Player.Hand);

            currentGame.Dealer.Hand = fourCards.GetRange(2, 2);
            currentGame.Dealer.Points = manage.AddPoints(currentGame.Dealer.Hand);
            currentGame.Bet = 0;
            currentGame.startOfGame = true;
            currentGame.Result = null;

            RoundOver(currentGame, true, false);

            return PartialView("Blackjack", currentGame);
        }

        public PartialViewResult BlackJack(string name)
        {            
            var currentGame = CreateGameCache(name);

            RoundOver(currentGame, true, false);

            manage.CacheGame(currentGame);

            return PartialView(currentGame);
        }

        private void GameOver(Game currentGame)
        {
            manage.MoneyLeft(currentGame);

        }

        private bool RoundOver(Game currentGame, bool firstRound, bool dealer)
        {
            if (dealer)
            {
                manage.UpdateHandAces(currentGame);
                if (!firstRound)
                {
                    if (currentGame.Dealer.Hand[1].Show == false)
                    {
                        currentGame.Dealer.Hand[1].Show = true;
                        currentGame.Dealer.Points = manage.AddPoints(currentGame.Dealer.Hand);
                    }
                }

                manage.DealerAlgorithm(rnd, currentGame);
            }

            if (!dealer)
            {
                manage.UpdateHandAces(currentGame);
            }

            GameResult gameResult = null;
            if (!firstRound)
            {
                gameResult = manage.CheckIfBust(currentGame);
                if (currentGame.Dealer.Hand[1].Show == false)
                {
                    currentGame.Dealer.Hand[1].Show = true;
                    currentGame.Dealer.Points = manage.AddPoints(currentGame.Dealer.Hand);
                }
            }

            if (dealer && gameResult == null)
            {
                gameResult = manage.CompareDealerAndPlayer(currentGame);
            }

            var roundOver = false;
            if (gameResult != null)
            {
                currentGame.Result = gameResult;

                roundOver = manage.ManageWinOrLoseOrTie(currentGame);
            }

            return roundOver;
        }

        private Game CreateGameCache(string name)
        {               
            var currentGame = new Game(name, rnd, true);
            manage.CacheGame(currentGame);

            return currentGame;
        }
        
        public PartialViewResult StartGame(int bet)
        {
            var currentGame = manage.CachedGame();            

            currentGame.startOfGame = false;
            currentGame.Bet = bet;

            manage.CacheGame(currentGame);

            return PartialView("BlackJack", currentGame);
        }

        public PartialViewResult Stand()
        {
            var currentGame = manage.CachedGame();

            var roundOver = RoundOver(currentGame, false, true);

            GameOver(currentGame);

            manage.CacheGame(currentGame);

            return PartialView("BlackJack", currentGame);
        }

        public PartialViewResult Surrender()
        {
            var currentGame = manage.CachedGame();

            currentGame.Result = new GameResult();
            currentGame.Result.DealerWin = true;

            int moneyBack = 0;
            if (currentGame.Dealer.Hand[1].Show == true)
            {
                moneyBack = currentGame.Bet / 3;
            }
            
            moneyBack = currentGame.Bet / 2;
            currentGame.Bet = 0;
            currentGame.Player.Money += moneyBack;

            manage.CacheGame(currentGame);
            return PartialView("BlackJack", currentGame);
        }

        public PartialViewResult Hit()
        {
            var currentGame = manage.CachedGame();
            
            manage.AddCard(rnd, currentGame, "player");

            var roundOver = RoundOver(currentGame, false, false);

            GameOver(currentGame);

            manage.CacheGame(currentGame);

            return PartialView("BlackJack", currentGame);
        }        

        [HttpPost]
        public ActionResult AddBet(int amount)
        {
            var currentGame = manage.CachedGame();

            if (currentGame != null)
            {                
                if (currentGame.Player.Money >= amount)
                {
                    currentGame.Bet += amount;
                    currentGame.Player.Money -= amount;
                }
            }

            manage.CacheGame(currentGame);
            return PartialView("BlackJack", currentGame);
        }

        [HttpPost]
        public ActionResult ClearBet()
        {
            var currentGame = manage.CachedGame();
            if (currentGame != null)
            {
                int betback = currentGame.Bet;
                currentGame.Bet = 0;
                currentGame.Player.Money += betback;
            }

            manage.CacheGame(currentGame);
            return PartialView("BlackJack", currentGame);
        }
    }
}