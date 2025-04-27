using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.UserManagement
{
    public class Role
    {
        public int RoleId { get; set; }
        //parent table user management Id
        public int UserId { get; set; }

        public string RoleName { get; set; }

        public string RoleBase { get; set; }

    }
}
