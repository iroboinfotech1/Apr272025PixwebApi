using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.data.access.Repositories
{
    public class SupportGroupRepository : ISupportGroupRepository
    {
        private readonly DbSession _session;

        public SupportGroupRepository(DbSession session)
        {
            _session = session;
        }

		public async Task<SupportGroup> Create(SupportGroup entity)
		{
			var query = $@"INSERT INTO space_admin.support_group_master(support_group_name, support_group_enable)
						VALUES (@SupportGroupName,@SupportGroupEnable)
						RETURNING support_group_id
						";
			//RETURNING id


			entity.SupportGroupID = await _session.Connection.ExecuteScalarAsync<int>(query, entity, _session.Transaction);

			return entity;
		}

		public async Task<bool> Delete(int id)
		{
			var query = "Delete from space_admin.support_group_master where support_group_id=@ID";
			var result = await _session.Connection.ExecuteAsync(query, new {ID = id}, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}

		

		public async Task<List<SupportGroup>> GetAll()
		{
			var query = "Select * from space_admin.support_group_master";
			var result = await _session.Connection.QueryAsync<SupportGroup>(query, null, _session.Transaction);
			return result.ToList();
		}

		public async Task<SupportGroup?> GetById(int id)
		{
			var query = $@"Select * from space_admin.support_group_master where support_group_id=@ID";
			var result = await _session.Connection.QueryAsync<SupportGroup>(query, new { ID = id }, _session.Transaction);
			return result.FirstOrDefault();
		}

		public async Task<bool> Update(SupportGroup entity)
		{
			var query = $@"UPDATE space_admin.support_group_master
                        SET support_group_name = @SupportGroupName,
                        support_group_enable = @SupportGroupEnable
                        WHERE support_group_id=@ID";
			var result = await _session.Connection.ExecuteAsync(query, new { ID = entity.SupportGroupID,SupportGroupName = entity.SupportGroupName,SupportGroupEnable = entity.SupportGroupEnable
			}, _session.Transaction);
			if (result > 0)
			{
				return true;
			}
			else return false;
		}
	}
}
