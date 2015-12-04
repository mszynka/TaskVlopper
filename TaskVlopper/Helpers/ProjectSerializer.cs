using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using TaskVlopper.Base.Model;
using TaskVlopper.Helpers;

namespace TaskVlopper.Helpers
{
    public class ProjectSerializer : IBaseSerializer<Project>
    {
        public IDictionary<string, string> Deserialize(Project model)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add("Name", model.Name.ToString());
            dictionary.Add("Description", model.Description.ToString());
            dictionary.Add("StartDate", model.StartDate.ToString());
            dictionary.Add("Deadline", model.Deadline.ToString());
            dictionary.Add("EstimatedTimeInHours", model.EstimatedTimeInHours.ToString());

            return dictionary;
        }

        public Project Edit(Project model, NameValueCollection parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters["Name"]))
                model.Name = parameters["Name"];

            if (!string.IsNullOrWhiteSpace(parameters["Description"]))
                model.Description = parameters["Description"];

            if (!string.IsNullOrWhiteSpace(parameters["StartDate"]))
                model.StartDate = DateTime.Parse(parameters["StartDate"]);

            if (!string.IsNullOrWhiteSpace(parameters["Deadline"]))
                model.Deadline = DateTime.Parse(parameters["Deadline"]);

            if (!string.IsNullOrWhiteSpace(parameters["EstimatedTimeInHours"]))
                model.EstimatedTimeInHours = int.Parse(parameters["EstimatedTimeInHours"]);

            return model;
        }

        public Project Serialize(NameValueCollection parameters)
        {
            return Edit(new Project(), parameters);
        }
    }
}