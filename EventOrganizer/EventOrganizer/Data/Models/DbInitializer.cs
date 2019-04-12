using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Models
{
    public class DbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            AppDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<AppDbContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Events.Any())
            {
                context.AddRange
                (
                    new Event
                    {
                        Name = "Big Data conference",
                        ShortDescription = "Big Data conference",
                        LongDescription = "Big Data conference",
                        Category = Categories["Education"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Conference.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Conference.jpg",
                        EventDateTime = new DateTime(2019, 5, 18, 18, 00, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 35, 00)
                    },
                    new Event
                    {
                        Name = "Artificial intelligence seminar",
                        ShortDescription = "Artificial intelligence seminar",
                        LongDescription = "Artificial intelligence seminar",
                        Category = Categories["Education"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Seminar.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Seminar.jpg",
                        EventDateTime = new DateTime(2019, 5, 20, 18, 30, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 31, 00)
                    },
                    new Event
                    {
                        Name = "Hardware expo",
                        ShortDescription = "Hardware expo",
                        LongDescription = "Hardware expo",
                        Category = Categories["Marketing"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Expo.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Expo.jpg",
                        EventDateTime = new DateTime(2019, 5, 10, 18, 00, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 29, 00)
                    },
                    new Event
                    {
                        Name = "Volvo product launch",
                        ShortDescription = "Volvo product launch",
                        LongDescription = "Volvo product launch",
                        Category = Categories["Marketing"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Product-Launch.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Product-Launch.jpg",
                        EventDateTime = new DateTime(2019, 5, 25, 12, 30, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 27, 00)
                    },
                    new Event
                    {
                        Name = "Dell shareholder/board meeting",
                        ShortDescription = "Dell shareholder/board meeting",
                        LongDescription = "Dell shareholder/board meeting",
                        Category = Categories["Bussiness"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Board-Meetings.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Board-Meetings.jpg",
                        EventDateTime = new DateTime(2019, 5, 15, 18, 00, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 30, 00)
                    },
                    new Event
                    {
                        Name = "Holiday party",
                        ShortDescription = "Holiday party",
                        LongDescription = "Holiday party",
                        Category = Categories["Entertainment"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Year-End-Functions.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Year-End-Functions.jpg",
                        EventDateTime = new DateTime(2019, 5, 22, 22, 00, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 34, 00)
                    },
                    new Event
                    {
                        Name = "36 po opening ceremony",
                        ShortDescription = "36 po opening ceremony",
                        LongDescription = "36 po opening ceremony",
                        Category = Categories["Culture"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Executive-Retreat.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Executive-Retreat.jpg",
                        EventDateTime = new DateTime(2019, 5, 20, 15, 00, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 35, 00)
                    },
                    new Event
                    {
                        Name = "Oscar 2019",
                        ShortDescription = "Oscar 2019",
                        LongDescription = "Oscar 2019",
                        Category = Categories["Culture"],
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Corporate-Dinner.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Corporate-Dinner.jpg",
                        EventDateTime = new DateTime(2019, 5, 27, 21, 00, 00),
                        CreationalDateTime = new DateTime(2019, 3, 17, 17, 37, 00)
                    }
                );
            }

            context.SaveChanges();
        }

        private static Dictionary<string, Category> categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    var genresList = new Category[]
                    {
                        new Category { Name = "Bussiness", Description = "Bussiness conferences, meetings and..." },

                        new Category { Name = "Education", Description = "Educational seminars, lectures and..." },

                        new Category { Name = "Marketing", Description = "Marketing trade shows, product launches and..." },

                        new Category { Name = "Entertainment", Description = "Theme parties, weddings, birthdays and..." },

                        new Category { Name = "Culture", Description = "Opening ceremonies, award ceremonies and..." }
                    };

                    categories = new Dictionary<string, Category>();

                    foreach (Category genre in genresList)
                    {
                        categories.Add(genre.Name, genre);
                    }
                }

                return categories;
            }
        }
    }
}
