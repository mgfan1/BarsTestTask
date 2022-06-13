using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Bars.Helpers
{
    public class Command<T> : ICommand
    {
        #region Fields

        private const string ParameterIsNullExMessage = "Execute parameter cannot be null";
        private readonly Action<T> _execute;
        private readonly Predicate<T> _canExecute;

        #endregion

        #region Constructors

        public Command(Action<T> execute, Predicate<T> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(ParameterIsNullExMessage);
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke((T)parameter) ?? true;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter) => _execute((T)parameter);

        #endregion
    }

    public class RelayCommand : Command<object>
    {
        public RelayCommand(Action execute) : this(execute, null) { }
        public RelayCommand(Action execute, Func<bool> canExecute)
            : base(param => execute?.Invoke(),
                param => (canExecute?.Invoke()) ?? true)
        { }
    }
}