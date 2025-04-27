using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.ContentManagement;
using sms.space.management.domain.Entities.PlayerManagement;

namespace sms.space.management.application.Services
{
    public class PlayListService : IPlayListService
    {
        private readonly IPlayListRepository _repository;

        public PlayListService(IPlayListRepository repository)
        {
            _repository = repository;
        }

        public async Task<PlayList> Create(PlayList request)
        {
            var playList = await _repository.Create(request);
            return playList;
        }

        public async Task<bool> Delete(string playListName)
        {
            var isdeleted = await _repository.Delete(playListName);
            return isdeleted;
        }

        public async Task<bool> DeletePlayListItem(int id)
        {
            var isdeleted = await _repository.DeletePlayListItem(id);
            return isdeleted;
        }

        public async Task<IReadOnlyList<PlayList>> GetAll()
        {
            var playList = await _repository.GetAll();
            return playList;
        }

        public async Task<PlayList> GetById(int id)
        {
            var playList = await _repository.GetById(id);
            return playList;
        }
        public async Task<IReadOnlyList<PlayList>> GetByPlayListName(string playListName)
        {
            List<PlayList> pList = null;
            var playList = await _repository.GetByPlayListName(playListName);
            if(playList !=null )
            {
                pList = playList.ToList();
                pList.ForEach(x =>
                {
                    x.duration = new Duration();
                    if (x.DurationType == "full")
                    {
                        x.duration.full = "10:00";
                    }
                    else
                    {
                        x.duration.part = x.PlayDuration;
                    }

                });
            }
            return pList;
        }

        public async Task<bool> Update(List<PlayList> request)
        {
            var modifiedRecords = request.ToList().FindAll(x => x.ActionInd == "M");
            if (modifiedRecords != null && modifiedRecords.Count > 0)
            {
                var isupdated = await _repository.Update(modifiedRecords);
            }
            var addedRecords = request.ToList().FindAll(x => x.ActionInd == "A");
            if (addedRecords != null && addedRecords.Count > 0)
            {
                var isAdded = await _repository.CreateMultiple(addedRecords);
            }
            return true;
        }
    }
}
