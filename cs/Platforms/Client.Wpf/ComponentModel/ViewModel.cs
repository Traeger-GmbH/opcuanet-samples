namespace Client.Wpf
{
    using System.ComponentModel;

    public abstract class ViewModel : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region ---------- Protected constructors ----------

        protected ViewModel()
            : base()
        {
        }

        #endregion

        #region ---------- Public events ----------

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        #region ---------- Protected methods ----------

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            this.PropertyChanged?.Invoke(this, e);
        }

        protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
        {
            this.PropertyChanging?.Invoke(this, e);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        protected void RaisePropertyChanging(string propertyName)
        {
            this.OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
        }

        #endregion
    }
}
