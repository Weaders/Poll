using Poll.Models;
using Poll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poll.Services {

    public class PollManager : IPollManager {

        /// <summary>
        // Used for save validation and process result.
        /// </summary>
        public class PollFormValidProcessResult : IPollFormValidProcessResult {

            public bool IsValid { get; set; } = false;

            public IDictionary<PollQuestion, UserAnswerSelectData> SelectedSingleAnswer { get; set; } = new Dictionary<PollQuestion, UserAnswerSelectData>();
            public IDictionary<PollQuestion, UserAnswerSelectData[]> SelectedMultipleAnswer { get; set; } = new Dictionary<PollQuestion, UserAnswerSelectData[]>();

        }

        private readonly DefaultContext _dbContext;

        public PollManager(DefaultContext dbContext) {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Check is valid form and process her.
        /// </summary>
        /// <param name="pollFormResult"></param>
        /// <returns></returns>
        public IPollFormValidProcessResult ValidProcessPollForm(PollFormResult pollFormResult) {

            var result = new PollFormValidProcessResult();

            var questions = _dbContext.Questions.ToArray();
            var answers = _dbContext.Answers.ToArray();

            //Add to result questions with single answer
            foreach (var questionIdAnswerId in pollFormResult.SingleAnswer) {

                var question = questions.FirstOrDefault((q) => q.Id == questionIdAnswerId.Key);
                var answer = answers.FirstOrDefault(a => a.Id == questionIdAnswerId.Value);

                // Not found question or answer in db.
                if (question == null || answer == null) {
                    return result;
                }

                result.SelectedSingleAnswer.Add(question, new UserAnswerSelectData() {
                    Answer = answer
                });

            }

            //Add to result questions with multiple answers
            foreach (var questionIdAnswerIds in pollFormResult.MultiAnswer) {

                //Search by question, by question id.
                var question = questions.FirstOrDefault(q => q.Id == questionIdAnswerIds.Key);

                //There no question with this id in db.
                if (question == null) {
                    return result;
                }

                var answersToAdd = answers.Where(answer => questionIdAnswerIds.Value.Contains(answer.Id)).ToArray();

                //There no answer with this id in db.
                if (answersToAdd.Length == 0 || questionIdAnswerIds.Value.Length != answersToAdd.Length) {
                    return result;
                }

                var answersData = answersToAdd.Select((answer) =>
                    new UserAnswerSelectData() {
                        Answer = answer
                    }
                ).ToArray();

                result.SelectedMultipleAnswer.Add(question, answersData);

            }

            result.IsValid = true;

            return result;

        }

        /// <summary>
        /// Add to db questions with answers.
        /// </summary>
        /// <param name="pollFormResult"></param>
        /// <returns></returns>
        public async Task<UserAnswer[]> AddPollFormResult(IPollFormValidProcessResult pollFormResult) {

            var tasksAddsingleAnswer = pollFormResult.SelectedSingleAnswer.Select(qRefA =>
                AddUserAnswerWithSelectAnwerData(qRefA.Key, qRefA.Value)
            ).ToArray();

            var tasksAddMultiAnswer = pollFormResult.SelectedMultipleAnswer.Select(qRefA =>
                AddUserAnswerWithSelectAnwerData(qRefA.Key, qRefA.Value)
            ).ToArray();

            var tasks = tasksAddsingleAnswer.Concat(tasksAddMultiAnswer).ToArray();

            Task.WaitAll(tasks);

            await _dbContext.SaveChangesAsync();

            return tasks.Select(task => task.Result).ToArray();

        }

        /// <summary>
        /// Get poll form with questions
        /// </summary>
        /// <returns></returns>
        public PollFormView GetPollView() {

            var questions = _dbContext.Questions.ToArray();
            var answers = _dbContext.Answers.ToArray();

            var qViews = new QuestionSelectAnswer[questions.Length];

            for (var i = 0; i < questions.Length; i++) {

                var questionAnswers = answers.Where(answer => answer.Question == questions[i]).ToArray();
                var answersView = new PollAnswerView[questionAnswers.Length];

                for (var o = 0; o < questionAnswers.Length; o++) {

                    answersView[o] = new PollAnswerView() {
                        Text = questionAnswers[o].Text,
                        Id = questionAnswers[o].Id
                    };

                }

                qViews[i] = new QuestionSelectAnswer() {
                    Text = questions[i].Text,
                    Answers = answersView,
                    QuestionType = questions[i].QuestionType,
                    Id = questions[i].Id
                };

            }

            return new PollFormView {
                Questions = qViews
            };

        }

        private async Task<UserAnswer> AddUserAnswerWithSelectAnwerData(PollQuestion question, params UserAnswerSelectData[] answersData) {

            var addedUserAnswer = await _dbContext.UserAnswers.AddAsync(new UserAnswer() {
                Question = question,
                Date = DateTime.Now,
            });

            foreach (var answerData in answersData) {
                answerData.UserAnswer = addedUserAnswer.Entity;
            }

            await _dbContext.AnswerSelectData.AddRangeAsync(answersData);

            return addedUserAnswer.Entity;

        }

    }

}
