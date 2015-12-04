using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using TaskVlopper.Base.Model;
using TaskVlopper.Base.Enums;

namespace TaskVlopper.Helpers
{
    public class TaskSerializer : IBaseSerializer<Task>
    {
        public IDictionary<string, string> Deserialize(Task model)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("ProjectID", model.ProjectID.ToString());
            dictionary.Add("Name", model.Name.ToString());
            dictionary.Add("Description", model.Description.ToString());
            dictionary.Add("StartDate", model.StartDate.ToString());
            dictionary.Add("EndDate", model.EndDate.ToString());
            dictionary.Add("EstimatedTimeInHours", model.EstimatedTimeInHours.ToString());
            dictionary.Add("Status", model.Status.ToString());
            dictionary.Add("ExecutiveUser", model.ExecutiveUserID.ToString());
            dictionary.Add("Storypoints", model.Storypoints.ToString());

            return dictionary;
        }

        public Task Edit(Task model, NameValueCollection parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters["ProjectID"]))
                model.ProjectID = int.Parse(parameters["ProjectID"]);

            if (!string.IsNullOrWhiteSpace(parameters["Name"]))
                model.Name = parameters["Name"];

            if (!string.IsNullOrWhiteSpace(parameters["Description"]))
                model.Description = parameters["Description"];

            if (!string.IsNullOrWhiteSpace(parameters["StartDate"]))
                model.StartDate = DateTime.Parse(parameters["StartDate"]);

            if (!string.IsNullOrWhiteSpace(parameters["EndDate"]))
                model.EndDate = DateTime.Parse(parameters["EndDate"]);

            if (!string.IsNullOrWhiteSpace(parameters["EstimatedTimeInHours"]))
                model.EstimatedTimeInHours = int.Parse(parameters["EstimatedTimeInHours"]);

            if (!string.IsNullOrWhiteSpace(parameters["Status"]))
                model.Status = (new TaskStatusEnum().Parse(parameters["Status"]));

            if (!string.IsNullOrWhiteSpace(parameters["ExecutiveUserID"]))
                model.ExecutiveUserID = int.Parse(parameters["ExecutiveUserID"]);

            if (!string.IsNullOrWhiteSpace(parameters["Storypoints"]))
                model.Storypoints = int.Parse(parameters["Storypoints"]);

            return model;
        }

        public Task Serialize(NameValueCollection parameters)
        {
            return Edit(new Task(), parameters);
        }
    }
}