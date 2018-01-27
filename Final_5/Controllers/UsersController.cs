using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Final_5.Models;
using System.Diagnostics;
using System.Windows.Forms;

namespace Final_5.Controllers
{
    public class UsersController : Controller
    {
        Registration.Form1 Reg;

        private Database1Entities db = new Database1Entities();

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(u => u.Doctor);
            return View(await users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "userId,password,fileFinger,firstName,lastName,address,phone,idDoctor,permissions")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName", users.idDoctor);
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName", users.idDoctor);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "userId,password,fileFinger,firstName,lastName,address,phone,idDoctor,permissions")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName", users.idDoctor);
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = await db.Users.FindAsync(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Users users = await db.Users.FindAsync(id);
            db.Users.Remove(users);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<JsonResult> CreateFinger()
        {
            string result = "good";

            ProcessStartInfo stinfo = new ProcessStartInfo();
            stinfo.FileName = @"C:\לימודים\פרויקט גמר\FlexCodeSDK\Registration\Registration\bin\Debug\Registration.exe";
            stinfo.CreateNoWindow = true;
            stinfo.UseShellExecute = true;
            Process myProcess = Process.Start(stinfo);
            //myProcess.Close();
            //return View();

            //return RedirectToAction("Index");

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
