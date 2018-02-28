using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DocPlusApi.Startup))]
namespace DocPlusApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}
