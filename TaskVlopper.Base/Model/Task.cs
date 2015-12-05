using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
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

        [DisplayName("Start date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Due date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Estimated time")]
        public int EstimatedTimeInHours { get; set; }
        public TaskStatusEnum Status { get; set; }
        
        [DisplayName("Executive user")]
        public int ExecutiveUserID { get; set; }
        public int Storypoints { get; set; }


    }
}
