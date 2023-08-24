using System.Net;

namespace PRN231_G4_ProductManagement_BE.DTO
{
    public class ResponseBody<T>
    {
        public HttpStatusCode code { get; set; }
        public string message { get; set; }
        public T? data { get; set; }
        public int? maxPage { get; set; }
    }
}
