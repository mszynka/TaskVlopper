using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskVlopper.Helpers
{
    public static class JsonErrorHelpers
    {
        public static object HttpError(HttpErrorCode errorCode = HttpErrorCode.UnknownError, string message = null)
        {
            return new { StatusCode = (int)errorCode, StatusDescription = message };
        }
    }
}