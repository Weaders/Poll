using Poll.Models;

namespace Poll.ViewModels {
    public interface IQuestionView {

        int Id { get; }
        string Text { get; }
        QuestionType QuestionType { get; }

        string GetTemplateName();

    }
}
