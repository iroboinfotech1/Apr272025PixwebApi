using Dapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Category;
using sms.space.management.domain.Entities.RoomService;
using sms.space.management.domain.Entities.Schedule;

namespace sms.space.management.data.access.Repositories
{
    public class RoomServiceRepository: IRoomServiceRepository
    {
        private readonly DbSession _session;
        public RoomServiceRepository(DbSession session)
        {
            _session = session;
        }

        public async Task<IReadOnlyList<RoomServiceDetail>> GetRoomServices()
        {
            var query = $@"SELECT room_service_id,item_id,space_admin.troom_service.item_name,item_image,space_admin.troom_service.category_id,space_admin.troom_service.building_id,space_admin.troom_service.space_id,space_admin.troom_service.schedule_id FROM space_admin.troom_service
                        INNER JOIN space_admin.tcategory ON space_admin.tcategory.category_id = space_admin.troom_service.category_id 
                        INNER JOIN space_admin.buildings_master ON space_admin.buildings_master.building_id = space_admin.troom_service.building_id 
                        INNER JOIN space_admin.space_master ON space_admin.space_master.space_id = space_admin.troom_service.space_id 
                        INNER JOIN space_admin.tschedule ON space_admin.tschedule.schedule_id = space_admin.troom_service.schedule_id ";
            var result = await _session.Connection.QueryAsync<RoomServiceDetail>(query, null, _session.Transaction);
            return result.ToList();
        }

        public async Task<RoomServiceDetail> GetRoomService(int roomServiceId)
        {
            var query = $@"SELECT room_service_id,item_id,space_admin.troom_service.item_name,item_image,space_admin.troom_service.category_id,space_admin.troom_service.building_id,space_admin.troom_service.space_id,space_admin.troom_service.schedule_id 
                        FROM space_admin.troom_service 
                        INNER JOIN space_admin.tcategory ON space_admin.tcategory.category_id = space_admin.troom_service.category_id 
                        INNER JOIN space_admin.buildings_master ON space_admin.buildings_master.building_id = space_admin.troom_service.building_id 
                        INNER JOIN space_admin.space_master ON space_admin.space_master.space_id = space_admin.troom_service.space_id 
                        INNER JOIN space_admin.tschedule ON space_admin.tschedule.schedule_id = space_admin.troom_service.schedule_id 
                        where room_service_id=@roomServiceId";
            var result = await _session.Connection.QueryAsync<RoomServiceDetail>(query, new { room_service_id = roomServiceId }, _session.Transaction);
            return result.FirstOrDefault();
        }
       
        public async Task<RoomServiceDetail> CreateRoomService(RoomServiceDetail request)
        {
            var query = $@"INSERT INTO  space_admin.troom_service (item_id,Item_name,item_image,category_id,building_id,space_id,schedule_id)
						VALUES (@ItemId,@ItemName,@ItemImage,@CategoryId,@BuildingId,@SpaceId,@ScheduleId)
						RETURNING room_service_id			
            ";


            request.RoomServiceId = await _session.Connection.ExecuteScalarAsync<int>(query, request, _session.Transaction);
            return request;
        }

        public async Task<bool> UpdateRoomService(RoomServiceDetail request)
        {
            var query = $@"UPDATE space_admin.troom_service 
                        SET 
                        item_id = @ItemId,
                        Item_name = @ItemName,
                        item_image = @ItemImage
                        category_id = @CategoryId,
                        building_id = @BuildingId,
                        space_id = @SpaceId,
                        schedule_id=@ScheduleId
                        WHERE room_service_id = @roomServiceId";
            var result = await _session.Connection.ExecuteAsync(query, new
            {
                item_id = request.ItemId,
                Item_name = request.ItemImage,
                item_image = request.ItemImage,
                category_id = request.CategoryId,
                building_id = request.BuildingId,
                space_id = request.SpaceId,
                schedule_id = request.ScheduleId

            }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> DeleteRoomService(int roomServiceId)
        {
            var query = "Delete from space_admin.troom_service where room_service_id=@roomServiceId";
            var result = await _session.Connection.ExecuteAsync(query, new { room_service_id = roomServiceId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }
    }
}
