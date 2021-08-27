using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Monitoring_First_Version.Startup))]
namespace Monitoring_First_Version
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
