using ReversiLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace Reversi.ViewModels
{
    class GameVM : INotifyPropertyChanged
    {
        private ReversiGame _game;
        public ICommand _saveCommand;
        private SaveManager _saveManager = SaveManager.GetInstance();
        public GameVM(int fieldWidth, int fieldHeight) : this(new ReversiGame(fieldHeight, fieldHeight)){}

        public ICommand Save
        {
            get { return _saveCommand; }
        }

        private void save()
        {
            _saveManager.Save(_game);
            OnCloseWindow();
        }

        public ReversiGame Game
        { 
            get { return _game; }
            set { _game = value; }
        }
        public GameVM(ReversiGame savedGame)
        {
            _game = savedGame;
            _saveCommand = new CommandHandler(save);
            _game.PropertyChanged += (s, e) =>
            {
                if (s is ReversiGame 
                && e.PropertyName.Equals(nameof(ReversiGame.IsOver))
                && Game.IsOver == true)
                {
                    GameOverHandler?.Invoke(this, new GameOverEventArgs(Game.FirstPlayerScore, Game.SecondPlayerScore));
                }
            };

        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop="") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event EventHandler? CloseWindowHandler;
        public event EventHandler? GameOverHandler;
        private void OnCloseWindow() => CloseWindowHandler?.Invoke(this, new EventArgs());
    }
}
