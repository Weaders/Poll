using Poll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poll.Services {

    /// <summary>
    // Used for save validation and process result.
    /// </summary>
    public class PollFormValidProcessResult : IPollFormValidProcessResult {

        public bool IsValid { get; set; } = false;

        public IDictionary<PollQuestion, UserAnswerSelectData> SelectedSingleAnswer { get; set; } = new Dictionary<PollQuestion, UserAnswerSelectData>();
        public IDictionary<PollQuestion, UserAnswerSelectData[]> SelectedMultipleAnswer { get; set; } = new Dictionary<PollQuestion, UserAnswerSelectData[]>();

    }
}
