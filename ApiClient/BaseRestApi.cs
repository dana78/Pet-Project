using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PetApiClient
{
    public abstract class BaseRestApi
    {
        protected abstract string BaseUrl { get; }
        private readonly HttpClient _client = new HttpClient();

        #region GetApiAsync

        protected async Task<TResult> GetApiAsync<TResult>(string relativeUri, params KeyValuePair<string, string>[] pathParams)
        {
            var requestUri = BuildUri(relativeUri, pathParams);
            var response = await _client.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
                throw ExceptionFactory.ThrowApiException("GetApiAsync<T>", response);

            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(jsonContent);

            return result;
        }

        #endregion

        #region PostApiAsync

        protected async Task<TResult> PostApiAsync<TResult>(string relativeUri, object body, params KeyValuePair<string, string>[] pathParams)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            return await PostApiAsync<TResult>(relativeUri, jsonBody, pathParams);
        }

        protected async Task<TResult> PostApiAsync<TResult>(string relativeUri, string jsonBody, params KeyValuePair<string, string>[] pathParams)
        {
            var requestUri = BuildUri(relativeUri, pathParams);
            var response = await _client.PostAsync(requestUri, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw ExceptionFactory.ThrowApiException("PostApiAsync<T>", response);

            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(jsonContent);

            return result;
        }

        protected async Task PostApiAsync(string relativeUri, object body, params KeyValuePair<string, string>[] pathParams)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            await PostApiAsync(relativeUri, jsonBody, pathParams);
        }

        protected async Task PostApiAsync(string relativeUri, string jsonBody, params KeyValuePair<string, string>[] pathParams)
        {
            var requestUri = BuildUri(relativeUri, pathParams);
            var response = await _client.PostAsync(requestUri, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw ExceptionFactory.ThrowApiException("PostApiAsync", response);
        }

        #endregion

        #region PutApiAsync

        protected async Task PutApiAsync(string relativeUri, object body, params KeyValuePair<string, string>[] pathParams)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            await PutApiAsync(relativeUri, jsonBody, pathParams);
        }

        protected async Task PutApiAsync(string relativeUri, string jsonBody, params KeyValuePair<string, string>[] pathParams)
        {
            var requestUri = BuildUri(relativeUri, pathParams);
            var response = await _client.PutAsync(requestUri, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw ExceptionFactory.ThrowApiException("PutApiAsync", response);
        }

        protected async Task<TResult> PutApiAsync<TResult>(string relativeUri, object body, params KeyValuePair<string, string>[] pathParams)
        {
            var jsonBody = JsonConvert.SerializeObject(body);
            return await PutApiAsync<TResult>(relativeUri, jsonBody, pathParams);
        }

        protected async Task<TResult> PutApiAsync<TResult>(string relativeUri, string jsonBody, params KeyValuePair<string, string>[] pathParams)
        {
            var requestUri = BuildUri(relativeUri, pathParams);
            var response = await _client.PutAsync(requestUri, new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode)
                throw ExceptionFactory.ThrowApiException("PutApiAsync<T>", response);

            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TResult>(jsonContent);

            return result;
        }

        #endregion

        #region Helper methods
        private string BuildUri(string relativeUri, KeyValuePair<string, string>[] pathParams = null)
        {
            var uriBuilder = new StringBuilder(BaseUrl);
            uriBuilder.Append(relativeUri);
            if (pathParams != null)
            {
                uriBuilder.Append("?");
                foreach (var param in pathParams)
                    uriBuilder.Append($"{param.Key}={param.Value}&");
            }
            uriBuilder.Remove(uriBuilder.Length - 1, 1);
            return uriBuilder.ToString();
        }
        #endregion
    }
}
