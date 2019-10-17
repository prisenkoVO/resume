// <copyright file="RandomCoffeeService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    public class RandomCoffeeService : IRandomCoffee
    {
        private readonly UserContext UserContext;
        public Dictionary<string, string> answer = new Dictionary<string, string>();


        public RandomCoffeeService(UserContext user)
        {
            this.UserContext = user;
        }

        public Dictionary<string, string> Enroll(long id)
        {
            var user = this.UserContext.Users.FirstOrDefault(c => c.Id == id);

            if (!this.UserContext.UserStatuses.Any(c => c.User == user))
            {
                this.UserContext.UserStatuses.Add(new UserStatus { User = user, Status = 1 });
            }
            else
            {
                if (this.UserContext.UserStatuses.FirstOrDefault(c=>c.UserId == id).Status == 4)
                {
                    var userInfo = this.UserContext.UserStatuses.FirstOrDefault(c => c.Id == id);
                    userInfo.Status = 1;
                    this.UserContext.UserStatuses.Update(userInfo);
                }
            }

            this.UserContext.SaveChanges();

            var userStatus = this.UserContext.UserStatuses.FirstOrDefault(c => c.UserId == id);

            var statuses = this.UserContext.UserStatuses;

            if (userStatus.Status == 2)
            {
                answer.Add("message", "Вы уже отправляли заявку. Ждите подтверждения!");
            }
            else if (userStatus.Status == 3)
            {
                answer.Add("message", "Вы отменили встречу, поэтому не можете в течение недели записываться на новые! Ждем вас на следующей неделе");
            }

            else
            {
                try
                {
                    this.FindNewPair(id);
                }
                catch (NullReferenceException)
                {
                }
                answer.Add("message", "Ваша заявка принята! Ждите подтверждения в пятницу в 16:00");
            }
            return answer;
        }

        public string CloseMeeting(long id)
        {
            var user = this.UserContext.Users.FirstOrDefault(c => c.Id == id);

            var meeting = this.UserContext.UserMeetings.Where(c => c.UserId == id && c.RandomCoffee.Status == 1).FirstOrDefault();
            var rnd = this.UserContext.RandomCoffees.Where(c=>c.Id == meeting.RandomCoffeeId).FirstOrDefault();
            rnd.Status = 4;
            

            var secondUserId = this.UserContext.UserMeetings.Where(c => c.RandomCoffeeId == meeting.RandomCoffeeId && c.UserId != id).FirstOrDefault().UserId;
            var secondUserStatus =  this.UserContext.UserStatuses.FirstOrDefault(c => c.UserId == secondUserId);
            secondUserStatus.Status = 1;

            var update = this.UserContext.UserStatuses.FirstOrDefault(c => c.UserId == id);
            update.Status = 3;
            
            this.UserContext.SaveChanges();
            return "Metting was closed";
        }

        public string AddFeedback(RandomCoffeeFeedback feedback)
        {
            var user = this.UserContext.UserStatuses.FirstOrDefault(c => c.UserId == feedback.UserId && c.Status != 4);
            var userInMeeting = this.UserContext.UserMeetings.FirstOrDefault(c => c.UserId == feedback.UserId);
            var coffee = this.UserContext.RandomCoffees.FirstOrDefault(c => c.Id == userInMeeting.RandomCoffeeId);

            user.Status = 4;
            userInMeeting.Star = feedback.Star;
            userInMeeting.Feedback = feedback.FeedBack;
            coffee.Status = 2;

            this.UserContext.UserStatuses.Update(user);
            this.UserContext.UserMeetings.Update(userInMeeting);
            this.UserContext.RandomCoffees.Update(coffee);

            this.UserContext.RandomCoffeeFeedbacks.Add(feedback);

            this.UserContext.SaveChanges();

            return "Add new feedback in database";
        }

        public string FindNewPair(long userId)
        {
            var user = this.UserContext.Users.FirstOrDefault(c => c.Id == userId);
            var firstUser=new UserStatus ();
            var secondUser = new UserStatus ();
            try
            {
                firstUser = this.UserContext.UserStatuses.FirstOrDefault(c => c.UserId == userId);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            var notWaitingPeople = this.UserContext.UserStatuses.Join(this.UserContext.UserMeetings, c => c.UserId, p => p.UserId,
                (c, p) => new { CoffeeId = p.RandomCoffeeId, UserId = c.UserId, Status = c.Status }).Where(c => c.UserId != userId && c.Status == 1).Select(c => c.UserId).ToArray();

            var allUsers = this.UserContext.UserStatuses.Where(c => c.UserId != userId && c.Status == 1).Select(c => c.UserId).ToArray();

            var newUsers = allUsers.Except(notWaitingPeople).ToArray();

            long choice = 0;

            foreach (var newUser in newUsers)
            {
                foreach (var k in allUsers.Except(notWaitingPeople))
                {
                    if (this.UserContext.Users.FirstOrDefault(c => c.Id == newUser).RoleId != user.RoleId)
                    {
                        choice = newUser;
                    }
                }
            }

            secondUser = this.UserContext.UserStatuses.FirstOrDefault(c => c.UserId == choice);

            this.UserContext.RandomCoffees.Add(new RandomCoffee { Status = 1 });
            this.UserContext.SaveChanges();


            firstUser.Status = 2;
            secondUser.Status = 2;

            this.UserContext.UserStatuses.Update(firstUser);
            this.UserContext.UserStatuses.Update(secondUser);
            this.UserContext.UserMeetings.Add(new UserMeeting { RandomCoffeeId = this.UserContext.RandomCoffees.LastOrDefault().Id, UserId = userId });
            this.UserContext.UserMeetings.Add(new UserMeeting { RandomCoffeeId = this.UserContext.RandomCoffees.LastOrDefault().Id, UserId = choice });

            this.UserContext.SaveChanges();

            return "Successfully find new pair";
        }

        public int GetUserStatus(long userId)
        {
            var result = this.UserContext.UserStatuses.LastOrDefault(c => c.UserId == userId).Status;
            return result;
        }

        public IQueryable MyRCHistory(long id)
        {
            var user = this.UserContext.UserMeetings.FirstOrDefault(c => c.UserId == id);
            var meetings = this.UserContext.UserMeetings.Where(c => c.UserId != id)
                .Join(this.UserContext.RandomCoffees.Where(c => c.Id == user.RandomCoffeeId),
                c => c.RandomCoffeeId,
                p => p.Id,
                (p, c) => new
                {
                    Date = c.DateTime.ToString(),
                    Pair = p.User.FirstName + ' ' + p.User.LastName,
                    p.Feedback,
                });

            return meetings;
        }

        public Dictionary<string, object> ServiceUsage()
        {
            Dictionary<string, object> Report = new Dictionary<string, object>();

            var query = UserContext.RandomCoffees
                   .GroupBy(p => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(p.DateTime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                   .Select(g => new { name = g.Key, count = g.Count() }).OrderBy(c => c.name);
            Report["WeekRepost"] = query;


            var Mark = this.UserContext.UserMeetings
                  .GroupBy(g => g.UserId, c => c.Star)
                  .Select(g => new
                  {
                      UserId = g.Key,
                      Average = g.Average(),
                  });
            Report["AverageMark"] = Mark;
            var max = this.UserContext.UserMeetings.Max(c => c.Star);
            var theBest = this.UserContext.UserMeetings.Where(c => c.Star == max);
            Report["BestUser"] = theBest;

            var min = this.UserContext.UserMeetings.Min(c => c.Star);
            var theWorst = this.UserContext.UserMeetings.Where(c => c.Star == min);
            Report["WorstUser"] = theWorst;

            var RCFeedback = this.UserContext.RandomCoffeeFeedbacks;
            Report["RCFeedback"] = RCFeedback;

            return Report;
        }

        public IQueryable GetUserInfo(long userId)
        {
            var pair = this.UserContext.UserMeetings.LastOrDefault(c => c.UserId == userId).RandomCoffeeId;
            var id = this.UserContext.UserMeetings.FirstOrDefault(c => c.RandomCoffeeId == pair & c.UserId != userId).UserId;
            var user = this.UserContext.Users.Where(c => c.Id == id).Select(c => new { Name = c.FirstName + ' ' + c.LastName, Email = c.Email, Phone = c.Phone });
            return user;
        }
    }
}
