using Poll.Models;

namespace Poll.ViewModels {
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
