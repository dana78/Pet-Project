using System.Net.Http;

namespace PetApiClient
{
    public class ExceptionFactory
    {
        public static ApiException ThrowApiException(string methodName, HttpResponseMessage response)
        {
            return new ApiException(response.StatusCode, $"Error in {methodName}: {response.ReasonPhrase}");
        }
    }
}
