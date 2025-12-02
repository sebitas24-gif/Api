using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos
{
    public class ApirResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public static ApirResult<T> Ok(T data)
        {
            return new ApirResult<T>
            {
                Success = true,
                Data = data
            };
        }

        public static ApirResult<T> Fail(string message)
        {
            return new ApirResult<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}
