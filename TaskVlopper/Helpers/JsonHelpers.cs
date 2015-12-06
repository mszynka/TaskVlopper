using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskVlopper.Helpers
{
    public static class JsonHelpers
    {
        public static object HttpMessage(HttpCodeEnum errorCode = HttpCodeEnum.UnknownError, string message = null)
        {
            return new { StatusCode = (int)errorCode, StatusDescription = message };
        }
    }
}