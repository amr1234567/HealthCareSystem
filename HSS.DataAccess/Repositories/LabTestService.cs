using HSS.DataAccess.Contexts;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSS.Services.Services.LabTest
{
    public class LabTestService : ILabTestService
    {
        private readonly ApplicationDbContext _context;

        public LabTestService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddLabTestForUser(LabCenterTest labCenterTest)
        {
            labCenterTest.UserId = "1";
            await _context.LabCenterTests.AddAsync(labCenterTest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTest(int id)
        {
            _context.LabCenterTests.Remove(await _context.LabCenterTests.FirstOrDefaultAsync(x => x.Id == id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LabCenterTest>> GetAllTestForUser(string userId)
        {
            return await _context.LabCenterTests.Where(x => x.UserId == userId && !x.IsDeleted).ToListAsync();
        }

        public async Task<LabCenterTest> GetTestById(int id)
        {
            return await _context.LabCenterTests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateTest(LabCenterTest test)
        {
            _context.LabCenterTests.Update(test);
            await _context.SaveChangesAsync();
        }

    }
}
