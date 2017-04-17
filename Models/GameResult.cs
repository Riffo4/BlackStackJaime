using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlackJack.Models
{
    public class GameResult
    {
        public bool PlayerWin { get; set; }
        public bool PlayerBust { get; set; }
        public bool DealerWin { get; set; }
        public bool DealerBust { get; set; }
        public bool RoundOver { get; set; }
        public bool Tie { get; set; }
        public bool Surrender { get; set; }

        public int Win { get; set; }

        public GameResult()
        {
            PlayerWin = false;
            PlayerBust = false;
            DealerWin = false;
            DealerBust = false;
            RoundOver = false;
            Tie = false;
            Surrender = false;
        }
    }
}