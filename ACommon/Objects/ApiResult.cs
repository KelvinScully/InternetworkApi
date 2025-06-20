using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ACommon.Objects
{
    public class ApiResult<T>
    {
        public bool IsSuccessful { get; set; }
        public T? Value { get; set; }
        public string Message { get; set; } = string.Empty;
        public StatusCodes HttpStatusCode { get; set; }
    }

    public enum StatusCodes
    {
        Status200Ok = 200,
        Status400BadRequest = 400,
        Status404NotFound = 404,
        Status500InternalServerError = 500
    }
}
