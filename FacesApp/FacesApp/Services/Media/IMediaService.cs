using FacesApp.Models;
using System.Threading.Tasks;

namespace FacesApp.Services.Media
{
    public interface IMediaService
    {
        Task<Photo> TakePhotoAsync(string name);

        Task<Photo> SelectPhotoAsync(string name);
    }
}