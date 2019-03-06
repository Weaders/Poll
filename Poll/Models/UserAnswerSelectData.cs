namespace Poll.Models {

    /// <summary>
    /// Stored data of answer, when need to select one of predefined question answers
    /// </summary>
    public class UserAnswerSelectData : IAnswerUserData {

        public int Id { get; set; }

        public PollAnswer Answer { get; set; }

        public UserAnswer UserAnswer { get; set; }

    }
}
