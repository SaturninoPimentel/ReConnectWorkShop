using FacesApp.ViewModels.Base;
using System.Threading.Tasks;

namespace FacesApp.Services.Navigation
{
    public interface INavigationServices
    {
        Task InitializeAsync();

        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}