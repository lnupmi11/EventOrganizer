using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Interfaces;
using EventOrganizer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOrganizer.DAL.Repositories
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

        public User GetUserById(string id) => _eventOrganizerDbContext.Users.FirstOrDefault(p => p.Id == id);

        public void DeleteById(string id)
        {
            _eventOrganizerDbContext.Remove(GetUserById(id));
            _eventOrganizerDbContext.SaveChanges();
        }
    }
}
