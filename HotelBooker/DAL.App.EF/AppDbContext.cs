using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ee.itcollege.ekmand.Contracts.DAL.Base;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext: IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {

        private IUserNameProvider _userNameProvider;
        
        private readonly Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> _entityTracker =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();
        
        
        public DbSet<Campaign> Campaigns { get; set; } = default!;
        public DbSet<Convenience> Conveniences { get; set; } = default!;
        public DbSet<ConvenienceGroup> ConvenienceGroups { get; set; } = default!;
        public DbSet<Currency> Currencies { get; set; } = default!;
        public DbSet<Hotel> Hotels { get; set; } = default!;
        public DbSet<HotelConvenience> HotelConveniences { get; set; } = default!;
        public DbSet<ImageOfRoom> ImageOfRooms { get; set; } = default!;
        public DbSet<OwnerCompany> OwnerCompanies { get; set; } = default!;
        public DbSet<Person> Person { get; set; } = default!;
        public DbSet<Price> Prices { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductGroup> ProductGroups { get; set; } = default!;
        public DbSet<Reservation> Reservations { get; set; } = default!;
        public DbSet<ReservationRow> ReservationRows { get; set; } = default!;
        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<ReviewCategory> ReviewCategories { get; set; } = default!;
        public DbSet<Room> Rooms { get; set; } = default!;
        public DbSet<RoomType> RoomTypes { get; set; } = default!;
        public DbSet<RoomTypeConvenience> RoomTypeConveniences { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider)
            : base(options)
        {
            _userNameProvider = userNameProvider;
        }
        
        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // disable cascade delete
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            //
            // Person
            //
            builder.Entity<Person>()
                .HasMany(p => p.Users)
                .WithOne(u => u.Person!)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Person>()
                .HasMany(p => p.Reservations)
                .WithOne(u => u.Person!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // OwnerCompany
            //
            builder.Entity<OwnerCompany>()
                .HasMany(o => o.Hotels)
                .WithOne(o => o.OwnerCompany!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // Currency
            //
            builder.Entity<Currency>()
                .HasMany(o => o.Prices)
                .WithOne(o => o.Currency!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // Campaign
            //
            builder.Entity<Campaign>()
                .HasMany(o => o.Prices)
                .WithOne(o => o.Campaign!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // Product
            //
            builder.Entity<Product>()
                .HasMany(o => o.Prices)
                .WithOne(o => o.Product!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Product>()
                .HasOne(o => o.RoomType)
                .WithOne(o => o!.Product!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Product>()
                .HasMany(o => o.ReservationRows)
                .WithOne(o => o.Product!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // ProductGroup
            //
            builder.Entity<ProductGroup>()
                .HasMany(o => o.Products)
                .WithOne(o => o.ProductGroup!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // RoomType
            //
            builder.Entity<RoomType>()
                .HasMany(o => o.Reservations)
                .WithOne(o => o.RoomType!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<RoomType>()
                .HasMany(o => o.Rooms)
                .WithOne(o => o.RoomType!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<RoomType>()
                .HasMany(o => o.ImageOfRooms)
                .WithOne(o => o.RoomType!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<RoomType>()
                .HasMany(o => o.RoomTypeConveniences)
                .WithOne(o => o.RoomType!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<RoomType>()
                .HasMany(o => o.Reviews)
                .WithOne(o => o.RoomType!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // Hotel
            //
            builder.Entity<Hotel>()
                .HasMany(o => o.ImageOfRooms)
                .WithOne(o => o.Hotel!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Hotel>()
                .HasMany(o => o.HotelConveniences)
                .WithOne(o => o.Hotel!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Hotel>()
                .HasMany(o => o.Rooms)
                .WithOne(o => o.Hotel!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Hotel>()
                .HasMany(o => o.Reservations)
                .WithOne(o => o.Hotel!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Hotel>()
                .HasMany(o => o.Prices)
                .WithOne(o => o.Hotel!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Hotel>()
                .HasMany(o => o.Reviews)
                .WithOne(o => o.Hotel!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // AppUser
            //
            builder.Entity<AppUser>()
                .HasMany(o => o.Reservations)
                .WithOne(o => o.User!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<AppUser>()
                .HasMany(o => o.Reviews)
                .WithOne(o => o.User!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // Reservation
            //
            builder.Entity<Reservation>()
                .HasMany(o => o.ReservationRows)
                .WithOne(o => o.Reservation!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // ReviewCategory
            //
            builder.Entity<ReviewCategory>()
                .HasMany(o => o.Reviews)
                .WithOne(o => o.ReviewCategory!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // ConvenienceGroup
            //
            builder.Entity<ConvenienceGroup>()
                .HasMany(o => o.Conveniences)
                .WithOne(o => o.ConvenienceGroup!)
                .OnDelete(DeleteBehavior.Cascade);
            
            //
            // Convenience
            //
            builder.Entity<Convenience>()
                .HasMany(o => o.HotelConveniences)
                .WithOne(o => o.Convenience!)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Entity<Convenience>()
                .HasMany(o => o.RoomTypeConveniences)
                .WithOne(o => o.Convenience!)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
        
        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();
            
            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }
        
        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
    }
}