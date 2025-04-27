using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.application.Models.Dtos.Spaces
{
    public class UpdateSpacesDto
    {
        public int Id { get; set; }
        public int FloorId { get; set; }
        public string? SpaceType { get; set; }
        public string? SpaceAliasName { get; set; }
        public string[]? MappedCalendarIds { get; set; }
        public string? Groupname { get; set; }
        public string[]? MappedConnectorIds { get; set; }
        public string? Email { get; set; }
        public string? DirectionNotes { get; set; }
        public string? ServicingFacilities { get; set; }
        public string? Coordinates { get; set; }
        public string? Workweekdays { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public string? Resources { get; set; }
        public string? Role { get; set; }

    }
}
