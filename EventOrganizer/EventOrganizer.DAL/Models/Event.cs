using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.DAL.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsPreferredEvent { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
