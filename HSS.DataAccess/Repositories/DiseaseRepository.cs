using HSS.DataAccess.Contexts;
using HSS.Domain.Abstractions;
using HSS.Domain.Enums;
using HSS.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace HSS.DataAccess.Repositories
{
    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly ApplicationDbContext _context;

        public DiseaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Disease> GetByIdAsync(int id)
        {
            return await _context.Diseases.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Disease>> GetAllAsync()
        {
            return await _context.Diseases.ToListAsync();
        }

        public async Task<IEnumerable<Disease>> SearchAsync(string name, string code)
        {
            return await _context.Diseases
                .Where(d => d.Name.Contains(name) || d.DiseaseCode.Contains(code))
                .ToListAsync();
        }

        public async Task<IEnumerable<Disease>> FilterAsync(Severity? severity, bool? contagious, int? affectedPopulation)
        {
            return await _context.Diseases
                .Where(d =>
                    (!severity.HasValue || d.Severity == severity) &&
                    (!contagious.HasValue || d.Contagious == contagious) &&
                    (!affectedPopulation.HasValue || d.AffectedPopulation >= affectedPopulation))
                .ToListAsync();
        }

        public async Task AddAsync(Disease disease)
        {
            await _context.Diseases.AddAsync(disease);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Disease disease)
        {
            _context.Diseases.Update(disease);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var disease = await _context.Diseases.FindAsync(id);
            if (disease != null)
            {
                _context.Diseases.Remove(disease);
                await _context.SaveChangesAsync();
            }
        }
    }
}
