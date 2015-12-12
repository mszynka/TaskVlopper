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

            if (Message == null)
                Message = "Internal Server Error";
        }

        private JsonHttpViewModel getHttpData()
        {
            JsonHttpViewModel viewModel = new JsonHttpViewModel();
            viewModel.HttpCode = (int)HttpCode;
            viewModel.Message = Message;

            return viewModel;
        }

        public JsonHttpViewModel getError()
        {
            Logger.LogException(Message);

            return getHttpData();
        }

        public JsonHttpViewModel getWarning()
        {
            Logger.LogWarning(Message);

            return getHttpData();
        }

        public JsonHttpViewModel getInfo()
        {
            Logger.LogInfo(Message);

            return getHttpData();
        }

        public JsonHttpViewModel getData()
        {
            Logger.Log(Message);

            return getHttpData();
        }
    }
}