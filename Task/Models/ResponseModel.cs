using System;

namespace Task.Models
{
    public class ResponseModel<T>
    {
        public T? Data { get; set; }
        public bool success { get; set; }
        public bool warning { get; set; }
        public string? message { get; set; }

        public string toString()
        {
            return "Response{" +
                    "Data=" + Data +
                    ", IsSuccess=" + success +
                    ", IsWarning=" + warning +
                    ", Message='" + message + '\'' +
                    '}';
        }
    }
}
