using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.DAL.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Event> Events { get; set; }
    }
}
