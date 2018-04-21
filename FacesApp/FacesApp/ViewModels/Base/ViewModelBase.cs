using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FacesApp.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public virtual Task InitAsync(object parameter)
        {
            return Task.FromResult(false);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}