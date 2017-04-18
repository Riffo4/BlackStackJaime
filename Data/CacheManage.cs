using BlackJack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Data
{
    public class CacheManage
    {
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
    }
}