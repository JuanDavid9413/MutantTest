using BackEnd.Mutants.Entities.Models;
using BackEnd.Mutants.Entities.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Mutants.Entities.interfaces.business
{
    public interface IMutantsBusiness
    {
        Task<MutantsResponse> GetMutants();
        Task<ResponseBase<bool>> IsMutant(DnaRequest dnaRequest);
    }
}
