using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ReversiLib
{
    public class ReversiCell : INotifyPropertyChanged
    {
        private CellState _state;

        public ReversiCell()
        {
            _state = CellState.None;
        }
        public ReversiCell(CellState state)
        {
            _state = state;
        }
        public CellState State
        {
            get { return _state; }
            set { _state = value; OnPropertyChanged(nameof(State)); }
        }

        public void Reverse()
        {
            if(State != CellState.None)
            {
                State = (State == CellState.FirstPlayer)
                    ? CellState.SecondPlayer
                    : CellState.FirstPlayer;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop="")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}