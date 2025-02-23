using FlashHackForum.Models;

namespace FlashHackForum.Data.Interfaces
{
    public interface ISecondCategoryRepository : IRepository<SecondCategory>
    {
        Task<SecondCategory> GetByCategoryIdIncludeThreads(int id);
        Task<SecondCategory> GetByCategoryNameIncludeThreads(string name);
    }
}
