namespace ClassLibrary.Models
{
    public class ResponseSql
    {
        public bool IsSucces { get; set; }
        public int StatusCode { get; set; }
        public string Msge { get; set; } = string.Empty;
    }
}
