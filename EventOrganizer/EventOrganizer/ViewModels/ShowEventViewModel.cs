using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.ViewModels
{
    public class ShowEventViewModel
    {
        public Event Event { get; set; }

        public string Comment { get; set; }

        public bool Liked { get; set; }

    }
}
