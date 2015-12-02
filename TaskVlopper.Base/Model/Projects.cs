using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Base;

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

        public DateTime? Deadline { get; set; }

        public DateTime? StartDate { get; set; }

        public int EstimatedTimeInHours { get; set; }

    }
}
