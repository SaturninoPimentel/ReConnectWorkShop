using FacesApp.Models;
using System.Threading.Tasks;

namespace FacesApp.Services.Storage
{
    public interface IStorageService
    {
        Task UploadPhotoAsync(Photo photoInfo);
    }
}