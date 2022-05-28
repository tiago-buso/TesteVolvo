using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteVolvo.Services;

namespace TesteVolvoTestProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDatabaseConfiguration, DatabaseConfiguration>();
            services.AddTransient<IFakeObjects, FakeObjects>();            
        }
    }
}
