using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User GetById(string id);

        User GetByUserName(string username);

        void CreateItem(User item);

        void UpdateItem(User item);

        void DeleteById(string id);
    }
}
