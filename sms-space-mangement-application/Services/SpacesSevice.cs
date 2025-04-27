using AutoMapper;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models.Dtos.Spaces;
using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Spaces;
using System.Numerics;

namespace sms.space.management.application.Services
{

    public class SpacesSevice : ISpacesSevice
    {
        private readonly ISpacesRepository _repository;
        private readonly IMapper _mapper;

        public SpacesSevice(IMapper mapper, ISpacesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<GetSpacesDto> Create(Spaces request)
        {
            //var spaces = _mapper.Map<Spaces>(request);

            var spaces = await _repository.Create(request);

            return _mapper.Map<GetSpacesDto>(spaces);
        }

        public async Task<bool> Delete(int id)
        {
            var isdeleted = await _repository.Delete(id);
            return isdeleted;

        }

        public async Task<List<KeyValuePair<string, FloorSpaces>>> FindRooms(SpaceFilter filter)
        {
            var availableSpaces = await _repository.FindRooms(filter);
            //var response = _mapper.Map<List<AvailableSpaceDto>>(availableSpaces);
            return availableSpaces;
        }

        public async Task<IReadOnlyList<GetSpacesDto>> GetAll()
        {
            var spaces = await _repository.GetAll();
            var response = _mapper.Map<List<GetSpacesDto>>(spaces);
            return response;
        }
        public async Task<IReadOnlyList<GetSpacesDto>> GetAllSpacebyFloorId(int floorid)
        {
            var allSpaces = await GetAll();
            var filteredSpaceList = allSpaces.Where(space => space.FloorId == floorid).ToList();
            return filteredSpaceList;
        }   
        public async Task<IReadOnlyList<AvailableSpaceDto>> GetAvailableSpace(SpaceFilter filter)
        {
            var availableSpaces = await _repository.GetAllAvailableSpaces(filter);
            var response = _mapper.Map<List<AvailableSpaceDto>>(availableSpaces);
            return response;
        }

        public async Task<GetSpacesDto> GetById(int id, string basedurl, string endpoint, string queryParamKey)
        {
            string queryParamValue = null;

            var spaces = await _repository.GetById(id);

            if(spaces != null)
            {
                queryParamValue = spaces?.MappedConnectorIds?.FirstOrDefault();
            }
            
            Task<List<CalendarDetails>> calendarDetails = new RestClientService(basedurl).GetDataAsync(endpoint, queryParamKey, queryParamValue);

            GetSpacesDto  dto= _mapper.Map<GetSpacesDto>(spaces);

            if(dto!= null)
            {
                List<string> selectedCalendarId = new List<string>();
                //dto.MappedCalendarIds= calendarDetails?.Result.Select(cd => cd.calendarId.ToString()).ToArray();
                foreach (var item in calendarDetails?.Result)
                {
                    if (item.sourceCalendarId != null && item.sourceCalendarId.Equals(dto.MappedCalendarIds?.FirstOrDefault()))
                    {
                        selectedCalendarId.Add(item.calendarId.ToString());
                    }                    
                }
                if(selectedCalendarId.Count > 0)
                {
                    dto.MappedCalendarIds = selectedCalendarId.ToArray();
                }
            }
            return dto;
        }

        public async Task<bool> SaveSettings(Settings request)
        {
            bool isupdated = await _repository.SaveSettings(request);
            return isupdated;

        }

        public async Task<bool> Update(int id, Spaces request)
        {
            bool isupdated = await _repository.Update(request);
            return isupdated;
        }

    }
}