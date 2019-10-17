// <copyright file="UserContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<Like> Likes { get; set; }

        public DbSet<Bookmark> Bookmarks { get; set; }

        public DbSet<BotContent> BotContents { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<BotCommand> BotCommands { get; set; }

        public DbSet<UserTask> Tasks { get; set; }

        public DbSet<QuestionTopic> QuestionTopics { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<UserStatus> UserStatuses { get; set; }

        public DbSet<UserMeeting> UserMeetings { get; set; }

        public DbSet<RandomCoffee> RandomCoffees { get; set; }

        public DbSet<RandomCoffeeFeedback> RandomCoffeeFeedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            foreach (var relationship in modelbuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

        }
    }
}
