// <copyright file="AuthController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Ingoport.Models;
    using Ingoport.Interfaces;

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorization authorization;
        private readonly UserContext UserContext;
        private readonly ILogger<AuthController> logger;

        public AuthController(UserContext UserContext, ILogger<AuthController> log, IAuthorization authorization)
        {
            this.authorization = authorization;
            this.UserContext = UserContext;
            this.logger = log;
        }

        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Todo
        ///     {
        ///        "Email": "admin@ingos.ru",
        ///        "Password": "aaa111"
        ///     }
        ///
        /// </remarks>
        /// <param name="userData"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Bad request. Use valid data</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Route("login")]
        public ActionResult LogIn([FromBody] User userData)
        {
            try
            {
                var result = this.authorization.CheckForAvailability(userData);
                this.logger.LogInformation($"Success -- Return auth data -- {result}");
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Wrong email or password -- Unauthorized -- {ex}");
                return this.BadRequest(ex.Message);
            }
        }
    }
}