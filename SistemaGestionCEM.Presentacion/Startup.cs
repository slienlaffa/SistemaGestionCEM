using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaGestionCEM.Presentacion.Startup))]
namespace SistemaGestionCEM.Presentacion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Cambio 2
        }
    }
}
