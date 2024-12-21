using CharityHub.Domain.Entities;
using CharityHub.Domain.Entities.Identities;
using CharityHub.Infrastructure.Data.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CharityHub.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        #region Fileds
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Invitation> Invitations { get; set; }

        
        #endregion

        #region Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        #endregion

        #region Functions

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeviceConfig).Assembly);
        }
        #endregion

    }
}
