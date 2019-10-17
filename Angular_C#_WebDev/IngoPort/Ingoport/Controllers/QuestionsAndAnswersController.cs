namespace Ingoport.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    [Route("api/qna")]
    [ApiController]
    [Authorize]
    public class QuestionsAndAnswersController : ControllerBase
    {

        private readonly IQuestionsAndAnswers _questionsAndAnswers;
        private readonly ILogger<QuestionsAndAnswersController> _logger;
        private readonly UserContext UserContext;

        public QuestionsAndAnswersController(IQuestionsAndAnswers questionsAndAnswers, ILogger<QuestionsAndAnswersController> logger, UserContext UserContext)
        {
            this._questionsAndAnswers = questionsAndAnswers;
            this._logger = logger;
            this.UserContext = UserContext;
        }

        [HttpGet]
        public IActionResult GetQuestionsAndAnswers()
        {
            try
            {
                var result = this._questionsAndAnswers.GetAllQuestions();
                this._logger.LogInformation("Successfully return data for questions and answers page");
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logger.LogInformation($"ERROR -- {ex}");
                return this.BadRequest(ex.Message);
            }
        }
    }
}