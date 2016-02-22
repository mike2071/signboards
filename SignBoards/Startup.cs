using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignBoards.Startup))]
namespace SignBoards
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
