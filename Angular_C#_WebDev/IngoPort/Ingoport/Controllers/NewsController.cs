namespace Ingoport.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    [ApiController]
    [Route("api/news")]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(401)]
    public class NewsController : ControllerBase
    {
        private readonly INews _news;
        private readonly IAuthorization authorization;
        private readonly ILogger<NewsController> _logger;
        private readonly UserContext UserContext;

        public NewsController(UserContext UserContext, IAuthorization authorization, ILogger<NewsController> logger, INews news)
        {
            this.UserContext = UserContext;
            this._logger = logger;
            this._news = news;
            this.authorization = authorization;
        }

        [HttpGet]
        public ActionResult GetNews()
        {
            try
            {
                int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                this._logger.LogInformation("Successfully return all news");
                return this.Ok(this._news.GetNews(userId));
            }
            catch (Exception ex)
            {
                this._logger.LogError($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Todo
        ///     {
        ///        "Title": "Название статьи",
        ///        "Text": "Текст статьи",
        ///        "Photo" : "image.jpg",
        ///        "Date" : "16/05/2013"
        ///     }
        ///
        /// </remarks>
        /// <param name="newPost"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Bad request. Use valid data</response>
        [HttpPost]
        [ProducesResponseType(404)]
        public IActionResult PostNews([FromBody] News json)
        {
            try
            {
                int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                json.Date = DateTime.Now;
                var result = this._news.AddNews(json, userId);
                this._logger.LogInformation($"Added successfully");
                return this.Ok(result.Id);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error when adding post -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <param name="id"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="200">Returns Ok</response>
        /// <response code="400">Bad request. Use valid data. </response>
        [HttpGet]
        [Route("{postId}/like")]
        public ActionResult AddLike(long postId)
        {
            try
            {
                int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));

                Dictionary<string, Like> res = new Dictionary<string, Like>();
                res = this._news.Like(userId, postId);
                foreach (KeyValuePair<string, Like> keyValuePair in res)
                {
                    if (Convert.ToBoolean(keyValuePair.Key) == true)
                    {
                        this.UserContext.Likes.Remove(keyValuePair.Value);
                    }
                    else
                    {
                        Like toAdd = new Like { User = keyValuePair.Value.User, News = keyValuePair.Value.News };
                        this.UserContext.Likes.Add(toAdd);
                    }
                }

                this.UserContext.SaveChanges();
                this._logger.LogInformation($"Like Added successfully");
                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error when adding like -- {ex}");
                return this.BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("bookmarks")]
        public IActionResult AddBookmark(object Json)
        {
            try
            {
                int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var post = JsonConvert.DeserializeObject<News>(Json.ToString());
                Dictionary<string, Bookmark> res = new Dictionary<string, Bookmark>();
                res = this._news.Bookmark(userId, post.Id);
                foreach (KeyValuePair<string, Bookmark> keyValuePair in res)
                {
                    if (Convert.ToBoolean(keyValuePair.Key) == true)
                    {
                        this.UserContext.Bookmarks.Remove(keyValuePair.Value);
                    }
                    else
                    {
                        Bookmark toAdd = new Bookmark { User = keyValuePair.Value.User, News = keyValuePair.Value.News };
                        this.UserContext.Bookmarks.Add(toAdd);
                    }
                }

                this.UserContext.SaveChanges();
                this._logger.LogInformation($"Like Added successfully");
                return this.Ok();
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error when adding like -- {ex}");
                return this.BadRequest(ex);
            }
        }

        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /Todo
        ///     {
        ///        "NewsId": 3,
        ///        "commentText" : "Текст комментария"
        ///     }
        ///
        /// </remarks>
        /// <param name="newComment"></param>
        /// <returns>A newly created TodoItem</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">Bad request. Use valid data</response>

        [HttpPost]
        [Route("{postId}/Comment")]
        public IActionResult AddNewsComment([FromRoute] int postId, [FromBody] Comment comment)
        {
            try
            {
                int userId = Convert.ToInt32(this.authorization.DecodeToken(this.Request.Headers["Authorization"].ToString().Substring(7)));
                var result = this._news.Comment(userId, postId, comment.commentText);
                return this.Ok(result.id);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error when adding comment -- {ex}");
                return this.BadRequest(ex);
            }
        }

        [HttpPatch]
        public ActionResult ChangeNews([FromBody] News news)
        {
            try
            {
                var result = this._news.ChangeNews(news);
                this._logger.LogInformation($"Updated news id: {news.Id}");
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }

        [HttpDelete]
        public ActionResult DeleteNews([FromQuery] int id)
        {

            try
            {
                var result = this._news.DeleteNews(id);
                this._logger.LogInformation($"SUCCESS -- {result}");
                return Ok(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                this._logger.LogError($"ERROR -- {ex}");
                return this.BadRequest(ex);
            }
        }
    }
}