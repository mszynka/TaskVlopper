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
        public string ID { get; private set; }

        public JsonDataHandler(Exception ex = null, HttpCodeEnum httpCode = HttpCodeEnum.InternalServerError, string message = null, string id = null)
        {
            Ex = ex;
            HttpCode = httpCode;
            ID = id;
            messageGenerator(message);
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

        #region Helpers

        private void messageGenerator(string message)
        {
            Message = HttpCode.ToString();

            if (!string.IsNullOrWhiteSpace(message))
                Message = string.Join(" ", Message, message);
            else
                Message = string.Join(" ", Message, "Internal Server Error");

            if (Ex != null)
                Message = string.Join(" ", Message, Ex.Message);
        }

        private JsonHttpViewModel getHttpData()
        {
            JsonHttpViewModel viewModel = new JsonHttpViewModel();
            viewModel.HttpCode = (int)HttpCode;
            viewModel.Message = Message;
            viewModel.ID = ID;

            return viewModel;
        }

        #endregion
    }
}