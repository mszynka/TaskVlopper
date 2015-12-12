using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TaskVlopper.Models;
using TaskVlopper.Repository.Base;

namespace TaskVlopper.Helpers
{
    public class JsonDataHandler
    {
        private Exception Ex { get; set; }
        public HttpCodeEnum HttpCode { get; private set; }
        public string Message { get; private set; }

        public JsonDataHandler(Exception ex = null, HttpCodeEnum httpCode = HttpCodeEnum.InternalServerError, string message = null)
        {
            Ex = ex;
            HttpCode = httpCode;
            Message = HttpCode.ToString();

            if (message != null)
                Message = string.Join(" ", Message, message);

            if (Ex != null)
                Message = string.Join(" ", Message, Ex.Message);
        }

        private JsonHttpViewModel handleHttpData()
        {
            JsonHttpViewModel viewModel = new JsonHttpViewModel();
            viewModel.HttpCode = (int)HttpCode;
            viewModel.Message = Message;

            return viewModel;
        }

        public JsonHttpViewModel handleError()
        {
            Logger.LogException(Message);

            return handleHttpData();
        }

        public JsonHttpViewModel handleWarning()
        {
            Logger.LogWarning(Message);

            return handleHttpData();
        }

        public JsonHttpViewModel handleInfo()
        {
            Logger.LogInfo(Message);

            return handleHttpData();
        }

        public JsonHttpViewModel handleData()
        {
            Logger.Log(Message);

            return handleHttpData();
        }
    }
}