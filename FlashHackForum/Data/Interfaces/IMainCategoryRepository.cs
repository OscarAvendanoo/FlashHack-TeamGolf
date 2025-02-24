using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface IMainCategoryRepository : IRepository<MainCategory>
    {
        Task<MainCategory> GetByIdIncludeSecondCategory(int id);
        Task<MainCategory> GetByNameIncludeSecondCategory(string name);
    }
}
