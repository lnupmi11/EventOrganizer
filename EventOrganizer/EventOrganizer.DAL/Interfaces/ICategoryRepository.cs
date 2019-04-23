using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.DAL.Interfaces
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
