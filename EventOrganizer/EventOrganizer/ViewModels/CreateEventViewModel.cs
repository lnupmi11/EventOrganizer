using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EventOrganizer.ViewModels
{
    public class CreateEventViewModel
    {
        [Required]
        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public string ImageThumbnailUrl { get; set; }

        public bool IsPreferredEvent { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public DateTime ScheduledAt { get; set; }
    }
}
