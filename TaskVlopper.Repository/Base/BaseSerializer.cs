using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base;
using TaskVlopper.Base.Enums;

namespace TaskVlopper.Repository.Base
{
    public class BaseSerializer<T> : IBaseSerializer<T>
        where T : IBaseModel, new()
    {
        public IDictionary<string, string> Deserialize(T model)
        {
            TypeInfo modelInfo = model.GetType() as TypeInfo;
            List<PropertyInfo> modelInfoList = modelInfo.GetProperties().ToList();

            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (PropertyInfo info in modelInfoList)
            {
                dictionary.Add(info.Name, info.GetValue(model).ToString());
            }

            return dictionary;
        }

        public T Edit(T model, NameValueCollection parameters)
        {
            TypeInfo modelInfo = model.GetType() as TypeInfo;
            List<PropertyInfo> modelInfoList = modelInfo.GetProperties().ToList();

            foreach (PropertyInfo info in modelInfoList)
            {
                var buf = parameters[model.GetType().Name + "." + info.Name];

                if (buf == null)
                    continue;

                else if (info.PropertyType == typeof(string))
                    info.SetValue(model, buf);

                else if (info.PropertyType == typeof(Nullable<DateTime>) || info.PropertyType == typeof(DateTime))
                    info.SetValue(model, DateTime.Parse(buf));

                else if (info.PropertyType == typeof(Int32)
                    || info.PropertyType == typeof(int)
                    || info.PropertyType == typeof(Nullable<Int32>))
                    info.SetValue(model, Int32.Parse(buf));

                else if (info.PropertyType == typeof(double))
                    info.SetValue(model, double.Parse(buf));

                else if (info.PropertyType == typeof(TaskStatusEnum) 
                    || info.PropertyType == typeof(Nullable<TaskStatusEnum>))
                    info.SetValue(model, new TaskStatusEnum().Parse(buf)); 


            }

            return model;
        }

        public T Serialize(NameValueCollection parameters)
        {
            return Edit(new T(), parameters);
        }
    }
}
