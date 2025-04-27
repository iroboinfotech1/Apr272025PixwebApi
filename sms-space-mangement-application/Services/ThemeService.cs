using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.domain.Entities.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Services
{
    public class ThemeService : IThemeService
    {

        private readonly IThemeServiceRepository _repository;

        public ThemeService(IThemeServiceRepository repository)
        {
            _repository = repository;
        }
        public async Task<ManageTheme> Create(ManageTheme request)
        {
            var createtheme = await _repository.Create(request);
            return createtheme;
        }

        public async Task<bool> Delete(int id)
        {
            var isdeleted = await _repository.Delete(id);
            return isdeleted;
        }


        public async Task<IReadOnlyList<ManageTheme>> GetAll()
        {
            var themelist = await _repository.GetAll();
            return themelist;
        }

        public async Task<ManageTheme> GetById(int id)
        {
            var getThemedetail = await _repository.GetById(id);
            return getThemedetail;
        }
        public async Task<IReadOnlyList<ThemeLanguage>> GetAllLanguages()
        {
            var getlanguageList = await _repository.GetAllLanguages();
            return getlanguageList;
        }

        public async Task<IReadOnlyList<ThemeFont>> GetThemeFont()
        {
            var getThemeFontList = await _repository.GetThemeFont();
            return getThemeFontList;
        }


        public async Task<bool> Update(ManageTheme request)
        {
            var isupdated = await _repository.Update(request);
            return isupdated;
        }
    }
}


