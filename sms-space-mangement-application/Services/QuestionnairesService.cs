using AutoMapper;
using sms.space.management.application.Abstracts.Repositories;
using sms.space.management.application.Abstracts.Services;
using sms.space.management.application.Models.Dtos.Facilities;
using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.Organization;
using sms.space.management.domain.Entities.UserManagement;

namespace sms.space.management.application.Services
{

    public class QuestionnairesService : IQuestionnairesService
    {
        private readonly IQuestionnairesRepository _repository;
        private readonly IMapper _mapper;

        public QuestionnairesService(IMapper mapper, IQuestionnairesRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IReadOnlyList<Questionnaires>> GetQuestionnairesMasterList()
        {
            var questionnaires = await _repository.GetQuestionnairesMasterList();
            return _mapper.Map<List<Questionnaires>>(questionnaires);
        }

        public async Task<Questionnaires> GetQuestionnairesMasterById(int qId)
        {
            var questionnaires = await _repository.GetQuestionnairesMasterById(qId);
            return _mapper.Map<Questionnaires>(questionnaires);
        }

        public async Task<int> CreateQuestionnaire(QuestionnairePortal qpJson)
        {
            var qpId = await _repository.CreateQuestionnaire(qpJson);
            return qpId;
        }

        public async Task<Questionnaires> AddQuestionnaires(Questionnaires addQuestionnaires)
        {
            var questionnaires = await _repository.AddQuestionnaires(addQuestionnaires);
            return questionnaires;
        }

        public async Task<QuestionnairePortal> GetQuestionnaireById(int qId)
        {
            var questionnairePortal = await _repository.GetQuestionnaireById(qId);
            return _mapper.Map<QuestionnairePortal>(questionnairePortal);
        }
        public async Task<bool> DeleteQuestionnaire(int qId)
        {
            var isdeleted = await _repository.DeleteQuestionnaire(qId);
            return isdeleted;
        }

        public async Task<int> SaveQuestionnaireAnswers(QuestionnaireAnswer qaJson)
        {
            var qpId = await _repository.SaveQuestionnaireAnswers(qaJson);
            return qpId;
        }

        public async Task<List<QuestionnaireAnswer>> GetVisitorDetailsByDate(DateTime startDate, DateTime endDate)
        {
            var visitorDetails = await _repository.GetVisitorDetailsByDate(startDate, endDate);
            return _mapper.Map<List<QuestionnaireAnswer>>(visitorDetails);
        }
    }
}
