using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.BookMeeting;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class BookRoomServices : IBookRoomServices
    {
        private readonly IBookServicesRepository _repository;
        public BookRoomServices(IBookServicesRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<ServiceDetail>> GetBookServices()
        {
            var services = await _repository.GetBookServices();
            return services;
        }

        public async Task<List<ServiceDetail>> GetBookServices(int meetingId)
        {
            var services = await _repository.GetBookServices(meetingId);
            return services;
        }

        public async Task<ServiceDetail> CreateBookServices(ServiceDetail request)
        {
            var services = await _repository.CreateBookServices(request);
            return services;
        }

        public async Task<bool> UpdateBookServices(List<ServiceDetail> requests)
        {
            foreach (var request in requests)
            {
                if (!string.IsNullOrWhiteSpace(request.Action))
                {
                    switch (request.Action.ToLower())
                    {
                        case "update":
                            await _repository.UpdateBookServices(request);
                            break;
                        case "delete":
                            await _repository.DeleteBookServices(request.ServiceId);
                            break;
                        default:
                            await _repository.CreateBookServices(request);
                            break;
                    }
                }
            }
            return true;
        }

        public async Task<bool> DeleteBookServices(int serviceId)
        {
            var isdeleted = await _repository.DeleteBookServices(serviceId);
            return isdeleted;
        }
    }
}