using System;
using System.Collections.Generic;
using System.Text;

namespace EventOrganizer.BLL.Interfaces
{
    public interface ILikeService
    {
        int LikesCount(int eventId);
        void Like(int eventId, string userId);
        void Unlike(int eventId, string userId);
        bool IsLiked(int eventId, string userId);

    }
}
