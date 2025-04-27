using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Category
{
    public class Category
    {
        public int CategoryTxnId { get; set; }
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName{ get; set; }
        public string Email { get; set; }
    }
}
