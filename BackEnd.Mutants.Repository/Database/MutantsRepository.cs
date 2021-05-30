using BackEnd.Mutants.Entities.interfaces.repository;
using BackEnd.Mutants.Entities.Models;
using BackEnd.Mutants.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Mutants.Repository.Database
{
    public class MutantsRepository : IMutantsRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MutantsRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<List<Mutants.Entities.DbSet.Mutants>> GetMutants()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                return await context.Mutants.ToListAsync();
            }
        }

        public async Task<bool> CreateMutants(Mutants.Entities.DbSet.Mutants mutants)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                context.Add(mutants);
                var response = await context.SaveChangesAsync();
                return response > 0;
            }
        }
    }
}
