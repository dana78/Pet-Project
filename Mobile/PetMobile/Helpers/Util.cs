using PetApiClient;
using Plugin.Connectivity.Abstractions;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PetMobile.Helpers
{
    public class Util
    {
        const string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
            @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public static async Task<(bool Available, string Message)> CheckApiConnection(IConnectivity connectivityService)
        {
            bool available = true;
            string message = string.Empty;

            if (!connectivityService.IsConnected)
            {
                available = false;
                message = "No tienes conexión a internet";
            }
            else if(! await connectivityService.IsRemoteReachable(Uris.BaseUrl))
            {
                available = false;
                message = "El servicio no está disponible ahora";
            }
            
            return (available, message);
        }

        public static bool EvaluateEmail(string text)
        {
            return Regex.IsMatch(text, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
    }
}
