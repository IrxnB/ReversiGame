using System;
using System.Windows.Input;

namespace Reversi
{
    
    internal class CommandHandler : ICommand
    {
        public event EventHandler? CanExecuteChanged;
    
        private Action _action;
    
        public CommandHandler(Action action)
        {
            _action = action;
        }
    
        public bool CanExecute(object? parameter)
        {
            return true;
        }
    
        public void Execute(object? parameter)
        {
            _action();
        }
    }
}
