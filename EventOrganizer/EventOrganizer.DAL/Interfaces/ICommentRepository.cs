using EventOrganizer.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventOrganizer.DAL.Interfaces
{
    public interface ICommentRepository
    {
        IQueryable<Comment> Comments { get; }
        Comment GetCommentById(int commentId);
        IQueryable<Comment> GetCommentsByEventId(int eventId);
        void Create(Comment comment);
        void Edit(Comment comment);
        void Delete(Comment comment);

    }
}
