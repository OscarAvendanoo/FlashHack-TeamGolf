using FlashHackForum.Data.Interfaces;
using FlashHackForum.Models;
using FLashHackForum.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace FlashHackForum.Data
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;

        public CompanyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            _context = applicationDbContext;
        }
        // Hämtar företaget via företagets namn, inkluderar listan med kompetenser dom letar efter samt education som tillhör kompetensen
        public async Task<Company> GetCompanyByIdIncludeCompetenses(int CompanyId)
        {
            return await _context.Companies.Include(c => c.CompetensesToLookFor).ThenInclude(clf => clf.Education).FirstOrDefaultAsync(c => c.CompanyId == CompanyId);
        }
        // Hämtar företaget via företagets ID, inkluderar listan med kompetenser dom letar efter samt education som tillhör kompetensen
        public async Task<Company> GetCompanyByNameIncludeCompetenses(string CompanyName)
        {
            return await _context.Companies.Include(c => c.CompetensesToLookFor).ThenInclude(clf => clf.Education).FirstOrDefaultAsync(c => c.Name == CompanyName);
        }
    }
}
