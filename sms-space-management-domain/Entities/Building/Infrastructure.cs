using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Building;
public class Infrastructure
{
    public int infra_id { get; set; }

    public string infra_name { get; set; }

    public int infracount { get; set; }

    public bool enabled { get; set; }

    public string category { get; set; }

    public Room Room { get; set; }
}
