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
    public class LikeRepository : ILikeRepository
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;

        public LikeRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }

        public virtual IQueryable<Like> Likes => _eventOrganizerDbContext.Likes.Include(c => c.UserId).Include(c => c.EventId).AsQueryable();

        public virtual int GetLikesByEventId(int eventId) => Likes.Where(p => p.EventId == eventId).Count();

        public virtual void Like(Like like)
        {
            _eventOrganizerDbContext.Likes.Add(like);
            _eventOrganizerDbContext.SaveChanges();
        }

        public virtual bool Exists(Like like)
        {
            var l = _eventOrganizerDbContext.Likes.FirstOrDefault(it => it.EventId == like.EventId && it.UserId == like.UserId);
            if (l == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public virtual void Unlike(Like like)
        {
            if (like != null)
            {
                var l = _eventOrganizerDbContext.Likes.FirstOrDefault(it => it.EventId == like.EventId && it.UserId == like.UserId);
                _eventOrganizerDbContext.Likes.Remove(l);
                _eventOrganizerDbContext.SaveChanges();
            }
        }
    }
}
