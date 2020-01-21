namespace Client.Wpf
{
    using System;
    using System.Windows.Input;

    public class DelegateCommand : ICommand
    {
        #region ---------- Private readonly fields ----------

        private readonly Func<object, bool> canExecute;
        private readonly Action<object> execute;

        #endregion

        #region ---------- Public constructors ----------

        public DelegateCommand(Action<object> execute)
            : base()
        {
            this.execute = execute;
        }

        public DelegateCommand(Action<object> execute, Func<object, bool> canExecute)
            : this(execute)
        {
            this.canExecute = canExecute;
        }

        #endregion

        #region ---------- Public events ----------

        public event EventHandler CanExecuteChanged;

        #endregion

        #region ---------- Public methods ----------

        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
