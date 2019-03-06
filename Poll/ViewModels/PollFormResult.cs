using System.Collections.Generic;
using System.Linq;

namespace Poll.ViewModels {
    /// <summary>
    /// Result of send poll form.
    /// </summary>
    public class PollFormResult {

        public IDictionary<int, int> SingleAnswer { get; set; }
        public IDictionary<int, int[]> MultiAnswer { get; set; }

    }
}
