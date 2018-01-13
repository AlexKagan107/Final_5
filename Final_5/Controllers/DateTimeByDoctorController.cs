﻿using System;
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
    public class DateTimeByDoctorController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: DateTimeByDoctor
        public async Task<ActionResult> Index()
        {
            var dateTimeByDoctor = db.DateTimeByDoctor.Include(d => d.Doctor);
            return View(await dateTimeByDoctor.ToListAsync());
        }

        // GET: DateTimeByDoctor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTimeByDoctor dateTimeByDoctor = await db.DateTimeByDoctor.FindAsync(id);
            if (dateTimeByDoctor == null)
            {
                return HttpNotFound();
            }
            return View(dateTimeByDoctor);
        }

        // GET: DateTimeByDoctor/Create
        public ActionResult Create()
        {
            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "idDoctor");

            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");

            return View();
        }

        // POST: DateTimeByDoctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,idDoctor,practicsName,insertDate,turnId")] DateTimeByDoctor dateTimeByDoctor)
        {
            if (ModelState.IsValid)
            {

                var resultSelect = (db.DateTimeByDoctor.Where(x => x.insertDate == dateTimeByDoctor.insertDate)
                                                      .Where(x => x.idDoctor == dateTimeByDoctor.idDoctor)
                                                      .Where(x => x.practicsName == dateTimeByDoctor.practicsName)).ToList();
                if (resultSelect.Count == 0)
                {
                    db.DateTimeByDoctor.Add(dateTimeByDoctor);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The doctor is not available");
                }
            }

            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName", dateTimeByDoctor.idDoctor);
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", dateTimeByDoctor.practicsName);

            return View(dateTimeByDoctor);
        }

        // GET: DateTimeByDoctor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTimeByDoctor dateTimeByDoctor = await db.DateTimeByDoctor.FindAsync(id);
            if (dateTimeByDoctor == null)
            {
                return HttpNotFound();
            }
            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName", dateTimeByDoctor.idDoctor);
            return View(dateTimeByDoctor);
        }

        // POST: DateTimeByDoctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,idDoctor,practicsName,insertDate,turnId")] DateTimeByDoctor dateTimeByDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dateTimeByDoctor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName", dateTimeByDoctor.idDoctor);
            return View(dateTimeByDoctor);
        }

        // GET: DateTimeByDoctor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateTimeByDoctor dateTimeByDoctor = await db.DateTimeByDoctor.FindAsync(id);
            if (dateTimeByDoctor == null)
            {
                return HttpNotFound();
            }
            return View(dateTimeByDoctor);
        }

        // POST: DateTimeByDoctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            DateTimeByDoctor dateTimeByDoctor = await db.DateTimeByDoctor.FindAsync(id);
            db.DateTimeByDoctor.Remove(dateTimeByDoctor);
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
    }
}
