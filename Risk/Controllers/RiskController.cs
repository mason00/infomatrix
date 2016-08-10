using Risk.Service;
using System.Web.Mvc;

namespace Risk.Controllers
{
    public class RiskController : Controller
    {
        IBetService betService;

        public RiskController(IBetService service)
        {
            betService = service;
        }

        public ActionResult HighWin()
        {
            ViewBag.Message = "Win more than 60% customers";

            return View(betService.GetUnusualWin());
        }

        public ActionResult HighBets()
        {
            ViewBag.Message = "Want to win more than 60% customers";

            return View(betService.GetHighRiskBets());
        }

        public ActionResult TenTimesBet()
        {
            ViewBag.Message = "Bets for win 10 times more";

            return View("UnusualBets", betService.GetUnsettledHighWinRate());
        }

        public ActionResult ThirtyTimesBet()
        {
            ViewBag.Message = "Bets for win 30 times more";

            return View("UnusualBets", betService.GetExtremeHighWinRate());
        }

        public ActionResult BigWin()
        {
            ViewBag.Message = "Win more than $1000";

            return View(betService.GetUnsettledBigWin());
        }
    }
}