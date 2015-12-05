using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using TaskVlopper.Base;

namespace TaskVlopper.Base
{
    public interface IBaseSerializer<T> where T : IBaseModel
    {
        /// <summary>
        /// This method returns Model from NameValueCollection of parameters
        /// </summary>
        /// <param name="parameters">NameValueCollection</param>
        /// <returns>Model object</returns>
        T Serialize(NameValueCollection parameters);

        /// <summary>
        /// This method returns deserialized IDictionary from model
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>IDictionary</returns>
        IDictionary<string, string> Deserialize(T model);

        /// <summary>
        /// This method replaces models fields with given parameters
        /// </summary>
        /// <param name="model">Model</param>
        /// <param name="parameters">NameValueCollection</param>
        /// <returns>Returns model object</returns>
        T Edit(T model, NameValueCollection parameters);
    }
}
