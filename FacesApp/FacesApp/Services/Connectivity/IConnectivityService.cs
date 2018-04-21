using System.Threading.Tasks;

namespace FacesApp.Services.Connectivity
{
    public interface IConnectivityService
    {
        Task<bool> HasConnectivity();
    }
}