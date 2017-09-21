namespace PetApiClient.Models
{
    public class RequestResult<T>
    {
        public T Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public ApiException Exception { get; set; }
    }
}
