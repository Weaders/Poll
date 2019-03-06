using System.Collections.Generic;
using System.Linq;

namespace Poll.ViewModels {
    public class PollFormResult {

        public IDictionary<int, int> SingleAnswer { get; set; }
        public IDictionary<int, int[]> MultiAnswer { get; set; }

    }
}
