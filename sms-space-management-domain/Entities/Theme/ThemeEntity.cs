using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sms.space.management.domain.Entities.Theme
{


    public class BaseThemeDetails
    {
        public List<ThemeEntity> BaseThemeList { get; set; }

        public string RequestThemeName { get; set; }

        public  int  RequestThemeId { get; set; }
    }

    public class ThemeEntity
    {

        public int ThemeId { get; set; }

        public string ThemeName { get; set; }

        public bool AllowBooking { get; set; }

        public bool ConfirmBooking { get; set; }

        public bool ChangeET { get; set; }

        public bool EndBooking { get; set; }

        public bool ShowOrganizer { get; set; }

        public bool HideSubject { get; set; }

        public bool ShowAppointmentfordays { get; set; }

        public bool FindRoom { get; set; }

        public bool EnableFaultReporting { get; set; }

        public bool AccessSetting { get; set; }


        public bool EnableLedStatus { get; set; }

        public bool ScrollSubject { get; set; }

        public bool SignageonAvailability { get; set; }

        public string  AvailableStatusColor { get; set; }

        public string  OccupiedStatusColor { get; set; }

        public ThemeFont SubjectFont { get; set; }

        public ThemeFont OrganizerFont { get; set; }

        public ThemeFont UpcomingMeetingSubjectFont { get; set; }

        public ThemeFont UpcomingMeetingOrganizerFont { get; set; }

        public MyDictionary WatermarkPlaylist { get; set; }

        public int startSignage { get; set; }

        public int stopSignage { get; set; }

        public string BackgroundLogo { get; set; }

        public string BackgroundColour { get; set; }

        public string SelectedLanguage { get; set; }


    }

    public class ThemeFont
    {
        public string utilityKey { get; set; }

        public string utilityValue { get; set; }

    }

    public class ThemeLanguage
    {
        public string Languagename { get; set; }

        public string Id { get; set; }

    }

    public class MyDictionary
    {
        public string Keyname { get; set; }

        public string Valuename { get; set; }

    }


   public class ManageTheme
    {
        public int Id { get; set; }

        public string themethumbnail { get; set; }

        public string themename { get; set; }

        public string themetype { get; set; }

        public string themedata { get; set; }

        public string logo { get; set; }

        public string background { get; set; }

        //public IFormFile file { get; set; }
    }

      
}



