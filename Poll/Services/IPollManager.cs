using Poll.Models;
using Poll.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poll.Services {
       
    public interface IPollManager {

        Task<UserAnswer[]> AddPollFormResult(IPollFormValidProcessResult pollFormResult);
        PollFormView GetPollView();
        IPollFormValidProcessResult ValidProcessPollForm(PollFormResult pollFormResult);

    }

    public interface IPollFormValidProcessResult {

        IDictionary<PollQuestion, UserAnswerSelectData> SelectedSingleAnswer { get; set; }
        IDictionary<PollQuestion, UserAnswerSelectData[]> SelectedMultipleAnswer { get; set; }

        bool IsValid { get; } 
    }

}
