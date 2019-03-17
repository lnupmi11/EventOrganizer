using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Mocks
{
    public class MockEventRepository : IEventRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

        public IEnumerable<Event> Events
        {
            get
            {
                return new List<Event>
                {
                    new Event {
                        Name = "Big Data conference",
                        ShortDescription = "Big Data conference",
                        LongDescription = "Big Data conference",
                        Category = _categoryRepository.Categories.ElementAt(1),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Conference.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Conference.jpg"
                    },
                    new Event {
                        Name = "Artificial intelligence seminar",
                        ShortDescription = "Artificial intelligence seminar",
                        LongDescription = "Artificial intelligence seminar",
                        Category = _categoryRepository.Categories.ElementAt(1),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Seminar.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Seminar.jpg"
                    },
                    new Event {
                        Name = "Hardware expo",
                        ShortDescription = "Hardware expo",
                        LongDescription = "Hardware expo",
                        Category = _categoryRepository.Categories.ElementAt(2),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Expo.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Expo.jpg"
                    },
                    new Event {
                        Name = "Volvo product launch",
                        ShortDescription = "Volvo product launch",
                        LongDescription = "Volvo product launch",
                        Category = _categoryRepository.Categories.ElementAt(2),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Product-Launch.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Product-Launch.jpg"
                    },
                    new Event {
                        Name = "Dell shareholder/board meeting",
                        ShortDescription = "Dell shareholder/board meeting",
                        LongDescription = "Dell shareholder/board meeting",
                        Category = _categoryRepository.Categories.First(),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Board-Meetings.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Board-Meetings.jpg"
                    },
                    new Event {
                        Name = "Holiday party",
                        ShortDescription = "Holiday party",
                        LongDescription = "Holiday party",
                        Category = _categoryRepository.Categories.ElementAt(3),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Year-End-Functions.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Year-End-Functions.jpg"
                    },
                    new Event {
                        Name = "36po opening ceremony",
                        ShortDescription = "36po opening ceremony",
                        LongDescription = "36po opening ceremony",
                        Category = _categoryRepository.Categories.Last(),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Executive-Retreat.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Executive-Retreat.jpg"
                    },
                    new Event {
                        Name = "Oscar 2019",
                        ShortDescription = "Oscar 2019",
                        LongDescription = "Oscar 2019",
                        Category = _categoryRepository.Categories.Last(),
                        ImageUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Corporate-Dinner.jpg",
                        IsPreferredEvent = true,
                        ImageThumbnailUrl = "https://thealeitgroup.com/wp/wp-content/uploads/2018/05/Corporate-Dinner.jpg"
                    }
                };
            }
        }

        public IEnumerable<Event> PreferredEvents { get; }

        public Event GetEventById(int EventId)
        {
            throw new NotImplementedException();
        }
    }
}
