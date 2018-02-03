using Final_5.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Final_5.Controllers
{
    public class LoginController : Controller
    {
        //Login.Form1 log;

        private Database1Entities db = new Database1Entities();

        // GET: Login
        //[HttpGet]
        //public async Task<ActionResult> Login()
        //{
        //    return View();
        //}
        public ActionResult Login()
        {
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Users model)
        {
            string result = "fail";
            Users user = await db.Users.SingleOrDefaultAsync(x => x.userId == model.userId && x.password == model.password);
            if (user != null)
            {
                if (user.fileFinger == "" || user.fileFinger == "not") {
                    Session["userId"] = user.userId.ToString();
                    Session["userName"] = user.firstName.ToString();
                    Session["permissions"] = user.permissions.ToString();
                    result = "reg_user";
                }
                else
                {
                    Session["userId"] = user.userId.ToString();
                    Session["userName"] = user.firstName.ToString();
                    Session["permissions"] = user.permissions.ToString();
                    result = "fin_user";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
            //try
            //{
            //    string userId = users.userId;
            //    var user = (db.Users.Where(x => x.userId == users.userId)
            //                       .Where(x => x.password == users.password)).ToList();

            //    if (user == null || user.Count == 0)
            //    {
            //        MessageBox.Show("ID or Password INVALID! Please try again.");
            //        return View(users);
            //    }

            //    if (user.Count == 1 && (user[0].fileFinger == "" || user[0].fileFinger == "not"))
            //    {
            //        Session["userId"] = user[0].userId.ToString();
            //        Session["userName"] = user[0].firstName.ToString();
            //        Session["permissions"] = user[0].permissions.ToString();
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        Session["userId"] = user[0].userId.ToString();
            //        Session["userName"] = user[0].firstName.ToString();
            //        Session["permissions"] = user[0].permissions.ToString();
            //        return RedirectToAction("Inde2", "Home2", new { area = "AnotherArea" });

            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw;
            //}
            //return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> loginFinger()
        {
            string result = "fail";
            string name = "";

            //log = new Login.Form1();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(log);

            //name = log.ThisName();

            if (name != "")
            {
                Session["userName"] = name;
                Session["permissions"] = "2";
                result = "user";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SearchDoctor(string str)
        {
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");
            return View();
        }
    }
}