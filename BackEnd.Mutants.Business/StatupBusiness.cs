using BackEnd.Mutants.Entities.interfaces.business;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Mutants.Business
{
    public static class StatupBusiness
    {
        public static void AddBusiness(this IServiceCollection services)
        {
            services.AddTransient<IMutantsBusiness, MutantsBusiness>();
        }
    }
}
