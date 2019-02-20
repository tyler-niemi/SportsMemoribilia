using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportsMemoribilia.Startup))]
namespace SportsMemoribilia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
