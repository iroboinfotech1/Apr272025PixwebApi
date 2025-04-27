using sms.space.management.domain.Entities.Category;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface ICategoryService
    {
        Task<IReadOnlyList<Category>> GetCategorys();
        Task<Category> GetCategory(int categoryTxnId);
        Task<Category> CreateCategory(Category request);
        Task<bool> UpdateCategory(Category request);
        Task<bool> DeleteCategory(Category request);
    }
}
