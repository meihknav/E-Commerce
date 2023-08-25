using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLBHDuongVanKhiem2054052026.Startup))]
namespace QLBHDuongVanKhiem2054052026
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
