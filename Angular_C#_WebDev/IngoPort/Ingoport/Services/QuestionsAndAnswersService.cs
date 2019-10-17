namespace Ingoport.Services
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using Ingoport.Interfaces;
    using Ingoport.Models;

    public class QuestionsAndAnswersService : IQuestionsAndAnswers
    {
        private readonly UserContext UserContext;

        public QuestionsAndAnswersService(UserContext UserContext)
        {
            this.UserContext = UserContext;
        }

        public string GetAllQuestions()
        {
            Dictionary<string, object> allQuestions = new Dictionary<string, object>();
            allQuestions.Add("Topics", this.UserContext.QuestionTopics.Select(c => new { Id = c.Id, Title = c.Name }));

            var result = this.UserContext.Questions.Join(this.UserContext.Answers, c => c.Id, p => p.QuestionId,
            (c, p) => new { TopicId = c.QuestionTopicId, Question = c.Text, Answer = (c.Answers.Where(g => g.QuestionId == c.Id).Select(k => new { Id = k.Id, Text = k.Text })), QuestionId = c.Id }).ToList();

            for (int i = 0; i < result.Count() - 1; i++)
            {
                if (result[i].QuestionId == result[i + 1].QuestionId)
                {
                    result.RemoveAt(i + 1);
                }
            }

            allQuestions.Add("QuestionsAndAnswers", result);

            return JsonConvert.SerializeObject(allQuestions);
        }
    }
}
