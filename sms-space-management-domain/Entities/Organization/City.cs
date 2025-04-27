using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Organization;
public class City
{
    public int ID { get; set; }

    public string Name { get; set; }

    public State State { get; set; }

    public string ZipCode { get; set; } 

}
