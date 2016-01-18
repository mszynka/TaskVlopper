using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskVlopper.Base.Repository;
using TaskVlopper.Models;

namespace TaskVlopper.Identity
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(DatabasePicker.GetConnectionStringName(), throwIfV1Schema: false)
        {
        }
    }
}
