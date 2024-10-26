using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TourAPI.Models;

namespace TourAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<Account>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Tour> Tours { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TransportDetail> TransportDetails { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<TourSchedule> TourSchedules { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<TourPromotion> TourPromotions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>()
            .HasOne(c => c.Account)
            .WithOne()
            .HasForeignKey<Customer>(c => c.AccountId);

            builder.Entity<Customer>()
            .HasOne(c => c.RelatedCustomer)
            .WithOne()
            .HasForeignKey<Customer>(c => c.RelatedCustomerId);

            builder.Entity<TransportDetail>(x => x.HasKey(td => new { td.TransportId, td.TourScheduleId }));

            builder.Entity<TransportDetail>()
                .HasOne(td => td.Transport)
                .WithMany(t => t.TransportDetails)
                .HasForeignKey(td => td.TransportId);

            builder.Entity<TransportDetail>()
                .HasOne(td => td.TourSchedule)
                .WithMany(ts => ts.TransportDetails)
                .HasForeignKey(td => td.TourScheduleId);

            builder.Entity<TourPromotion>(x => x.HasKey(tp => new { tp.PromotionId, tp.TourScheduleId }));

            builder.Entity<TourPromotion>()
                .HasOne(tp => tp.Promotion)
                .WithMany(p => p.TourPromotions)
                .HasForeignKey(tp => tp.PromotionId);

            builder.Entity<TourPromotion>()
                .HasOne(tp => tp.TourSchedule)
                .WithMany(ts => ts.TourPromotions)
                .HasForeignKey(tp => tp.TourScheduleId);

            builder.Entity<Booking>(x => x.HasKey(b => b.Id));

            builder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(bd => bd.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Booking>()
                            .HasOne(b => b.TourSchedule)
                            .WithMany(ts => ts.Bookings)
                            .HasForeignKey(b => b.TourScheduleId)
                             .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<BookingDetail>(x => x.HasKey(bd => new { bd.BookingId, bd.CustomerId }));

            builder.Entity<BookingDetail>()
                .HasOne(bd => bd.Customer)
                .WithMany(c => c.BookingDetails)
                .HasForeignKey(bd => bd.CustomerId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<BookingDetail>()
                            .HasOne(bd => bd.Booking)
                            .WithMany(c => c.BookingDetails)
                            .HasForeignKey(bd => bd.BookingId)
                             .OnDelete(DeleteBehavior.NoAction);

            List<IdentityRole> roles = new List<IdentityRole> {
                new IdentityRole {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}