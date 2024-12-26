using HSS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Services.LabTest
{
    public interface ILabTestService
    {
        Task<IEnumerable<LabCenterTest>> GetAllTestForUser(string userId);
        Task AddLabTestForUser(LabCenterTest labCenterTest);
        Task<LabCenterTest> GetTestById(int id);
        Task UpdateTest(LabCenterTest test);
        Task DeleteTest(int id);
    }
}
