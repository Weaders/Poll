using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Poll.Models;
using Poll.Services;
using Poll.ViewModels;

namespace Poll.Controllers {
    public class HomeController : Controller {

        private readonly IPollManager _pollManager;

        public HomeController(IPollManager pollManager) {
            _pollManager = pollManager;
        }

        public IActionResult Index() {
            return View(_pollManager.GetPollView());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitPoll(PollFormResult formResult) {

            var validProcess = _pollManager.ValidProcessPollForm(formResult);

            if (validProcess.IsValid) {

                await _pollManager.AddPollFormResult(validProcess);
                return Json("Ok");

            } else {

                return new JsonResult("Bad data") {
                    StatusCode = 400
                };

            }

        }

    }
}
