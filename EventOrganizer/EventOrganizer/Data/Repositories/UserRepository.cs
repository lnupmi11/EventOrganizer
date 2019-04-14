using EventOrganizer.Data.Interfaces;
using EventOrganizer.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventOrganizer.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;

        public UserRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }

        public IEnumerable<User> Users => _eventOrganizerDbContext.Users;

        public User GetUserByUserName(string username) => _eventOrganizerDbContext.Users.FirstOrDefault(p => p.UserName == username);
    }
}
