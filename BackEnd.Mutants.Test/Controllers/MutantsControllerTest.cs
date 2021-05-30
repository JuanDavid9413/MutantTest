using BackEnd.Mutants.Entities.interfaces.business;
using BackEnd.Mutants.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Mutants.Controllers.v1;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Mutants.Test.Controllers
{
    [TestClass]
    public class MutantsControllerTest
    {
        private readonly Mock<IMutantsBusiness> _mutantsBusiness = new Mock<IMutantsBusiness>();
        private MutantsController mutantsController;

        [TestInitialize]
        public void InitializateData()
        {
            mutantsController = new MutantsController(_mutantsBusiness.Object);
        }

        [TestMethod]
        [DataRow(1, 200, DisplayName = "GetMutantTest_Ok")]
        [DataRow(2, 400, DisplayName = "GetMutantTest_BatRequets")]
        [DataRow(3, 500, DisplayName = "GetMutantTest_InternalServerError")]
        public async Task GetMutantTest(int position, int status)
        {
            switch (position)
            {
                case 1:
                    _mutantsBusiness.Setup(l => l.GetMutants())
                        .Returns(Task.FromResult(new Entities.Models.MutantsResponse { count_mutant_dna = 40, count_human_dna = 10, ratio = 1.2, Code = 200, Message = "Solicitud Ok" }));
                    break;
                case 2:
                    _mutantsBusiness.Setup(l => l.GetMutants())
                        .Returns(Task.FromResult(new Entities.Models.MutantsResponse { Code = 400, Message = "Ningun Item Encontrado" }));
                    break;
                case 3:
                    _mutantsBusiness.Setup(l => l.GetMutants())
                        .Returns(Task.FromResult(new Entities.Models.MutantsResponse { Code = 500, Message = "Launch Error" }));
                    break;
            }

            var result = await mutantsController.GetMutant();
            var response = (ObjectResult)result;
            Assert.AreEqual(status, (int)response.StatusCode);
        }

        [TestMethod]
        [DataRow(1, 200, DisplayName = "CreateMutants_Ok")]
        [DataRow(2, 403, DisplayName = "CreateMutants_Forbieen")]
        [DataRow(3, 400, DisplayName = "CreateMutants_BadRequest")]
        [DataRow(4, 500, DisplayName = "CreateMutants_InternalServerError")]

        public async Task CreateMutants(int position, int status)
        {
            switch (position)
            {
                case 1:
                    _mutantsBusiness.Setup(l => l.IsMutant(It.IsAny<DnaRequest>()))
                        .Returns(Task.FromResult(new Entities.Response.ResponseBase<bool> { Code = 200, Data = true }));
                    break;
                case 2:
                    _mutantsBusiness.Setup(l => l.IsMutant(It.IsAny<DnaRequest>()))
                        .Returns(Task.FromResult(new Entities.Response.ResponseBase<bool> { Code = 403, Data = true }));
                    break;
                case 3:
                    _mutantsBusiness.Setup(l => l.IsMutant(It.IsAny<DnaRequest>()))
                        .Returns(Task.FromResult(new Entities.Response.ResponseBase<bool> { Code = 400 }));
                    break;
                case 4:
                    _mutantsBusiness.Setup(l => l.IsMutant(It.IsAny<DnaRequest>()))
                        .Returns(Task.FromResult(new Entities.Response.ResponseBase<bool> { Code = 500, Data = false }));
                    break;
            }

            var result = await mutantsController.IsMutant(It.IsAny<DnaRequest>());
            var response = (ObjectResult)result;
            Assert.AreEqual(status, (int)response.StatusCode);
        }
    }
}
