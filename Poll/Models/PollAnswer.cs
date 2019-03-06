using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poll.Models {

    /// <summary>
    /// Answers on question
    /// </summary>
    public class PollAnswer {

        public int Id { get; set; }
        public string Text { get; set; }

        public int QuestionId { get; set; }

        public PollQuestion Question { get; set; }

    }
}
