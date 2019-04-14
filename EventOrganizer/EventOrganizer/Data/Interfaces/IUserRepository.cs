using EventOrganizer.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.Data.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }

        User GetUserByUserName(string UserName);
    }
}
