using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Downwork_Notifier.GUI.Commands
{
    public class CommandHandler : ICommand
    {
        #region private fields
        private Action<object> _executeHandler;
        private Func<object, Task> _executeHandlerAsync;
        private Func<object, bool> _canExecuteHandler;
        #endregion private fields

        #region Constructors
        public CommandHandler(Action<object> executeHandler, Func<object, bool> canExecuteHandler = null)
        {
            _executeHandler = executeHandler ?? throw new ArgumentNullException(nameof(executeHandler));
            _canExecuteHandler = canExecuteHandler;
        }
        public CommandHandler(Func<object, Task> executeHandlerAsync, Func<object, bool> canExecuteHandler = null)
        {
            _executeHandlerAsync = executeHandlerAsync ?? throw new ArgumentNullException(nameof(executeHandlerAsync));
            _canExecuteHandler = canExecuteHandler;
        }
        #endregion Constructors

        #region ICommand implementation
        public event EventHandler CanExecuteChanged;

        private bool _canExecute = true;
        public bool CanExecute(object parameter)
        {
            if (_canExecute != (_canExecuteHandler?.Invoke(parameter) ?? _canExecute))
            {
                _canExecute = !_canExecute;
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
            return _canExecute;
        }

        public async void Execute(object parameter)
        {
            if (_executeHandler != null)
            {
                _executeHandler.Invoke(parameter);
            }
            else
            {
                await ExecuteAsync(parameter).ConfigureAwait(false);
            }
        }
        #endregion ICommand implementation

        public async Task ExecuteAsync(object parameter)
        {
            if (_executeHandlerAsync != null)
            {
                await _executeHandlerAsync.Invoke(parameter).ConfigureAwait(false);
            }
            else
            {
                await Task.Run(() => _executeHandler?.Invoke(parameter)).ConfigureAwait(false);
            }
        }
    }
}
