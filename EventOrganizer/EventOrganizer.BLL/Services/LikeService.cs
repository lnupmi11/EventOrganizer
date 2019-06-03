using EventOrganizer.BLL.Interfaces;
using EventOrganizer.DAL.DbContext;
using EventOrganizer.DAL.Models;
using EventOrganizer.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Services
{
    public class LikeService : ILikeService
    {
        private readonly LikeRepository _likeRepository;

        public LikeService(EventOrganizerDbContext context)
        {
            _likeRepository = new LikeRepository(context);
        }

        public virtual int LikesCount(int eventId)
        {
            return _likeRepository.GetLikesByEventId(eventId);
        }

        public virtual void Like(int eventId, string userId)
        {
            if (!_likeRepository.Exists(new Like() { UserId = userId, EventId = eventId }))
            {
                _likeRepository.Like(new Like() { UserId = userId, EventId = eventId });
            }
        }

        public virtual void Unlike(int eventId, string userId)
        {
            if (_likeRepository.Exists(new Like() { UserId = userId, EventId = eventId }))
            {
                _likeRepository.Unlike(new Like() { UserId = userId, EventId = eventId });
            }
        }
        public virtual bool IsLiked(int eventId, string userId)
        {
            return _likeRepository.Exists(new Like() { UserId = userId, EventId = eventId });
        }
    }
}
