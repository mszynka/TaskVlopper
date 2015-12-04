using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Helpers
{
    public class MeetingSerializer : IBaseSerializer<Meeting>
    {
        public IDictionary<string, string> Deserialize(Meeting model)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("ProjectID", model.ProjectID.ToString());
            dictionary.Add("TaskID", model.TaskID.ToString());
            dictionary.Add("DateAndTime", model.DateAndTime.ToString());
            dictionary.Add("Title", model.Title.ToString());
            dictionary.Add("Description", model.Description.ToString());

            return dictionary;
        }

        public Meeting Edit(Meeting model, NameValueCollection parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters["ProjectID"]))
                model.ProjectID = int.Parse(parameters["ProjectID"]);

            if (!string.IsNullOrWhiteSpace(parameters["TaskID"]))
                model.TaskID = int.Parse(parameters["TaskID"]);

            if (!string.IsNullOrWhiteSpace(parameters["DateAndTime"]))
                model.DateAndTime = DateTime.Parse(parameters["DateAndTime"]);

            if (!string.IsNullOrWhiteSpace(parameters["Title"]))
                model.Title = parameters["Title"];

            if (!string.IsNullOrWhiteSpace(parameters["Description"]))
                model.Description = parameters["Description"];

            return model;
        }

        public Meeting Serialize(NameValueCollection parameters)
        {
            return Edit(new Meeting(), parameters);
        }
    }
}