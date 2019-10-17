// <copyright file="PersonalAreaController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    [Route("api/personalarea")]
    [ApiController]
    [Authorize]
    public class PersonalAreaController : ControllerBase
    {
        private readonly IPersonalArea personalAreaService;
        private readonly IAuthorization authorization;
        private readonly ILogger<PersonalAreaController> logger;

        public PersonalAreaController(ILogger<PersonalAreaController> log, UserContext user, IPersonalArea personalArea, IAuthorization authorization)
        {
            this.personalAreaService = personalArea;
            this.logger = log;
            this.authorization = authorization;
        }

        /// <response code="200">Returns all user data.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return json or error. </returns>
        [HttpGet]
        [Route("getInfo")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]

        //public ActionResult GetUserDataFromDB()
        //{
        //    int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
        //    try
        //    {
        //        this.logger.LogInformation($"Successfully return user data");
        //        return this.Ok(this.personalAreaService.GetUserData(userId));
        //    }
        //    catch (Exception ex)
        //    {
        //        this.logger.LogInformation(ex.ToString());
        //        return this.BadRequest(ex);
        //    }
        //}

        public ActionResult GetUserInfo()
        {
            int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
            try
            {
                this.logger.LogInformation($"Successfully return user data");
                return this.Ok(this.personalAreaService.GetUserInfo(userId));
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(ex.ToString());
                return this.BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("getTeam")]
        public ActionResult GetUserTeam()
        {
            int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
            try
            {
                this.logger.LogInformation($"Successfully return user team");
                return this.Ok(this.personalAreaService.GetUserTeam(userId));
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(ex.ToString());
                return this.BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("getTasks")]
        public ActionResult GetUserTasks()
        {
            int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
            try
            {
                this.logger.LogInformation($"Successfully return user tasks");
                return this.Ok(this.personalAreaService.GetUserTasks(userId));
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(ex.ToString());
                return this.BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("getBookmarks")]
        public ActionResult GetUserBookmarks()
        {
            int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
            try
            {
                this.logger.LogInformation($"Successfully return user bookmarks");
                return this.Ok(this.personalAreaService.GetUserBookmarks(userId));
            }
            catch (Exception ex)
            {
                this.logger.LogInformation(ex.ToString());
                return this.BadRequest(ex);
            }
        }

        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Todo
        ///     {
        ///        "Title": "testTitle",
        ///        "TaskBody": "text",
        ///        "DateTime": "2019-03-02 00:00:00",
        ///        "FlagColor":"green"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns all user data.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Unauthorized. You use bad token or forgot to use it.Only mentor can use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <param name="json"></param>
        /// <returns>Return json or error. </returns>
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [Route("new-task")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        public ActionResult AddTasks([FromBody] UserTask task)
        {

            try
            {
            int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                this.personalAreaService.AddTasks(userId, task);
                this.logger.LogInformation($"Successfully add new task");
                return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Todo
        ///     {
        ///        "Title": "testTitle",
        ///        "TaskBody": "text",
        ///        "DateTime": "2019-03-02 00:00:00",
        ///        "FlagColor":"green",
        ///        "Id": 2
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns all user data.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Unauthorized. You use bad token or forgot to use it.Only mentor can use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <param name="json"></param>
        /// <returns>Return json or error. </returns>
        [Authorize(Roles = "Mentor")]
        [HttpPost]
        [Route("change-task")]
        public ActionResult ChangeTask([FromBody] UserTask userTask)
        {
            try
            {
                this.personalAreaService.ChangeTask(userTask);
                this.logger.LogInformation($"Successfully change task");
                return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <response code="200">Returns all user data.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Unauthorized. You use bad token or forgot to use it.Only mentor can use it.</response>
        /// <returns>Return json or error. </returns>
        [HttpGet]
        [Route("delete-task/{id}")]
        [Authorize(Roles = "Mentor")]
        public ActionResult DeleteTask(int Id)
        {
            try
            {
                this.personalAreaService.DeleteTask(Id);
                this.logger.LogInformation($"Successfully delete task");
                return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("rateme")]
        public ActionResult GetRating()
        {
            try
            {
                int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                return this.Ok(this.personalAreaService.UserRating(userId));
            }
            catch (Exception ex)
            {
                this.logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }
    }
}