using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using EventOrganizer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(EventOrganizerDbContext context)
        {
            _userRepository = new UserRepository(context);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.All();
        }

        public User GetById(string id)
        {
            return _userRepository.Get(id);
        }

        public User GetByUserName(string username)
        {
            return _userRepository.GetByUserName(username);
        }

        public void CreateItem(User item)
        {
            if (item != null)
            {
                _userRepository.Create(item);
            }
        }

        public void UpdateItem(User item)
        {
            if (item != null)
            {
                _userRepository.Update(item);
            }
        }

        public void DeleteById(string id)
        {
            _userRepository.Delete(id);
        }
    }
}
