using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.DAL.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }

        User GetUserByUserName(string UserName);

        User GetUserById(string id);

        void DeleteById(string id);
    }
}
