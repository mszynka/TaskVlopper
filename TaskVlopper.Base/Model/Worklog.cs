using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using TaskVlopper.Base;

namespace TaskVlopper.Base.Model
{
    public class Worklog : IBaseModel
    {
        [Key]
        [Index(IsUnique = true)]
        [Column(Order = 1)]
        public int ID { get; set; }

        public int TaskID { get; set; }

        public int UserID { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int Hours { get; set; }


    }
}
