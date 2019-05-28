﻿using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.Tests
{
    public static class TestObjects
    {
        public static readonly User User1 = new User
        {
            Email = "email1@gmail.com",
            UserName = "username#1"
        };

        public static readonly User User2 = new User
        {
            Email = "email2@gmail.com",
            UserName = "username#2"
        };

        public static readonly User User3 = new User
        {
            Email = "email3@gmail.com",
            UserName = "username#3"
        };

        public static readonly Event Event1 = new Event
        {
            Name = "Big Data conference",
            ShortDescription = "Big Data conference",
            LongDescription = "Big Data conference",
            CategoryId = 1,
            ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Conference.jpg",
            IsPreferredEvent = true,
            ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Conference.jpg",
            ScheduledAt = new DateTime(2019, 5, 18, 18, 00, 00),
            CreatedAt = new DateTime(2019, 3, 17, 17, 35, 00)
        };

        public static readonly Event Event2 = new Event
        {
            Name = "Artificial intelligence seminar",
            ShortDescription = "Artificial intelligence seminar",
            LongDescription = "Artificial intelligence seminar",
            CategoryId = 2,
            ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Seminar.jpg",
            IsPreferredEvent = true,
            ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Seminar.jpg",
            ScheduledAt = new DateTime(2019, 5, 20, 18, 30, 00),
            CreatedAt = new DateTime(2019, 3, 17, 17, 31, 00)
        };

        public static readonly Event Event3 = new Event
        {
            Name = "Hardware expo",
            ShortDescription = "Hardware expo",
            LongDescription = "Hardware expo",
            CategoryId = 3,
            ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Expo.jpg",
            IsPreferredEvent = true,
            ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Expo.jpg",
            ScheduledAt = new DateTime(2019, 5, 10, 18, 00, 00),
            CreatedAt = new DateTime(2019, 3, 17, 17, 29, 00)
        };
    }
}