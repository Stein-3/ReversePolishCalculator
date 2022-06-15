using System;
using System.Windows.Input;

namespace RPNCalculator.MAUI
{
    internal class DelegateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private Func<object?, bool>? _canExecute;

        private Action<object?> _execute;

        public DelegateCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _canExecute = canExecute;
            _execute = execute;

        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        public void OnCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
