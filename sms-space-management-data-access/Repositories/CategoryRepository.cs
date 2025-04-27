using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Category;
using static Dapper.SqlMapper;

namespace sms.space.management.data.access.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbSession _session;
        public CategoryRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IReadOnlyList<Category>> GetCategorys()
        {
            var query = "SELECT category_txn_id,space_admin.tcategory_master.building_id,building_name,space_admin.tcategory_master.category_id,category_name,email" +
                " FROM space_admin.tcategory_master INNER JOIN space_admin.buildings_master ON space_admin.tcategory_master.building_id = space_admin.buildings_master.building_id" +
                " INNER JOIN space_admin.tcategory ON space_admin.tcategory_master.category_id = space_admin.tcategory.category_id";
            var result = await _session.Connection.QueryAsync<Category>(query, null, _session.Transaction);
            return result.ToList();
        }

        public async Task<Category> GetCategory(int categoryTxnId)
        {
            var query = $@"SELECT category_txn_id,space_admin.tcategory_master.building_id,building_name,space_admin.tcategory_master.category_id,category_name 
                        FROM space_admin.tcategory_master 
                        INNER JOIN space_admin.buildings_master ON space_admin.tcategory_master.building_id = space_admin.buildings_master.building_id
                        INNER JOIN space_admin.tcategory ON space_admin.tcategory_master.category_id = space_admin.tcategory.category_id where category_txn_id =@categoryTxnId";
            var result = await _session.Connection.QueryAsync<Category>(query, new { CategoryTxnId = categoryTxnId }, _session.Transaction);
            return result.FirstOrDefault();
        }
        
        public async Task<Category> CreateCategory(Category request)
        {
            var query = $@"INSERT INTO  space_admin.tcategory_master (building_id,category_id,email)
						VALUES (@BuildingId,@CategoryId,@Email)
						RETURNING category_txn_id			
            ";
            //RETURNING id


            request.CategoryTxnId = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
            return request;
        }

        public async Task<bool> UpdateCategory(Category request)
        {
            var query = $@"UPDATE space_admin.tcategory_master 
                        SET 
                        email = @Email 
                        WHERE building_id=@BuildingId and category_Id=@CategoryId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                CategoryId=request.CategoryId,
                BuildingId =request.BuildingId,
                Email = request.Email
            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> DeleteCategory(Category request)
        {
            var query = "Delete from space_admin.tcategory_master  WHERE building_id=@BuildingId and category_id=@CategoryId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                CategoryId = request.CategoryId,
                BuildingId = request.BuildingId,
            }, _session.Transaction);
            
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
