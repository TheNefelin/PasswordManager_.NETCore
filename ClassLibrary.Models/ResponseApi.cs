namespace ClassLibrary.Models
{
    public class ResponseApi<T>
    {
        public bool IsSucces { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
    }
}
