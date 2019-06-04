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
    public class CommentRepository : ICommentRepository
    {
        private readonly EventOrganizerDbContext _eventOrganizerDbContext;

        public CommentRepository(EventOrganizerDbContext eventOrganizerDbContext)
        {
            _eventOrganizerDbContext = eventOrganizerDbContext;
        }

        public virtual IQueryable<Comment> Comments => _eventOrganizerDbContext.Comments.Include(c => c.User).Include(c => c.EventId).AsQueryable<Comment>();

        public virtual Comment GetCommentById(int commentId) => Comments.FirstOrDefault(p => p.Id == commentId);

        public virtual IQueryable<Comment> GetCommentsByEventId(int eventId) => Comments.Where(p => p.EventId == eventId);

        public virtual void Create(Comment comment)
        {
            _eventOrganizerDbContext.Comments.Add(comment);
            _eventOrganizerDbContext.SaveChanges();
        }

        public virtual void Edit(Comment comment)
        {
            var cm = _eventOrganizerDbContext.Comments.Where(it => it.Id == comment.Id).FirstOrDefault();
            _eventOrganizerDbContext.Comments.Remove(cm);
            _eventOrganizerDbContext.Add(comment);
            _eventOrganizerDbContext.SaveChanges();
        }

        public virtual void Delete(Comment item)
        {
            _eventOrganizerDbContext.Comments.Remove(item);
            _eventOrganizerDbContext.SaveChanges();
        }
    }
}
