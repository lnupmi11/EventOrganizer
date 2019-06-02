using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
