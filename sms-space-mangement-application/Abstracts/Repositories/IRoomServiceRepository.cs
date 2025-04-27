using sms.space.management.domain.Entities.RoomService;


namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IRoomServiceRepository
    {
        Task<IReadOnlyList<RoomServiceDetail>> GetRoomServices();
        Task<RoomServiceDetail> GetRoomService(int roomServiceId);
        Task<RoomServiceDetail> CreateRoomService(RoomServiceDetail request);
        Task<bool> UpdateRoomService(RoomServiceDetail request);
        Task<bool> DeleteRoomService(int roomServiceId);
    }
}
