namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Ingoport.Interfaces;
    using Ingoport.Models;
    using Newtonsoft.Json;

    [Route("api/coffee")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(400)]
    public class RandomCoffeeController : ControllerBase
    {
        private readonly IRandomCoffee randomCoffee;
        private readonly IAuthorization authorization;
        private readonly ILogger<RandomCoffeeController> logger;
        
        public RandomCoffeeController(ILogger<RandomCoffeeController> logger, IRandomCoffee randomCoffee, IAuthorization authorization)
        {
            this.randomCoffee = randomCoffee;
            this.authorization = authorization;
            this.logger = logger;
        }

        /// <response code="200">Return user status.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return json or error. </returns>
        [HttpGet]
        public ActionResult GetStatus()
        {
            try
            {
                var userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this.randomCoffee.GetUserStatus(userId);
                this.logger.LogInformation("Success -- return user status");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (NullReferenceException)
            {
                this.logger.LogInformation("Success -- return user status -- user not found in table -- status - 0");
                return this.Ok(0);
            }
            catch (Exception ex)
            {
                this.logger.LogError($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <remarks>
        /// Sample request:
        ///     POST /Todo
        ///
        ///     {
        ///        "Star": 5,
        ///        "Feedback": "Ok"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns message.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return json or error. </returns>
        [HttpPost]
        public ActionResult FeedBack(RandomCoffeeFeedback feedback)
        {
            try
            {
                var userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                feedback.UserId = userId;
                var result = this.randomCoffee.AddFeedback(feedback);
                this.logger.LogInformation($"Success -- {feedback}");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <response code="200">Returns message.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return json or error. </returns>
        [HttpGet]
        [Route("newpair")]
        public ActionResult FindPair()
        {
            try
            {
                var userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this.randomCoffee.FindNewPair(userId);
                this.logger.LogInformation("Success -- find new pair");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <response code="200">Returns json.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return json or error. </returns>
        [HttpGet]
        [Route("statistics")]
        public ActionResult GetStatistics()
        {
            try
            {
                var result = this.randomCoffee.ServiceUsage();
                this.logger.LogInformation("Success -- return statistics");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{ex}");
                return this.BadRequest(ex);
            }
        }

        /// <response code="200">Returns message.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return status code or error. </returns>
        [HttpGet]
        [Route("refuse")]
        public ActionResult RefuseMeeting()
        {
            try
            {
                var userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this.randomCoffee.CloseMeeting(userId);
                this.logger.LogInformation("Success -- close mitting");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{ex}");
                return this.BadRequest(ex);
            }
        }

        /// <response code="200">Returns message.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return message or error. </returns>
        [HttpGet]
        [Route("enroll")]
        public ActionResult EnrollUser()
        {
            try
            {
                var userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this.randomCoffee.Enroll(userId);
                this.logger.LogInformation($"Success -- {result}");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{ex}");
                return this.BadRequest(ex);
            }
        }

        /// <response code="200">Returns json.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return json or error. </returns>
        [HttpGet]
        [Route("history")]
        public ActionResult GetHistory()
        {
            try
            {
                var userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this.randomCoffee.MyRCHistory(userId);
                this.logger.LogInformation("Success -- return user history");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{ex}");
                return this.BadRequest(ex);
            }
            
        }

        /// <response code="200">Returns json.</response>
        /// <response code="400">Bad request. Use valid data.</response>
        /// <response code="401">Bad request. You use bad token or forgot to use it.</response>
        /// <response code="404">Couldn't find file or db.</response>
        /// <returns>Return json or error. </returns>
        [HttpGet]
        [Route("mypair")]
        public ActionResult GetPair()
        {
            try
            {
                var userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this.randomCoffee.GetUserInfo(userId);
                this.logger.LogInformation($"Success -- return user data{result}");
                return this.Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this.logger.LogError($"{ex}");
                return this.BadRequest(ex);
            }
        }
    }
}