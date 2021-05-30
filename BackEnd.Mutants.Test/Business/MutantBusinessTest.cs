using BackEnd.Mutants.Business;
using BackEnd.Mutants.Entities.interfaces.repository;
using BackEnd.Mutants.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Mutants.Test.Business
{
    [TestClass]
    public class MutantBusinessTest
    {
        private readonly Mock<IMutantsRepository> _mutantsRepository = new Mock<IMutantsRepository>();
        private MutantsBusiness mutantsBusiness;

        [TestInitialize]
        public void InitializateData()
        {
            mutantsBusiness = new MutantsBusiness(_mutantsRepository.Object);
        }

        [TestMethod]
        [DataRow(1, 200, DisplayName = "GetMutants_Ok")]
        [DataRow(2, 400, DisplayName = "GetMutants_BadRequest")]
        [DataRow(3, 500, DisplayName = "GetMutants_InternalServerError")]
        public async Task GetMutants(int position, int status)
        {
            switch (position)
            {
                case 1:
                    _mutantsRepository.Setup(l => l.GetMutants())
                        .Returns(Task.FromResult(new List<Mutants.Entities.DbSet.Mutants> { new Mutants.Entities.DbSet.Mutants { Id = 1, IsMutant = true } }));
                    break;
                case 2:
                    _mutantsRepository.Setup(l => l.GetMutants())
                        .Returns(Task.FromResult(new List<Mutants.Entities.DbSet.Mutants> { }));
                    break;
                case 3:
                    _mutantsRepository.Setup(l => l.GetMutants())
                        .ThrowsAsync(new Exception("launch error"));
                    break;
            }

            var response = await mutantsBusiness.GetMutants();
            Assert.AreEqual(status, response.Code);
        }

        [TestMethod]
        [DataRow(1, 200, DisplayName = "GetMutants_Ok")]
        [DataRow(2, 400, DisplayName = "GetMutants_BadRequest")]
        [DataRow(3, 500, DisplayName = "GetMutants_InternalServerError")]
        public async Task CreateMutants(int position, int status)
        {
            switch(position)
            {
                case 1:
                    _mutantsRepository.Setup(l => l.CreateMutants(It.IsAny<Entities.DbSet.Mutants>()))
                        .Returns(Task.FromResult(true));
                    break;
                case 2:
                    _mutantsRepository.Setup(l => l.CreateMutants(It.IsAny<Entities.DbSet.Mutants>()))
                        .Returns(Task.FromResult(false));
                    break;
                case 3:
                    _mutantsRepository.Setup(l => l.CreateMutants(It.IsAny<Entities.DbSet.Mutants>()))
                        .ThrowsAsync(new Exception("Launch error"));
                    break;
            }

            var response = await mutantsBusiness.IsMutant(It.IsAny<DnaRequest>());
            Assert.AreEqual(status, response.Code);
        }
    }
}
