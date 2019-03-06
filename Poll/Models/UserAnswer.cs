using System;

namespace Poll.Models {

    /// <summary>
    /// User answer of question.
    /// Data of answer stored in class inheritable from IAnswerUserData, for example - UserAnswerSelectData
    /// </summary>
    public class UserAnswer {

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public PollQuestion Question { get; set; }

    }

}
