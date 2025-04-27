using sms.space.management.application.Abstracts.Repositories;
using Dapper;
using sms.space.management.data.access.BusinessLogic;
using sms.space.management.domain.Entities.Organization;
using static Dapper.SqlMapper;
using sms.space.management.domain.Entities;
using sms.space.management.domain.Entities.UserManagement;
using sms.space.management.domain.Entities.Theme;

namespace sms.space.management.data.access.Repositories
{
    public class QuestionnairesRepository : IQuestionnairesRepository
    {
        private readonly DbSession _session;

        public QuestionnairesRepository(DbSession session) 
        {
            _session = session;
        }

        public async Task<List<Questionnaires>> GetQuestionnairesMasterList()
        {
            //Get All
            var query = @"SELECT * FROM space_admin.questionnaires_master";

            var result = await _session.Connection.QueryAsync<Questionnaires>(query, _session.Transaction);
            return result.ToList();
        }

        public async Task<Questionnaires> GetQuestionnairesMasterById(int qId)
        {
            //Get All
            var query = @"SELECT * FROM space_admin.questionnaires_master where q_id=@QID";

            var result = await _session.Connection.QueryAsync<Questionnaires>(query, new { QId = qId }, _session.Transaction);
            return result.FirstOrDefault();
        }

        public async Task<int> CreateQuestionnaire(QuestionnairePortal qpJson)
        {
            //Insert query
            string query = "";
            if (qpJson.QDefault)
            {
                query = "UPDATE space_admin.questionnaires_portal SET  q_default=false;";
            }
            query = query + $@"INSERT INTO space_admin.questionnaires_portal(q_name,q_default,q_json)
							VALUES (@QName,@QDefault, @QJson)
							RETURNING q_id
							";
            //RETURNING id

            int qpId = await _session.Connection.ExecuteScalarAsync<int>(query, qpJson, _session.Transaction);

            return qpId;


        }

        public async Task<Questionnaires> AddQuestionnaires(Questionnaires addQuestionnaires)
        {
            var query = $@"INSERT INTO space_admin.questionnaires_master(q_text,q_type,q_type_value,q_soft_delete) 
                            VALUES(@QText,@QType,@QTypeValue,@QSoftDelete)
						RETURNING q_id			
            ";
            //RETURNING id


            addQuestionnaires.QId = await _session.Connection.ExecuteScalarAsync<int>(query, addQuestionnaires, _session.Transaction);
            return addQuestionnaires;
        }

        public async Task<QuestionnairePortal> GetQuestionnaireById(int qId)
        {
            var query = $@"Select * from space_admin.questionnaires_portal where  q_default=true";
            var result = await _session.Connection.QueryAsync<QuestionnairePortal>(query, new { QId = qId }, _session.Transaction);
            return result.FirstOrDefault();
        }

        public async Task<bool> DeleteQuestionnaire(int qId)
        {
            var query = "Delete from space_admin.questionnaires_master where q_id=@QId";
            var result = await _session.Connection.ExecuteAsync(query, new { QId = qId }, _session.Transaction);
            if (result > 0)
            {
                return true;
            }
            else return false;
        }

        public async Task<int> SaveQuestionnaireAnswers(QuestionnaireAnswer qaJson)
        {
            //Insert query
            var query = $@"INSERT INTO space_admin.visitor_portal(visit_date,vp_json,visitor_photo)
							VALUES (@VisitDate,@VpJson,@VisitorPhoto)
							RETURNING vp_id
							";
            //RETURNING id

            int qpId = await _session.Connection.ExecuteScalarAsync<int>(query, qaJson, _session.Transaction);

            return qpId;

        }

        public async Task<List<QuestionnaireAnswer>> GetVisitorDetailsByDate(DateTime startDate, DateTime endDate)
        {
            var query = $@"Select * from space_admin.visitor_portal where  visit_date  between '{startDate.ToString("yyyy-MM-dd")}' and '{endDate.ToString("yyyy-MM-dd")}';";
            var result = await _session.Connection.QueryAsync<QuestionnaireAnswer>(query, _session.Transaction);
            return result.ToList();
        }
    }
}
