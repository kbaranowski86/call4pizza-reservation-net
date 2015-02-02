using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Call4Pizza_reservation.Startup))]
namespace Call4Pizza_reservation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
