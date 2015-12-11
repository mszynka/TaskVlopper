using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TaskVlopper.Base;

namespace TaskVlopper.Base.Model
{
    public class Project : IBaseModel
    {
        [Key]
        [Index(IsUnique = true)]
        [Column(Order = 1)]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Project name can't be empty.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("Due date")]
        public DateTime? Deadline { get; set; }

        [DisplayName("Start date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("Estimated time")]
        public int EstimatedTimeInHours { get; set; }

    }
}
