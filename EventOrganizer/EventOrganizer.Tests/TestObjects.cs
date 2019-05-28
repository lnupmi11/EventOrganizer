using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.Tests
{
    public static class TestObjects
    {
        public static readonly User User1 = new User
        {
            Id = "1",
            Email = "email1@gmail.com",
            UserName = "username#1"
        };

        public static readonly User User2 = new User
        {
            Id = "2",
            Email = "email2@gmail.com",
            UserName = "username#2"
        };

        public static readonly User User3 = new User
        {
            Id = "3",
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

        public static readonly Category Category1 = new Category
        {
            Name = "Bussiness",
            Description = "Bussiness conferences, meetings and..."
        };

        public static readonly Category Category2 = new Category
        {
            Name = "Education",
            Description = "Educational seminars, lectures and..."
        };

        public static readonly Category Category3 = new Category
        {
            Name = "Marketing",
            Description = "Marketing trade shows, product launches and..."
        };

        public static readonly Category Category4 = new Category
        {
            Name = "Entertainment",
            Description = "Theme parties, weddings, birthdays and..."
        };

        public static readonly Category Category5 = new Category
        {
            Name = "Culture",
            Description = "Opening ceremonies, award ceremonies and..."
        };
    }
}
