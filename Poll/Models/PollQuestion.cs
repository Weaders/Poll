using System.Collections.Generic;

namespace Poll.Models {

    public enum QuestionType {
        Single,
        Multi
    }

    public class PollQuestion {

        public int Id { get; set; }

        public QuestionType QuestionType { get; set; }

        public string Text { get; set; }

        public virtual ICollection<PollAnswer> Answers { get; set; }

    }
}
