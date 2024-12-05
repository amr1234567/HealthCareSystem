using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddServiceLayerServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
 