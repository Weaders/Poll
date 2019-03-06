using Poll.Models;

namespace Poll.ViewModels {
    /// <summary>
    /// View of question, on reply that, need select one or more answers.
    /// </summary>
    public class QuestionSelectAnswer : IQuestionView {

        public int Id { get; set; }
        public QuestionType QuestionType { get; set; }
        public string Text { get; set; }
        public PollAnswerView[] Answers { get; set; }

        public string GetTemplateName() {
            return QuestionType.ToString();
        }

    }
}
