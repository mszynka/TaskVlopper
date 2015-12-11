using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskVlopper.Base;
using TaskVlopper.Base.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TaskVlopper.Base.Model
{
    public class Test : IBaseModel
    {
        //Implementing interface 
        [Key]
        [Index(IsUnique = true)]
        [Column(Order = 1)]
        public int ID { get; set; }


        [Index(IsUnique = true)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 1)]
        public string Name { get; set; }

        public DateTime? Date { get; set; }
        public TestTypeEnum Type { get; set; }
        public double Result { get; set; }

    }
}
