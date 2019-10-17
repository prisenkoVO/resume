using System.Collections.Generic;
using Ingoport.Models;

namespace Ingoport.Interfaces
{
    public interface INews
    {
        string GetNews(long isLikedId);

        News AddNews(News news, long userId);

        Dictionary<string, Like> Like(int UserId, long PostId);

        Comment Comment(int UserId, long PostId, string text);

        Dictionary<string, Bookmark> Bookmark(int UserId, long PostId);

        string ChangeNews(News news);
        string DeleteNews(int id);
    }
}