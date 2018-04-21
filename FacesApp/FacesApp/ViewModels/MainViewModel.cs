using FacesApp.Models;
using FacesApp.Services.Connectivity;
using FacesApp.Services.Navigation;
using FacesApp.Services.People;
using FacesApp.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace FacesApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly INavigationServices _navigationServices;
        private readonly IPeopleService _personService;
        private readonly IConnectivityService _connectivityService;
        private ObservableCollection<Person> _persons;

        public ObservableCollection<Person> Persons
        {
            get => _persons ?? (_persons = new ObservableCollection<Person>());
            set
            {
                _persons = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(INavigationServices navigationServices,
            IPeopleService personService,
            IConnectivityService connectivityService)
        {
            _navigationServices = navigationServices;
            _personService = personService;
            _connectivityService = connectivityService;
        }

        public ICommand RefreshCommand => new Command(RefreshCommandExecute);

        public ICommand AddNewImageCommand => new Command(AddNewImageCommandExecute);

        public override async Task InitAsync(object parameter)
        {
            await GetPersonsAsync();
        }

        public async void RefreshCommandExecute()
        {
            await GetPersonsAsync().ConfigureAwait(false);
        }

        private async void AddNewImageCommandExecute(object obj)
        {
            await _navigationServices.NavigateToAsync<NewImageViewModel>();
        }

        private async Task GetPersonsAsync()
        {
            IsBusy = true;
            if (await _connectivityService.HasConnectivity())
            {
                try
                {
                    List<Person> peopleResult = await _personService.GetFacesAsync();
                    if (peopleResult != null)
                    {
                        foreach (Person person in peopleResult)
                        {
                            if (Persons.All(pre => pre.RowKey != person.RowKey))
                            {
                                Persons.Add(person);
                            }
                        }
                    }
                }
                catch (System.Exception)
                {
                    //TODO: enviar mensaje a usuario
                }
            }
            else
            {
                //TODO: enviar mensaje a usuario
            }

            IsBusy = false;
        }
    }
}