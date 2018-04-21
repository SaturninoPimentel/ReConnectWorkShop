using Plugin.Connectivity;
using System.Threading.Tasks;

namespace FacesApp.Services.Connectivity
{
    public class ConnectivityService : IConnectivityService
    {
        public async Task<bool> HasConnectivity()
        {
            return CrossConnectivity.Current.IsConnected &&
                   await CrossConnectivity.Current.IsReachable("https://google.com.mx");
        }
    }
}