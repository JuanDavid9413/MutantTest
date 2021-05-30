using BackEnd.Mutants.Business.Processors;
using BackEnd.Mutants.Entities.interfaces.business;
using BackEnd.Mutants.Entities.interfaces.repository;
using BackEnd.Mutants.Entities.Models;
using BackEnd.Mutants.Entities.Response;
using BackEnd.Mutants.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Mutants.Business
{
    public class MutantsBusiness : IMutantsBusiness
    {
        private readonly IMutantsRepository _repository;

        public MutantsBusiness(IMutantsRepository repository)
        {
            _repository = repository;
        }

        public async Task<MutantsResponse> GetMutants()
        {
            var response = new MutantsResponse();
            try
            {
                var result = await _repository.GetMutants().ConfigureAwait(true);
                if (result.Count > 0)
                {
                    response.Code = (int)HttpStatusCode.OK;
                    response.Message = "Solicitud OK";
                    response.count_mutant_dna = result.Where(l => l.IsMutant).Count();
                    response.count_human_dna = result.Where(l => l.IsMutant).Count();
                    response.ratio = 1.22222;
                }
                else
                {
                    response.Code = (int)HttpStatusCode.BadRequest;
                    response.Message = "Ningun registro encontrado";
                }
            }
            catch (Exception ex)
            {
                response.Code = (int)HttpStatusCode.InternalServerError;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseBase<bool>> IsMutant(DnaRequest dnaRequest )
        {
            var response = new ResponseBase<bool>();
            try
            {
                int countPatronMutant = 0;
                var dimension = MutantsBusinessProccesor.ExtractDimensionMatriz(dnaRequest.dna);
                if (dimension[0] != 0 && dimension[1] != 0)
                {
                    for (int i = 1; i <= 2; i++)
                    {
                        var dnaHorizontal = MutantsBusinessProccesor.ConvertDataBidimentional(dnaRequest.dna, i);
                        var resultMutant = MutantsBusinessProccesor.ValidateMutant(dnaHorizontal, dimension);
                        countPatronMutant += resultMutant;
                    }

                    if (countPatronMutant > 1)
                    {
                        //Registra en base de datos si es mutante
                        response.Data = await _repository.CreateMutants(new Entities.DbSet.Mutants { IsMutant = true });
                        response.ResponseBaseCatch<bool>(true, "El patron DNA analizado es mutante", HttpStatusCode.OK);
                    }
                    else
                    {
                        //Registra en base de datos si no es mutante
                        response.Data = await _repository.CreateMutants(new Entities.DbSet.Mutants { IsMutant = false });
                        response.ResponseBaseCatch<bool>(true, "El patron DNA analizado no es mutante", HttpStatusCode.Forbidden);
                    }
                }
                else
                    response.ResponseBaseCatch<bool>(true, "El patron DNA viene vacio.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                response.ResponseBaseCatch<bool>(true, ex.Message, HttpStatusCode.InternalServerError);
            }
            return response;
        }
    }
}
