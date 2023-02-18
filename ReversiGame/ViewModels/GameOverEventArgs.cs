using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.ViewModels
{
    internal class GameOverEventArgs : EventArgs
    {
        public int FirstScore;
        public int SecondScore;
        public GameOverEventArgs(int firstScore, int secondScore) 
        { 
            FirstScore= firstScore;
            SecondScore= secondScore;
        }
    }
}
