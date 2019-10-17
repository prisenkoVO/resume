// <copyright file="BotController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Ingoport.Models;
    using Ingoport.Services;
    using Ingoport.Interfaces;

    [Route("api/bot")]
    [ApiController]
    [Authorize]
    public class BotController : ControllerBase
    {
        private readonly ChatBotServices bot;
        private readonly IAuthorization authorization;

        public BotController(UserContext UserContext)
        {
            this.bot = new ChatBotServices(UserContext);
        }

        /*api/bot?query='информация о дмс'*/
        [HttpGet]
        public ActionResult GetRequest([FromQuery]string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    return this.Ok();
                }

                return this.Ok(this.bot.Bot(query));
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}