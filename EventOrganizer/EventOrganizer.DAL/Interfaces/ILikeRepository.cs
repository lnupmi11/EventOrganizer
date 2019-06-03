using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOrganizer.DAL.Interfaces
{
    public interface ILikeRepository
    {
        IQueryable<Like> Likes { get; }
        int GetLikesByEventId(int eventId);
        void Like(Like like);
        bool Exists(Like like);
        void Unlike(Like like);
    }
}
