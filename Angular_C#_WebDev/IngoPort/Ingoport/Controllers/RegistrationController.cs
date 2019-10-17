// <copyright file="RegistrationController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    [Route("api/reg")]
    [ApiController]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistration registration;
        private readonly ILogger<RegistrationController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationController"/> class.
        /// </summary>
        /// <param name="registration"></param>
        public RegistrationController(ILogger<RegistrationController> logger, IRegistration registration)
        {
            this.registration = registration;
            this.logger = logger;
        }

        /// <remarks>
        /// Sample request:
        ///     POST /Todo
        ///     {"FirstName":"John",
        ///     "LastName":"Dohn",
        ///     "Email":"john@ingos.ru",
        ///     "Password":"1234",
        ///     "RoleId":"3"
        ///     }.
        /// </remarks>
        /// <param name="json"></param>
        /// <returns>A newly created TodoItem. </returns>
        /// <response code="200">Returns json with name and token. </response>
        /// <response code="400">Bad request. Use valid data. </response>
        [HttpPost]
        public ActionResult Registration([FromBody] User user)
        {
            try
            {
                var reg = this.registration.RegisterUser(user);
                return this.Ok(reg);

            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }
    }
}