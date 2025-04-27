using Microsoft.EntityFrameworkCore;

namespace sms.space.management.data.access
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options)
            : base(options)
        {
        }

        public DbSet<sms.space.management.domain.Entities.Organization.Organization> Organization { get; set; } = default!;

        public DbSet<sms.space.management.domain.Entities.Organization.TestDevs> TestDevs { get; set; }
        public DbSet<sms.space.management.domain.Entities.Organization.Industry> Industry { get; set; }

        public DbSet<sms.space.management.domain.Entities.Organization.Facilities> Facilities { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.Floor> Floor { get; set; }

        public DbSet<sms.space.management.domain.Entities.Organization.City> City { get; set; }

        public DbSet<sms.space.management.domain.Entities.Organization.State> State { get; set; }

        public DbSet<sms.space.management.domain.Entities.Organization.Country> Country { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.Building> Building { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.Room> Room { get; set; }

        public DbSet<sms.space.management.domain.Entities.Common.Calendar> Calendar { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.Infrastructure> Infrastructure { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.Space> Space { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.SpaceType> SpaceType { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.Desk> Desk { get; set; }

        public DbSet<sms.space.management.domain.Entities.Building.SupportGroup> SupportGroup { get; set; }

        public DbSet<sms.space.management.domain.Entities.QRCode.qrcodedetail> QRCodeRequestEntity { get; set; }


    }
}
