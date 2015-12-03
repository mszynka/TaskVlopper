using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Base;
using TaskVlopper.Base.Enums;

namespace TaskVlopper.Base.Model
{
    public class Task : IBaseModel
    {
        [Key]
        [Index(IsUnique = true)]
        [Column(Order = 1)]
        public int ID { get; set; }

        public int ProjectID { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int EstimatedTimeInHours { get; set; }
        public TaskStatusEnum Status { get; set; }
        public int ExecutiveUser { get; set; }
        public int Storypoints { get; set; }


    }
}
