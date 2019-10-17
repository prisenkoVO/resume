// <copyright file="SettingController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Ingoport.Models;
    using Ingoport.Services;
    using Ingoport.Interfaces;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/settings")]
    [ApiController]
    [Authorize]
    public class SettingController : ControllerBase
    {
        private readonly SettingService settingService;
        private readonly IAuthorization authorization;
        private readonly ILogger<SettingController> logger;

        public SettingController(UserContext user,ILogger<SettingController> logger, IAuthorization authorization)
        {
            this.settingService = new SettingService(user);
            this.authorization = authorization;
            this.logger = logger;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult GetUserData()
        {
            try
            {
                int id = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this.settingService.GetUsers(id);
                this.logger.LogInformation($"Success -- Return auth data -- {result}");
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"Success -- Return auth data -- {ex}");
                return this.BadRequest(ex);
            }
        }

        [HttpPut]
        public ActionResult PutUserData([FromBody]User json)
        {
            try
            {
                int id = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                this.settingService.PutUserData(json, id);
                this.logger.LogInformation($"Success");
                return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }
    }
}