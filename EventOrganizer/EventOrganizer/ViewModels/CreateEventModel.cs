using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.ViewModels
{
    class CreateEventModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string ImageUrl { get; set; }
        public string ImageThumbnailUrl { get; set; }
        public bool IsPreferredEvent { get; set; }
        public int CategoryId { get; set; }
        public DateTime ScheduledAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
