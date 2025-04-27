using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities
{
    public class Questionnaires
    {
        public int QId { get; set; }
        public string QText { get; set; }
        public string QType { get; set; }
        public string QTypeValue { get; set; }
        public bool QSoftDelete { get; set; }

    }

    public class QuestionnairePortal
    {
        public int QId { get; set; }
        public string QName { get; set; }
        public bool QDefault { get; set; }
        public string QJson { get; set; }

    }

    public class QuestionnaireAnswer
    {
        public int VpId { get; set; }
        public DateTime VisitDate { get; set; }
        public string VpJson { get; set; }

        public string VisitorPhoto { get; set; }
    }
}
