using sms.space.management.domain.Entities.Category;
using sms.space.management.domain.Entities.RoomService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IRoomService
    {
        Task<IReadOnlyList<RoomServiceDetail>> GetRoomServices();
        Task<RoomServiceDetail> GetRoomService(int roomServiceId);
        Task<RoomServiceDetail> CreateRoomService(RoomServiceDetail request);
        Task<bool> UpdateRoomService(RoomServiceDetail request);
        Task<bool> DeleteRoomService(int roomServiceId);
    }
}
