using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Helpers
{
    public class WorklogSerializer : IBaseSerializer<Worklog>
    {
        public IDictionary<string, string> Deserialize(Worklog model)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("TaskID", model.TaskID.ToString());
            dictionary.Add("UserID", model.UserID.ToString());
            dictionary.Add("Description", model.Description.ToString());
            dictionary.Add("Date", model.Date.ToString());
            dictionary.Add("Hours", model.Hours.ToString());

            return dictionary;
        }

        public Worklog Edit(Worklog model, NameValueCollection parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters["TaskID"]))
                model.TaskID = int.Parse(parameters["TaskID"]);

            if (!string.IsNullOrWhiteSpace(parameters["UserID"]))
                model.UserID = int.Parse(parameters["UserID"]);

            if (!string.IsNullOrWhiteSpace(parameters["Description"]))
                model.Description = parameters["Description"];

            if (!string.IsNullOrWhiteSpace(parameters["Date"]))
                model.Date = DateTime.Parse(parameters["Date"]);

            if (!string.IsNullOrWhiteSpace(parameters["Hours"]))
                model.Hours = int.Parse(parameters["Hours"]);

            return model;
        }

        public Worklog Serialize(NameValueCollection parameters)
        {
            return Edit(new Worklog(), parameters);
        }
    }
}