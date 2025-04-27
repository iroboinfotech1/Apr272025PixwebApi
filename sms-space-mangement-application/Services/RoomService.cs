using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.RoomService;

namespace sms.space.management.application.Services
{
    public class RoomService: IRoomService
    {
        private readonly IRoomServiceRepository _repository;
        public RoomService(IRoomServiceRepository repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<RoomServiceDetail>> GetRoomServices()
        {
            var roomServices = await _repository.GetRoomServices();
            return roomServices;
        }
        public async Task<RoomServiceDetail> GetRoomService(int roomServiceId)
        {
            var roomService = await _repository.GetRoomService(roomServiceId);
            return roomService;
        }
        public async Task<RoomServiceDetail> CreateRoomService(RoomServiceDetail request)
        {
            var roomService = await _repository.CreateRoomService(request);
            return roomService;
        }
        public async Task<bool> UpdateRoomService(RoomServiceDetail request)
        {
            var isupdated = await _repository.UpdateRoomService(request);
            return isupdated;
        }
        public async Task<bool> DeleteRoomService(int roomServiceId)
        {
            var isdeleted = await _repository.DeleteRoomService(roomServiceId);
            return isdeleted;
        }
    }
}

