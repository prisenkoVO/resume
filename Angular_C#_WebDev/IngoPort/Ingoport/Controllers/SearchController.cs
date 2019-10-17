// <copyright file="SearchController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Ingoport.Models;
    using Ingoport.Services;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly SearchService searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(UserContext UserContext, ILogger<SearchController> logger)
        {
            this.searchService = new SearchService(UserContext);
            this._logger = logger;
        }

        [HttpGet]
        [Route("users")]
        public ActionResult RequestUsers([FromQuery] string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    this._logger.LogInformation("NoContent query is empty");
                    return this.StatusCode(204);
                }

                this._logger.LogInformation("Successfully return search users");
                return this.Ok(this.searchService.GetUsers(query));
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("news")]
        public ActionResult RequestNews([FromQuery] string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    this._logger.LogInformation("NoContent query is empty");
                    return this.StatusCode(204);
                }

                this._logger.LogInformation("Successfully return search news");
                return this.Ok(this.searchService.GetNews(query));
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        [HttpGet]
        [Route("qa")]
        public ActionResult RequestQA([FromQuery] string query)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(query))
                {
                    this._logger.LogInformation("NoContent query is empty");
                    return this.StatusCode(204);
                }

                this._logger.LogInformation("Successfully return search Q&A");
                return this.Ok(this.searchService.GetQA(query));
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }
    }
}