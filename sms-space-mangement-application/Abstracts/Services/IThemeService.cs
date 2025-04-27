using sms.space.management.domain.Entities.Building;
using sms.space.management.domain.Entities.Theme;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IThemeService
    {
        Task<ManageTheme> GetById(int id);

        Task<IReadOnlyList<ManageTheme>> GetAll();

        Task<ManageTheme> Create(ManageTheme request);

        Task<bool> Update(ManageTheme request);

        Task<bool> Delete(int id);

        Task<IReadOnlyList<ThemeLanguage>> GetAllLanguages();

        Task<IReadOnlyList<ThemeFont>> GetThemeFont();

    }

}

