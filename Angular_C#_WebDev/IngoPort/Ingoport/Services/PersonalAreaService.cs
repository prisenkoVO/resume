// <copyright file="PersonalAreaService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    public class PersonalAreaService : IPersonalArea
    {
        private readonly UserContext UserContext;

        public PersonalAreaService(UserContext user)
        {
            this.UserContext = user;
        }

        public string GetUserInfo(int id)
        {
            var user = this.UserContext.Users
                .Where(c => c.Id == id)
                .Select(
                x => new
                {
                    x.Id,
                    x.FirstName,
                    x.LastName,
                    x.Login,
                    x.Photo,
                    x.Phone,
                    x.Email,
                    x.Birth,
                    x.Team,
                    x.RoleId,
                }).FirstOrDefault();

            return JsonConvert.SerializeObject(user);
        }

        public string GetUserTeam(int id)
        {
            var team = this.UserContext.Users
                .OrderBy(c => c.RoleId)
                .Where(c => c.Id != id)
                .Join(this.UserContext.Users, p => p.Team, c => c.Team, (p, c) => new
                {
                    c.FirstName,
                    c.LastName,
                    c.RoleId,
                    c.Team,
                    c.Photo,
                });

            var teams = this.UserContext.Users.OrderBy(c => c.RoleId).Where(c => c.Team == this.UserContext.Users.FirstOrDefault(p => p.Id == id).Team).Select(c=>new { c.FirstName, c.LastName, c.RoleId, c.Team, c.Photo}); 

            return JsonConvert.SerializeObject(teams);
        }

        public string GetUserTasks(int id)
        {
            // this.userWithTeammates.Add("team", this.UserContext.Users.OrderBy(c => c.RoleId).Where(c => c.Team == user.Team && c.Id != id).Select(c => new { FirstName = c.FirstName,LastName =  c.LastName,Photo =  c.Photo,RoleId =  c.RoleId }));
            var tasks = this.UserContext.Tasks
                .Where(c => c.InternId == id && c.IsDeleted == false)
                .Select(x => new
                {
                    x.Id,
                    x.Title,
                    x.TaskBody,
                    x.Owner,
                    author = x.Author.FirstName + " " + x.Author.LastName,
                    x.FlagColor,
                    x.Done,
                    x.DateTime,
                    x.Deadline,
                });

            return JsonConvert.SerializeObject(tasks);
        }

        public string GetUserBookmarks(int id)
        {
            var bookmarks = this.UserContext.Bookmarks
                 .Where(c => c.UserId == id)
                .Select(x => new
                {
                    x.Id,
                    Title = x.News.Title,
                    newsId = x.NewsId,
                });

            return JsonConvert.SerializeObject(bookmarks);
        }

        public string GetBookmarks(User user)
        {

            var idbook = this.UserContext.Bookmarks.Where(c => c.UserId == user.Id).Join(this.UserContext.News, k => k.NewsId, p => p.Id, (k, p) => new { PostId = p.Id, Photo = p.Photo, Title = p.Title });
            return JsonConvert.SerializeObject(idbook);
        }

        public string GetTask(User user)
        {
            if (this.UserContext.Users.Any(c => c.Email == user.Email && c.Password == user.Password))
            {
                return JsonConvert.SerializeObject(this.UserContext.Tasks.Where(c => c.IsDeleted == false).Join(this.UserContext.Users, c => c.AuthorId, p => p.Id, (c, p) => new { mentor = p.FirstName + ' ' + p.LastName, title = c.Title, text = c.TaskBody, dateTime = c.DateTime, deadline = c.Deadline, isDone = c.Done, owner = c.Owner, flag = c.FlagColor, id = c.Id }));
            }

            throw new UnauthorizedAccessException();
        }

        public void DeleteTask(int id)
        {
            long key = Convert.ToInt64(id);
            this.UserContext.Tasks.FirstOrDefault(c => c.Id == key).IsDeleted = true;
            this.UserContext.SaveChanges();
        }

        public void ChangeTask(UserTask inputTask)
        {
            var task = this.UserContext.Tasks.FirstOrDefault(c => c.Id == inputTask.Id);
            task.Title = task.Title != inputTask.Title ? inputTask.Title : task.Title;
            task.TaskBody = task.TaskBody != inputTask.TaskBody ? inputTask.TaskBody : task.TaskBody;
            task.DateTime = task.DateTime != inputTask.DateTime ? inputTask.DateTime : task.DateTime;
            this.UserContext.SaveChanges();
        }

        public void AddTasks(int id, UserTask tasks)
        {
            var user = this.UserContext.Users.FirstOrDefault(c => c.Id == id);
            tasks.Author = this.UserContext.Users.FirstOrDefault(c => c.Id == id);
            tasks.DateTime = DateTime.Now;
            this.UserContext.Tasks.Add(tasks);
            this.UserContext.SaveChanges();
        }

        public Dictionary<string, double> UserRating(int id)
        {
            var intern = this.UserContext.Users.FirstOrDefault(c => c.Id == id);


            var likesRating = this.UserContext.News.Where(c => c.UserId == id).Select(c => c.Likes).Count();

            var commentRating = this.UserContext.News.Where(c => c.UserId == id).Select(p => p.Comments).Count();

            return new Dictionary<string, double>
            {
                { "Likes" , likesRating},
                { "Comments", commentRating},
            };
        }
    }
}