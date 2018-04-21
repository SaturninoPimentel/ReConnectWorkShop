using FacesApp.Models;
using FacesApp.Services.Media;
using FacesApp.Services.Navigation;
using FacesApp.Services.Storage;
using FacesApp.ViewModels.Base;
using System.IO;
using System.Windows.Input;
using Xamarin.Forms;

namespace FacesApp.ViewModels
{
    public class NewImageViewModel : ViewModelBase
    {
        private readonly IMediaService _mediaService;
        private readonly IStorageService _storageService;
        private readonly INavigationServices _navigationServices;
        private string _imageName;
        private ImageSource _image;

        public NewImageViewModel(IMediaService mediaService,
            IStorageService storageService,
            INavigationServices navigationServices)
        {
            _mediaService = mediaService;
            _storageService = storageService;
            _navigationServices = navigationServices;
        }

        public ICommand TakePhotoCommand => new Command(TakePhotoCommandExecute);

        public string ImageName
        {
            get => _imageName;
            set
            {
                _imageName = value;
                OnPropertyChanged();
            }
        }

        public ImageSource Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged();
            }
        }

        private async void TakePhotoCommandExecute(object obj)
        {
            Photo photo = await _mediaService.SelectPhotoAsync(ImageName);
            Image = ImageSource.FromStream(() => new MemoryStream(photo.PhotoData));
            await _storageService.UploadPhotoAsync(photo);
            await _navigationServices.NavigateToAsync<MainViewModel>();
        }
    }
}