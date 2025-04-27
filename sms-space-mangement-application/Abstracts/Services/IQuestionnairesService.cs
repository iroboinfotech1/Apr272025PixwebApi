using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.Organization;
using sms.space.management.domain.Entities.UserManagement;

namespace sms.space.management.application.Abstracts.Services
{
    public interface IQuestionnairesService
    {
        Task<IReadOnlyList<Questionnaires>> GetQuestionnairesMasterList();

        Task<Questionnaires> GetQuestionnairesMasterById(int qId);

        Task<int> CreateQuestionnaire(QuestionnairePortal qpJson);
        
        Task<Questionnaires> AddQuestionnaires(Questionnaires addQuestionnaires);

        Task<bool> DeleteQuestionnaire(int qId);

        Task<QuestionnairePortal> GetQuestionnaireById(int qId);
        
        Task<int> SaveQuestionnaireAnswers(QuestionnaireAnswer qaJson);
        Task<List<QuestionnaireAnswer>> GetVisitorDetailsByDate(DateTime startDate, DateTime endDate);
    }
}
