﻿using System;
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

        public virtual DbSet<Event> Events { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<EventsCartItem> EventsCartItems { get; set; }

        public virtual DbSet<User> Users { get; set; }
    }
}
