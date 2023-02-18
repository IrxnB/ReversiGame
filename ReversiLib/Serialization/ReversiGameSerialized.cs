using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReversiLib.Serialization
{
    [Serializable]
    public class ReversiGameSerialized
    {
        internal GameCellSerialized[][] _field;
        internal bool _isFirstPlayerTurn;
        internal bool _isFirstTurn;

        public ReversiGameSerialized(ReversiGame game)
        {
            _field = new GameCellSerialized[game.Height][];
            for(int row = 0; row < game.Height; row++) 
            {
                _field[row] = new GameCellSerialized[game.Width];
                for(int column = 0; column < game.Width; column++)
                {
                    _field[row][column] = new GameCellSerialized(game.Field[row][column]);
                }
            }
            _isFirstPlayerTurn = game.IsFirstPlayerTurn;
            _isFirstTurn = game.IsFirstTurn;
        }
    }
}
