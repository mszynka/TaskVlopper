using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskVlopper.Helpers;

namespace TaskVlopper.Models
{
    public class ErrorViewModel
    {   
        [Required]
        public int HttpCode { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Message { get; set; }
    }
}