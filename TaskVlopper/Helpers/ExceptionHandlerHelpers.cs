using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskVlopper.Models;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Helpers
{
    public class ExceptionHandler
    {
        private Exception Ex { get; set; }
        public HttpCodeEnum ErrorCode { get; private set; }
        public string Message { get; private set; }

        public ExceptionHandler(Exception ex = null, HttpCodeEnum errorCode = HttpCodeEnum.InternalServerError)
        {
            Ex = ex;
            ErrorCode = errorCode;

            if (Ex != null)
                Message = Ex.Message;
            else
                Message = errorCode.ToString();
        }

        public ErrorViewModel handleError()
        {
            Logger.LogException(Message);

            ErrorViewModel viewModel = new ErrorViewModel();
            viewModel.HttpCode = (int)ErrorCode;
            viewModel.Message = Message;

            return viewModel;
        }
    }
}