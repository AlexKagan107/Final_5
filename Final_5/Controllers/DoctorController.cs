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

namespace Final_5.Controllers
{
    public class DoctorController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Doctor
        public async Task<ActionResult> Index()
        {
            var doctor = db.Doctor.Include(d => d.Practics);
            return View(await doctor.ToListAsync());
        }

        // GET: Doctor/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await db.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idDoctor,firstName,lastName,practicsName")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Doctor.Add(doctor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", doctor.practicsName);
            return View(doctor);
        }

        // GET: Doctor/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await db.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", doctor.practicsName);
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idDoctor,firstName,lastName,practicsName")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", doctor.practicsName);
            return View(doctor);
        }

        // GET: Doctor/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = await db.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Doctor doctor = await db.Doctor.FindAsync(id);
            db.Doctor.Remove(doctor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> SearchDoctor(string str)
        {
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
