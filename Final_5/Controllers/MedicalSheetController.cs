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
    public class MedicalSheetController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: MedicalSheet
        public async Task<ActionResult> Index()
        {
            var medicalSheet = db.MedicalSheet.Include(m => m.Medicine).Include(m => m.Turn);
            return View(await medicalSheet.ToListAsync());
        }

        // GET: MedicalSheet/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalSheet medicalSheet = await db.MedicalSheet.FindAsync(id);
            if (medicalSheet == null)
            {
                return HttpNotFound();
            }
            return View(medicalSheet);
        }

        // GET: MedicalSheet/Create
        public ActionResult Create()
        {
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName");
            ViewBag.turnId = new SelectList(db.Turn, "Id", "userId");
            return View();
        }

        // POST: MedicalSheet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,turnId,userId,dateTime,comment,medicineName")] MedicalSheet medicalSheet)
        {
            if (ModelState.IsValid)
            {
                db.MedicalSheet.Add(medicalSheet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName", medicalSheet.medicineName);
            ViewBag.turnId = new SelectList(db.Turn, "Id", "userId", medicalSheet.turnId);
            return View(medicalSheet);
        }

        // GET: MedicalSheet/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalSheet medicalSheet = await db.MedicalSheet.FindAsync(id);
            if (medicalSheet == null)
            {
                return HttpNotFound();
            }
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName", medicalSheet.medicineName);
            ViewBag.turnId = new SelectList(db.Turn, "Id", "userId", medicalSheet.turnId);
            return View(medicalSheet);
        }

        // POST: MedicalSheet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,turnId,userId,dateTime,comment,medicineName")] MedicalSheet medicalSheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicalSheet).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName", medicalSheet.medicineName);
            ViewBag.turnId = new SelectList(db.Turn, "Id", "userId", medicalSheet.turnId);
            return View(medicalSheet);
        }

        // GET: MedicalSheet/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicalSheet medicalSheet = await db.MedicalSheet.FindAsync(id);
            if (medicalSheet == null)
            {
                return HttpNotFound();
            }
            return View(medicalSheet);
        }

        // POST: MedicalSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MedicalSheet medicalSheet = await db.MedicalSheet.FindAsync(id);
            db.MedicalSheet.Remove(medicalSheet);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> allMedicalSheet()
        {
            if (Session["userId"] != null)
            {
                string userId = Session["userId"].ToString();
                var result = db.MedicalSheet.Where(x => x.userId.Equals(userId)).ToList();
                var medicalSheet = db.MedicalSheet.Include(m => m.Medicine).Include(m => m.Turn);
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
