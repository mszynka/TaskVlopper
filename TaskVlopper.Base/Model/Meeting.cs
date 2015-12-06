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
    public class Meeting : IBaseModel
    {
        [Key]
        [Index(IsUnique = true)]
        [Column(Order = 1)]
        public int ID { get; set; }

        public int ProjectID { get; set; }
        public int? TaskID { get; set; }

        [DisplayName("Date and time")]
        public DateTime DateAndTime { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Title can't be emepty.")]
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
