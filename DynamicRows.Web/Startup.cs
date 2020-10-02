using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DynamicRows.Web.Startup))]
namespace DynamicRows.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
