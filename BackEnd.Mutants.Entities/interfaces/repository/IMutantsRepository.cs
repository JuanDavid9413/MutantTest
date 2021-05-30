using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Mutants.Entities.interfaces.repository
{
    public interface IMutantsRepository
    {
        Task<List<Mutants.Entities.DbSet.Mutants>> GetMutants();
        Task<bool> CreateMutants(Mutants.Entities.DbSet.Mutants mutants);
    }
}
