using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiLib.Serialization
{
    [Serializable]
    public class GameCellSerialized
    {
        internal CellState _state;

        public GameCellSerialized(ReversiCell cell)
        {
            _state = cell.State;
        }
    }
}
