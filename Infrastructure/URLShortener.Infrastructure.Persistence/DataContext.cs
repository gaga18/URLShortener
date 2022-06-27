using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Project.Core.Application.Interfaces.Contracts;
using Project.Core.Domain.Basics;
using Project.Core.Domain.Entities;
using Project.Infrastructure.Persistence.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;
using URLShortener.Core.Domain.Entities;
using URLShortener.Infrastructure.Persistence.Configurations;

namespace Project.Infrastructure.Persistence
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<LinkEntity> Links { get; set; }
        public DbSet<LinkTracking> LinkTracking { get; set; }

        private readonly IActiveUserService user;
        public DataContext(DbContextOptions<DataContext> options, IActiveUserService user)
            : base(options) => this.user = user;

        #region SaveChanges override
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
                this.Audition(entry);

            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
                this.Audition(entry);

            return base.SaveChanges();
        }
        private void Audition(EntityEntry<AuditableEntity> entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    //entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.CreatedBy = user.UserId;
                    break;
                case EntityState.Modified:
                    entry.Entity.DateUpdated = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = user.UserId;

                    // არ შეიცვლება ქვემოთ ჩამოთვლილი ველები
                    entry.Property(nameof(AuditableEntity.CreatedBy)).IsModified = false;
                    entry.Property(nameof(AuditableEntity.DateCreated)).IsModified = false;

                    entry.Property(nameof(AuditableEntity.DeletedBy)).IsModified = false;
                    entry.Property(nameof(AuditableEntity.DateDeleted)).IsModified = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged;

                    // შეიცვლება მხოლოდ ქვემოთ ჩამოთვლილი ველები
                    entry.Entity.DateDeleted = DateTime.UtcNow;
                    entry.Entity.DeletedBy = user.UserId;
                    break;
            };
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new LinkConfiguration());
            modelBuilder.ApplyConfiguration(new LinkTrackingConfiguration());
        }
    }
}
