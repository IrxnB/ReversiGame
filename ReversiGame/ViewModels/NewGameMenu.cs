using Reversi.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reversi.ViewModels
{
    internal class NewGameMenu : INotifyPropertyChanged
    {
        private int _fieldWidth;
        private int _fieldHeight;
        private ICommand _startGame;

        public NewGameMenu()
        {
            _fieldHeight = 8;
            _fieldWidth = 8;
            _startGame = new CommandHandler(newGame);
        }
       
        public ICommand StartGame
        {
            get => _startGame;
        }
        
        public int FieldWidth
        {
            get { return _fieldWidth; }
            set { _fieldWidth = (value > 5) ? value : 5; OnPropertyChanged(nameof(FieldWidth)); }
        }

        public int FieldHeight
        {
            get { return _fieldHeight; }
            set { _fieldHeight = (value > 5) ? value : 5; OnPropertyChanged(nameof(FieldHeight)); }
        }
        private void newGame()
        {
            new GameWindow(FieldWidth, FieldHeight).Show();
            OnCloseWindow();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        
        public event EventHandler? CloseWindowHandler;

        private void OnCloseWindow() => CloseWindowHandler?.Invoke(this, new EventArgs());
    }
}
