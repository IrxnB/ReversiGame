using ReversiLib.Serialization;
using System;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Windows.Media.Media3D;
using System.Xaml;

namespace ReversiLib
{
    [Serializable]
    public class ReversiGame : INotifyPropertyChanged
    {
        private ReversiCell[][] _field;
        private bool _isFirstPlayerTurn = true;
        private bool _isFirstTurn = true;
        public bool IsOver => _field.AsQueryable()
            .SelectMany(row => row)
            .All(cell => cell.State != CellState.None);
        public ReversiGame(int height, int width)
        {
            _field = new ReversiCell[height][];
            for (int row = 0; row < height; row++)
            {
                Field[row] = new ReversiCell[width];
                for(int column = 0; column < width; column++)
                {
                    Field[row][column] = new ReversiCell();
                }
            }
        }
        public ReversiGame(ReversiGameSerialized game)
        {
            Field = new ReversiCell[game._field.Length][];
            for (int row = 0; row < game._field.Length; row++)
            {
                Field[row] = new ReversiCell[game._field[0].Length];
                for (int column = 0; column < game._field[0].Length; column++)
                {
                    Field[row][column] = new ReversiCell(game._field[row][column]._state);
                }
            }
            IsFirstPlayerTurn = game._isFirstPlayerTurn;
            _isFirstTurn = game._isFirstTurn;
        }
        public ReversiGame(ReversiCell[][] field, bool isFirstPlayerTurn, bool isFirstTurn)
        {
            _field = field;
            _isFirstPlayerTurn = isFirstPlayerTurn;
            _isFirstTurn = isFirstTurn;
        }
        public int FirstPlayerScore => _field.SelectMany(x => x).Select(cell => cell.State).Count(state => state == CellState.FirstPlayer);
        public int SecondPlayerScore => _field.SelectMany(x => x).Select(cell => cell.State).Count(state => state == CellState.SecondPlayer);
        public ReversiCell[][] Field 
        {
            get { return _field; }
            private set { _field = value; OnPropertyChanged(nameof(Field)); OnPropertyChanged(nameof(Width)); OnPropertyChanged(nameof(Height)); }
        }
        public bool IsFirstPlayerTurn
        {
            get { return _isFirstPlayerTurn; }
            set { _isFirstPlayerTurn = value; OnPropertyChanged(nameof(IsFirstPlayerTurn)); }
        }
        public bool IsFirstTurn
        {
            get { return _isFirstTurn; }
        }
        public int Width { get { return _field[0].Length; } }
        public int Height { get { return _field.Length; } }

        public void MakeTurn(int y, int x)
        {
            if (Field[y][x].State != CellState.None) return;
            if(HasNeighbour(y, x))
            {
                Field[y][x].State = IsFirstPlayerTurn ? CellState.FirstPlayer : CellState.SecondPlayer;
                IsFirstPlayerTurn = !IsFirstPlayerTurn;
                //X axis before
                int limit = findXBefore(x, y);
                for (int column = limit + 1; column < x; column++)
                {
                    Field[y][column].Reverse();
                }
                //X axis after
                limit = findXAfter(x, y);
                for (int column = x + 1; column < limit; column++)
                {
                    Field[y][column].Reverse();
                }
                //Y axis before
                limit = findYBefore(x, y);
                for (int row = limit + 1; row < y; row++)
                {
                    Field[row][x].Reverse();
                }
                //Y axis after
                limit = findYAfter(x, y);
                for (int row = y + 1; row < limit; row++)
                {
                    Field[row][x].Reverse();
                }
                //Main diagonal before
                limit = findMainBefore(x, y);
                for (int row = y - 1, column = x - 1; column >  limit; column--, row--)
                {
                    Field[row][column].Reverse();
                }
                //Main diagonal after
                limit = findMainAfter(x, y);
                for (int row = y + 1, column = x + 1; column < limit; column++, row++)
                {
                    Field[row][column].Reverse();
                }
                //Side diagonal before
                limit = findSideBefore(x, y);
                for(int row = y + 1, column = x - 1; column > limit; column--, row++)
                {
                    Field[row][column].Reverse();
                }
                //Side diagonal after
                limit = findSideAfter(x, y);
                for (int row = y - 1, column = x + 1; column < limit; column++, row--)
                {
                    Field[row][column].Reverse();
                }

            }
            OnPropertyChanged(nameof(IsOver));
            OnPropertyChanged(nameof(FirstPlayerScore));
            OnPropertyChanged(nameof(SecondPlayerScore));
        }

        private int findXBefore(int x, int y)
        {
            var state = _field[y][x].State;
            for(int column = x - 1; column >= 0; column--)
            {
                if (_field[y][column].State == state)
                {
                        return column;
                }
            }
            return x;
        }
        private int findXAfter(int x, int y)
        {
            var state = _field[y][x].State;
            for (int column = x + 1; column < Width; column++)
            {
                if (_field[y][column].State == state)
                {
                    return column;
                }
            }
            return x;
        }
        private int findYBefore(int x, int y)
        {
            var state = _field[y][x].State;
            for (int row = y - 1; row >= 0; row--)
            {
                if (_field[row][x].State == state)
                {
                    return row;
                }
            }
            return y;
        }
        private int findYAfter(int x, int y)
        {
                var state = _field[y][x].State;
                for (int row = y + 1; row < Height; row++)
                {
                    if (_field[row][x].State == state)
                    {
                        return row;
                    }
                }
            return y;
        }
        private int findMainBefore(int x, int y)
        {
            var state = _field[y][x].State;
            for(int column = x - 1, row = y - 1; column >= 0 && row >= 0; row--, column--)
            {
                if (_field[row][column].State == state)
                {
                    return column;
                }
            }
            return x;
        }
        private int findMainAfter(int x, int y)
        {
            var state = _field[y][x].State;
            for (int column = x + 1, row = y + 1; column < Width && row < Height; row++, column++)
            {
                if (_field[row][column].State == state)
                {
                    return column;
                }
            }
            return x;
        }
        private int findSideBefore(int x, int y)
        {
            var state = _field[y][x].State;
            for(int column = x - 1, row = y + 1; column >= 0 && row < Height; column--, row++)
            {
                if (_field[row][column].State == state)
                {
                    return column;
                }
            }
            return x;
        }
        private int findSideAfter(int x, int y)
        {
            var state = _field[y][x].State;
            for (int column = x + 1, row = y - 1; column < Width && row >= 0; column++, row--)
            {
                if (_field[row][column].State == state)
                {
                    return column;
                }
            }
            return x;
        }
        private bool HasNeighbour(int y, int x)
        {
            bool result = false;
            try
            {
                result |= Field[y + 1][x].State != CellState.None;
            }
            catch (Exception) { }
            try
            {
                result |= Field[y][x + 1].State != CellState.None;
            }
            catch (Exception) { }
            try
            {
                result |= Field[y - 1][x].State != CellState.None;
            }
            catch (Exception) { }
            try
            {
                result |= Field[y][x - 1].State != CellState.None;
            }
            catch (Exception) { }
            try
            {
                result |= Field[y - 1][x - 1].State != CellState.None;
            }
            catch (Exception) { }
            try
            {
                result |= Field[y + 1][x - 1].State != CellState.None;
            }
            catch (Exception) { }
            try
            {
                result |= Field[y - 1][x + 1].State != CellState.None;
            }
            catch (Exception) { }
            try
            {
                result |= Field[y + 1][x + 1].State != CellState.None;
            }
            catch (Exception) { }
            result |= _isFirstTurn;
            _isFirstTurn = false;
            return result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}
