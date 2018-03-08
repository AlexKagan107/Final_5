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
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Final_5.Controllers
{
    public class LoginController : Controller
    {
        Login.Form1 log;

        private Database1Entities db = new Database1Entities();

        // GET: Login
        //[HttpGet]
        //public async Task<ActionResult> Login()
        //{
        //    return View();
        //}
        public ActionResult Login()
        {
            //var resultCity = db.Brunch.Select(x => x.city).ToList();

            //List<String> listCity = new List<string>();
            //for (int i = 0; i < resultCity.Count; i++)
            //{
            //    listCity.Add(resultCity[i]);
            //}

            //ViewBag.city = new SelectList(listCity, "city");

            ViewBag.city = new SelectList(db.Cities, "city", "city");

            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login1(Users model)
        {
            string result = "fail";
            string psw = "";
            psw = Encrypt(model.password);
            Users user = await db.Users.SingleOrDefaultAsync(x => x.userId == model.userId && x.password == psw);
            if (user != null)
            {
                if (user.fileFinger == "" || user.fileFinger == "not")
                {
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

            log = new Login.Form1();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(log);

            name = log.ThisName();

            if (name != "")
            {
                Session["userName"] = name;
                Session["permissions"] = "2";
                result = "user";
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchDoctor()
        {
            ViewBag.practicsName = new SelectList(db.DateTimeByDoctor, "practicsName", "practicsName");
            ViewBag.city = new SelectList(db.DateTimeByDoctor, "city", "city");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(DateTimeByDoctor tmp)
        {
            //string flag = "fail";

            //if(tmp != null)
            //{
            //    //flag = "true";
            //    return RedirectToAction("SearchDoctor", "DateTimeByDoctor", new { practicsName = tmp.practicsName, city = tmp.city });
            //}

            //return Json(flag, JsonRequestBehavior.AllowGet);
            return RedirectToAction("SearchDoctor", "DateTimeByDoctor", new { practicsName = tmp.practicsName, city = tmp.city });
        }


        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


    }
}

//test
//test2