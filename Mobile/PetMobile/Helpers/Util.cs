using Plugin.Connectivity.Abstractions;
using System.Threading.Tasks;

namespace PetMobile.Helpers
{
    public class Util
    {
        public static async Task<(bool Available, string Message)> CheckApiConnection(IConnectivity connectivityService)
        {
            bool available = true;
            string message = string.Empty;

            if (!connectivityService.IsConnected)
            {
                available = false;
                message = "No tienes conexión a internet";
            }
            else if(! await connectivityService.IsRemoteReachable(Constants.BaseUrl))
            {
                available = false;
                message = "El servicio no está disponible ahora";
            }
            
            return (available, message);
        }
    }
}
