using EventOrganizer.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.Data
{
    public class EventOrganizerDbContext : IdentityDbContext<User>
    {
        public EventOrganizerDbContext(DbContextOptions<EventOrganizerDbContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<EventsCartItem> EventsCartItems { get; set; }

        public DbSet<User> Users { get; set; }
    }   
}
