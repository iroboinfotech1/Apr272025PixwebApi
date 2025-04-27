using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.Category;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<Category>> GetCategorys()
        {
            var categorys = await _repository.GetCategorys();
            return categorys;
        }
        public async Task<Category> GetCategory(int categoryTxnId)
        {
            var category = await _repository.GetCategory(categoryTxnId);
            return category;
        }
        public async Task<Category> CreateCategory(Category request)
        {

            var category = await _repository.CreateCategory(request);
            return category;
        }
        public async Task<bool> UpdateCategory(Category request)
        {
            var isupdated = await _repository.UpdateCategory(request);
            return isupdated;
        }
        public async Task<bool> DeleteCategory(Category request)
        {
            var isdeleted = await _repository.DeleteCategory(request);
            return isdeleted;
        }

      

      
      
    }
}
