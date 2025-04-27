using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Building;
public class Room
{
    [Key]
    public int ID { get; set; }

    public string Alias { get; set; }

    public string Group { get; set; }

    public List<Common.Calendar> Calendars { get; set; }

    public string DirectionalCoordinates { get; set; }

    public  List<Infrastructure> Infrastructures { get; set; }

    public List<Desk> Desks { get; set; }


}
