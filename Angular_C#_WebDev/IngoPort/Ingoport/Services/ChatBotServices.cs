// <copyright file="ChatBotServices.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    public class ChatBotServices : IBot
    {
        private readonly UserContext UserContext;
        public ChatBotServices(UserContext user)
        {
            this.UserContext = user;
        }

        public List<string> Bot(string str)
        {
            str = str.ToLower().Replace('ё', 'е');
            var result = from b in this.UserContext.BotContents
                         join c in this.UserContext.BotCommands on b.Id equals c.BotContentId
                         where str.IndexOf(c.Commands) != -1
                         select b.Content;

            return result.ToList();
        }
    }
}
