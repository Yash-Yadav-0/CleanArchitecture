using Newtonsoft.Json;

namespace CleanArchitecture.Application.Middlewares.ExceptionMiddleware
{
    public class ExceptionModel : ErrorStatusCode
    {
        public IEnumerable<string> errors { set; get; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    public class ErrorStatusCode
    {
        public int StatusCode { get; set; }
    }
}
