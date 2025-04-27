using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Abstracts.Repositories
{
    public interface IQuestionnairesRepository
    {
        Task<List<Questionnaires>>  GetQuestionnairesMasterList();

        Task<Questionnaires> GetQuestionnairesMasterById(int qId);

        Task<int> CreateQuestionnaire(QuestionnairePortal qpJson);
        
        Task<Questionnaires> AddQuestionnaires(Questionnaires addQuestionnaires);

        Task<QuestionnairePortal> GetQuestionnaireById(int qId);

        Task<bool> DeleteQuestionnaire(int qId);

        Task<int> SaveQuestionnaireAnswers(QuestionnaireAnswer qaJson);

        Task<List<QuestionnaireAnswer>> GetVisitorDetailsByDate(DateTime startDate, DateTime endDate);
    }
}
