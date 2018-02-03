using Final_5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Final_5.Controllers
{
    public class SerachController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Serach
        [HttpGet]
        public async Task<ActionResult> search_1()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> search_1(SearchUser str)
        {
            if (str != null)
            {
                if (str.userId != null)
                {
                    Session["userNowId"] = str.userId.ToString();
                    return RedirectToAction("allTurnByUser", "Turn", new { userId = str.userId });
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
    }
}