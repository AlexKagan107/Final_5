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
            //ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "idDoctor");
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");

            return View();
        }

        // POST: DateTimeByDoctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,idDoctor,practicsName,insertDate,turnId")] DateTimeByDoctor dateTimeByDoctor)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        var resultSelect = (db.DateTimeByDoctor.Where(x => x.insertDate == dateTimeByDoctor.insertDate)
        //                                              .Where(x => x.idDoctor == dateTimeByDoctor.idDoctor)
        //                                              .Where(x => x.practicsName == dateTimeByDoctor.practicsName)).ToList();
        //        if (resultSelect.Count == 0)
        //        {
        //            dateTimeByDoctor.turnId = "0";
        //            db.DateTimeByDoctor.Add(dateTimeByDoctor);
        //            await db.SaveChangesAsync();
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "The doctor is not available");
        //        }
        //    }

        //    ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "firstName", dateTimeByDoctor.idDoctor);
        //    ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", dateTimeByDoctor.practicsName);

        //    return View(dateTimeByDoctor);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DateTimeByDoctor dateTimeByDoctor)
        {
            return RedirectToAction("DoctorByBrunche", new { practicsName = dateTimeByDoctor.practicsName });
        }

        [HttpGet]
        public async Task<ActionResult> DoctorByBrunche(string practicsName)
        {
            DateTimeByDoctor doc = new DateTimeByDoctor();
            doc.practicsName = practicsName;
            var resultCity = db.Brunch.Where(x => x.practicsName.Equals(practicsName)).ToList();
            var resultDocId = db.Doctor.Where(x => x.practicsName.Equals(practicsName)).ToList();

            List<String> listCity = new List<string>();
            for (int i = 0; i < resultCity.Count; i++)
            {
                listCity.Add(resultCity[i].city);
            }

            List<String> listDoc = new List<string>();
            for (int i = 0; i < resultDocId.Count; i++)
            {
                listDoc.Add(resultDocId[i].idDoctor);
            }

            ViewBag.city = new SelectList(listCity, "city");
            ViewBag.idDoctor = new SelectList(listDoc, "idDoctor");
            return View(doc);
        }


        [HttpPost]
        public async Task<ActionResult> DoctorByBrunche([Bind(Include = "Id,idDoctor,practicsName,insertDate,turnId,city")] DateTimeByDoctor dateTimeByDoctor)
        {

            if (ModelState.IsValid)
            {

                var resultSelect = (db.DateTimeByDoctor.Where(x => x.insertDate == dateTimeByDoctor.insertDate)
                                                      .Where(x => x.idDoctor == dateTimeByDoctor.idDoctor)
                                                      .Where(x => x.city == dateTimeByDoctor.city)
                                                      .Where(x => x.practicsName == dateTimeByDoctor.practicsName)).ToList();
                if (resultSelect.Count == 0)
                {
                    dateTimeByDoctor.turnId = "0";
                    db.DateTimeByDoctor.Add(dateTimeByDoctor);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The doctor is not available");
                }
            }

            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "idDoctor", dateTimeByDoctor.idDoctor);
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", dateTimeByDoctor.practicsName);
            ViewBag.practicsName = new SelectList(db.Brunch, "city", "city", dateTimeByDoctor.city);

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
            //ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "idDoctor", dateTimeByDoctor.idDoctor);
            ViewBag.city = new SelectList(db.Brunch, "city", "city", dateTimeByDoctor.city);

            var result = db.Doctor.Where(x => x.practicsName.Equals(dateTimeByDoctor.practicsName)).ToList();
            List<string> resSelect = new List<string>();
            for (int i = 0; i < result.Count; i++)
            {
                resSelect.Add(result[i].idDoctor);
            }
            ViewBag.idDoctor = new SelectList(resSelect, "idDoctor");

            return View(dateTimeByDoctor);
        }

        // POST: DateTimeByDoctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,idDoctor,practicsName,insertDate,turnId,city")] DateTimeByDoctor dateTimeByDoctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dateTimeByDoctor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idDoctor = new SelectList(db.Doctor, "idDoctor", "idDoctor", dateTimeByDoctor.idDoctor);
            // ViewBag.city = new SelectList(db.Brunch, "city", "city", dateTimeByDoctor.city);


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

        public async Task<ActionResult> SearchDoctor(string practicsName,string city)
        {
            if (practicsName != null)
            {
                var result = db.DateTimeByDoctor.Where(x => x.practicsName.Equals(practicsName))
                                                .Where(x => x.city.Equals(city))
                                                .Where(x => x.turnId.Equals("0")).ToList();
                var dateTimeByDoctor = db.DateTimeByDoctor.Include(d => d.Doctor);
                return View(result);
            }
            else
            {
                return View();
            }
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
