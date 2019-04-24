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

        public IEnumerable<User> All()
        {
            return _eventOrganizerDbContext.Users;
        }

        public User Get(string id)
        {
            return All().FirstOrDefault(u => u.Id == id);
        }

        public User GetByUserName(string username)
        {
            return All().FirstOrDefault(u => u.UserName == username);
        }

        public void Create(User item)
        {
            _eventOrganizerDbContext.Users.Add(item);
            _eventOrganizerDbContext.SaveChanges();
        }

        public void Update(User item)
        {
            if (item != null)
            {
                _eventOrganizerDbContext.Users.Update(item);
                _eventOrganizerDbContext.SaveChanges();
            }
        }

        public void Delete(string id)
        {
            var user = _eventOrganizerDbContext.Users.Find(id);
            if (user != null)
            {
                _eventOrganizerDbContext.Users.Remove(user);
                _eventOrganizerDbContext.SaveChanges();
            }
        }
    }
}
