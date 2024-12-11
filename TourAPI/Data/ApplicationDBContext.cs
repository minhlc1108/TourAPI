using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TourAPI.Models;

namespace TourAPI.Data
{
    public class ApplicationDBContext : IdentityDbContext<Account>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(RelationalEventId.PendingModelChangesWarning)
            );
        }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TourImage> TourImages { get; set; }
        public DbSet<TourSchedule> TourSchedules { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingDetail> BookingDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Promotion>()
                .HasKey(p => p.Id);

            builder.Entity<Promotion>()
                .HasIndex(p => p.Code)
                .IsUnique(true);

            builder.Entity<Customer>()
                .HasOne(c => c.Account)
                .WithOne()
                .HasForeignKey<Customer>(c => c.AccountId);

            builder.Entity<Customer>()
                .HasOne(c => c.RelatedCustomer)
                .WithOne()
                .HasForeignKey<Customer>(c => c.RelatedCustomerId);

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
                .HasOne(bd => bd.Booking)
                .WithMany(b => b.BookingDetails)
                .HasForeignKey(bd => bd.BookingId)  
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BookingDetail>()
                .HasOne(bd => bd.Customer)
                .WithMany(c => c.BookingDetails)
                .HasForeignKey(bd => bd.CustomerId)
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