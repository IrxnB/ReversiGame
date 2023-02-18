using Reversi.Views;
using ReversiLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reversi.ViewModels
{
    internal class LoadGameMenu : INotifyPropertyChanged
    {
        private SaveManager _manager;
        private FileInfo _selectedFile;
        private ICommand _load;
        public LoadGameMenu()
        {
            _manager = SaveManager.GetInstance();
            _load = new CommandHandler(load);
        }
        public FileInfo SelectedFile
        {
            get { return _selectedFile; }
            set { _selectedFile = value; OnPropertyChanged(nameof(SelectedFile)); }
        }
        public ICommand Load
        {
            get { return _load; }
        }

        public FileInfo[] Files => _manager.Files;
        private void load()
        {
            if(_selectedFile != null)
            {
                new GameWindow(_manager.Load(_selectedFile.FullName)).Show();
            }
            OnClose();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public event EventHandler? CloseHandler;
        private void OnClose()
            => CloseHandler?.Invoke(this, new EventArgs());
    }
}
