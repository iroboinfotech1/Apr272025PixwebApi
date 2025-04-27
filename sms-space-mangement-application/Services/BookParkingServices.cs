using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class BookParkingServices : IBookParkingServices
    {
        private readonly IBookParkingRepository _repository;
        public BookParkingServices(IBookParkingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<domain.Entities.BookRoom.BookParking>> GetBookParking()
        {
            var services = await _repository.GetBookParking();
            return services;
        }

        public async Task<List<domain.Entities.BookMeeting.Parkings>> GetBookParking(int meetingId)
        {
            var services = await _repository.GetBookParking(meetingId);
            return services;
        }

        public async Task<domain.Entities.BookMeeting.Parkings> CreateBookParking(domain.Entities.BookMeeting.Parkings request)
        {
            var services = await _repository.CreateBookParking(request);
            return services;
        }

        public Task<bool> UpdateBookParking(domain.Entities.BookRoom.BookParking request)
        {
            var isupdated = _repository.UpdateBookParking(request);
            return isupdated;
        }

        public async Task<bool> DeleteBookParking(int parkingId)
        {
            var isdeleted = await _repository.DeleteBookParking(parkingId);
            return isdeleted;
        }
    }
}