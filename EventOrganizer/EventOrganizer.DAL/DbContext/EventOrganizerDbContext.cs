using System;
using System.Collections.Generic;
using System.Text;
using EventOrganizer.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventOrganizer.DAL.DbContext
{
    public class EventOrganizerDbContext : IdentityDbContext<User>
    {
        public EventOrganizerDbContext() { }

        public EventOrganizerDbContext(DbContextOptions<EventOrganizerDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(e => e.Events)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<EventCartItem> EventCartItems { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}
