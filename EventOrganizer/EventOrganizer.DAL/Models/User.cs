using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace EventOrganizer.DAL.Models
{
    public class User : IdentityUser
    {
        public ICollection<Event> Events { get; set; }
    }
}
