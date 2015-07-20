using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IkMovieTime.Startup))]
namespace IkMovieTime
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
