// <copyright file="SearchService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Services
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    public class SearchService : ISearch
    {
        private readonly UserContext UserContext;

        public SearchService(UserContext user)
        {
            this.UserContext = user;
        }

        public IQueryable GetUsers(string str)
        {
            var entryFullName = from b in this.UserContext.Users
                                where str.IndexOf(b.LastName) != -1 && str.IndexOf(b.FirstName) != -1
                                select new { b.Id, b.FirstName, b.LastName, b.Photo };
            if (entryFullName.ToList().Count > 0)
            {
                return entryFullName;
            }
            else
            {
                var entryLastName = from b in this.UserContext.Users
                                    where str.IndexOf(b.LastName) != -1
                                    select new { b.Id, b.FirstName, b.LastName, b.Photo };
                if (entryLastName.ToList().Count > 0)
                {
                    return entryLastName;
                }
                else
                {
                    var entryFirstName = from b in this.UserContext.Users
                                         where str.IndexOf(b.FirstName) != -1
                                         select new { b.Id, b.FirstName, b.LastName, b.Photo };
                    return entryFirstName;
                }
            }
        }

        public IQueryable GetNews(string str)
        {
            var fullEntry = from b in this.UserContext.News
                            where str.IndexOf(b.Title) != -1 && b.Title != string.Empty
                            select new { b.Id, b.Title, b.Photo };
            if (fullEntry.ToList().Count > 0)
            {
                return fullEntry;
            }
            else
            {
                str = str.Replace("'", string.Empty);
                var partialEntry = from b in this.UserContext.News
                                   where b.Title.IndexOf(str) != -1 && b.Title != string.Empty
                                   select new { b.Id, b.Title, b.Photo };
                if (partialEntry.ToList().Count > 0)
                {
                    return partialEntry;
                }
                else
                {
                    var partialEntryInText = from b in this.UserContext.News
                                             where b.Text.IndexOf(str) != -1 && b.Text != string.Empty
                                             select new { b.Id, b.Title, b.Photo };

                    return partialEntryInText;
                }
            }
        }

        public IQueryable GetQA(string str)
        {
            str = str.Replace("'", string.Empty);
            var partialEntry = from b in this.UserContext.Questions
                               where b.Text.IndexOf(str) != -1 && b.Text != string.Empty
                               select new { b.Id, b.Text };
            return partialEntry;
        }
    }
}
