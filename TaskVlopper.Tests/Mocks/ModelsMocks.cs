using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TaskVlopper.Base.Model;

namespace TaskVlopper.Tests.Mocks
{
    public static class ModelsMocks
    {
        public static Project ProjectModelFirst = new Project()
        {
            ID = 1,
            Deadline = new DateTime(2015, 12, 12),
            Description = "Description",
            EstimatedTimeInHours = 50,
            Name = "Name",
            StartDate = new DateTime(2015, 01, 01),
        };

        public static Project ProjectModelSecond = new Project()
        {
            ID = 2,
            Deadline = new DateTime(2015, 12, 12),
            Description = "DescriptionSecond",
            EstimatedTimeInHours = 50,
            Name = "NameSecond",
            StartDate = new DateTime(2015, 01, 01),
        };

        public static FormCollection Form = new FormCollection()
        {
            {"Project.Deadline", new DateTime(2015, 12, 12).ToString() },
            {"Project.Description", "Description" },
            {"Project.EstimatedTimeInHours", "" + 50 },
            {"Project.Name", "Name"},    
            {"Project.StartDate", new DateTime(2015, 01, 01).ToString() }        
        };
        
    }
}
