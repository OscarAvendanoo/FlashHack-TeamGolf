using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface ICompanyRepository : IRepository<Company>
    {
        Task<Company> GetCompanyByNameIncludeCompetenses(string CompanyName);
        Task<Company> GetCompanyByIdIncludeCompetenses(int CompanyId);
    }
}
