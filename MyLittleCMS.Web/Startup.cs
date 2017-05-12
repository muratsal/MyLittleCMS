using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyLittleCMS.Web.Startup))]
namespace MyLittleCMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
