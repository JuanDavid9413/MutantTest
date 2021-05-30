using BackEnd.Mutants.Entities.interfaces.repository;
using BackEnd.Mutants.Repository.Context;
using BackEnd.Mutants.Repository.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Mutants.Repository
{
    public static class StratupRepository
    {
        public static void AddRepository(this IServiceCollection service, IConfiguration configuration )
        {
            service.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("currentConnetion")));
            service.AddTransient<IMutantsRepository, MutantsRepository>();
        }
    }
}
