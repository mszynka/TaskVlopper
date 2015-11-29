using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskVlopper.Startup))]
namespace TaskVlopper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
