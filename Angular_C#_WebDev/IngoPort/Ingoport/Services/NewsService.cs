namespace Ingoport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Newtonsoft.Json;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    public class NewsService : INews
    {
        private readonly Dictionary<string, Like> like = new Dictionary<string, Like>();
        private readonly Dictionary<string, Bookmark> bookmark = new Dictionary<string, Bookmark>();
        private readonly UserContext UserContext;
        //private readonly ImagesService imagesService;

        public NewsService(UserContext UserContext, IHostingEnvironment hosting)
        {
            this.UserContext = UserContext;
        }

        public string GetNews(long isLikedId)
        {
            var list = from p in this.UserContext.News
                       where p.IsDeleted == false
                       join c in this.UserContext.Users on p.UserId equals c.Id
                       select new {
                           Title = p.Title,
                           Text = p.Text,
                           Photo = p.Photo,
                           UserPhoto = c.Photo,
                           Likes = p.Likes.Count,
                           Author = c.FirstName + ' ' + c.LastName,
                           Id = p.Id, Date = p.Date,
                           isLiked = (p.Likes.Any(f => f.User.Id == isLikedId)),
                           comments = (p.Comments.Where(k => k.NewsId == p.Id)
                           .Select(r => new { Id = r.id,
                               UserId = r.UserId,
                               UserName = c.FirstName+c.LastName,
                               Text = r.commentText,
                               Photo = c.Photo
                           }))
                       };

            return JsonConvert.SerializeObject(list, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

        }

        public News AddNews(News news, long userId)
        {
            var newsOwner = this.UserContext.Users.FirstOrDefault(c => c.Id == userId);

            news.User = newsOwner;
            this.UserContext.News.Add(news);
            this.UserContext.SaveChanges();

            return news;
        }

        public Dictionary<string, Like> Like(int UserId, long PostId)
        {
            try
            {
                var user = this.UserContext.Users.FirstOrDefault(c => c.Id == UserId);
                var post = this.UserContext.News.FirstOrDefault(c => c.Id == PostId);
                bool isLiked = this.UserContext.Likes.Any(c => c.News == post && c.User == user);
                if (isLiked)
                {
                    Like toDelete = this.UserContext.Likes.FirstOrDefault(c => c.News == post && c.User == user);
                    this.like[isLiked.ToString()] = toDelete;
                }
                else
                {
                    Like toAdd = new Like { User = user, News = post };
                    this.like[isLiked.ToString()] = toAdd;
                }

                return this.like;
            }
            catch (Exception ex)
            {
                this.like[ex.Message] = null;
                return this.like;
            }
        }

        public Comment Comment(int UserId, long PostId, string text)
        {
            var user = this.UserContext.Users.FirstOrDefault(c => c.Id == UserId);
            var post = this.UserContext.News.FirstOrDefault(c => c.Id == PostId);
            Comment comment = new Comment { commentText = text, News = post, User = user };
            this.UserContext.Comments.Add(comment);
            this.UserContext.SaveChanges();
            return comment;
        }

        public Dictionary<string, Bookmark> Bookmark(int UserId, long PostId)
        {
            try
            {
                var user = this.UserContext.Users.FirstOrDefault(c => c.Id == UserId);
                var post = this.UserContext.News.FirstOrDefault(c => c.Id == PostId);
                bool isLiked = this.UserContext.Bookmarks.Any(c => c.News == post && c.User == user);
                if (isLiked)
                {
                    Bookmark toDelete = this.UserContext.Bookmarks.FirstOrDefault(c => c.News == post && c.User == user);
                    this.bookmark[isLiked.ToString()] = toDelete;
                }
                else
                {
                    Bookmark toAdd = new Bookmark { User = user, News = post };
                    this.bookmark[isLiked.ToString()] = toAdd;
                }

                return this.bookmark;
            }
            catch (Exception ex)
            {
                this.bookmark[ex.Message] = null;
                return this.bookmark;
            }
        }

        public string GetNews(long isLikedId, string length)
        {
            bool flag;
            int num = 1;
            try
            {
                num = Convert.ToInt32(length);
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
            }

            var list = this.UserContext.News.Where(c => c.IsDeleted == false).Join(this.UserContext.Users, p => p.UserId, c => c.Id, (p, c) => new
            {
                Title = p.Title,
                Text = p.Text,
                Photo = p.Photo,
                Likes = p.Likes.Count,
                Author = c.FirstName + ' ' + c.LastName,
                Id = p.Id,
                Date = p.Date,
                isLiked = (p.Likes.Any(f => f.User.Id == isLikedId)),
                comments = (p.Comments.Where(k => k.NewsId == p.Id).Select(r => new { Id = r.id, UserId = r.UserId, Text = r.commentText }))
            }).ToArray().Reverse();

            if (flag == true)
            {
                return JsonConvert.SerializeObject(list.Skip(20 + num).Take(5), Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            }

            return JsonConvert.SerializeObject(list.Take(20), Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public string ChangeNews(News news)
        {
            var oldNews = this.UserContext.News.FirstOrDefault(c => c.Id == news.Id);

            oldNews.Text = oldNews.Text != news.Text ? news.Text : oldNews.Text;
            oldNews.Title = oldNews.Title != news.Title ? news.Title : oldNews.Title;
            oldNews.Photo = oldNews.Photo != news.Photo ? news.Photo : oldNews.Photo;

            this.UserContext.News.Update(oldNews);
            this.UserContext.SaveChanges();

            return "News updated";
        }

        public string DeleteNews(int id)
        {
            var news = this.UserContext.News.FirstOrDefault(c => c.Id == id);
            news.IsDeleted = true;
            this.UserContext.News.Update(news);

            this.UserContext.SaveChanges();

            return "Delete news";
        }
    }
}