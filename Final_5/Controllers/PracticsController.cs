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
    public class PracticsController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Practics
        public async Task<ActionResult> Index()
        {
            return View(await db.Practics.ToListAsync());
        }

        // GET: Practics/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practics practics = await db.Practics.FindAsync(id);
            if (practics == null)
            {
                return HttpNotFound();
            }
            return View(practics);
        }

        // GET: Practics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Practics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "practicsName")] Practics practics)
        {
            if (ModelState.IsValid)
            {
                db.Practics.Add(practics);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(practics);
        }

        // GET: Practics/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practics practics = await db.Practics.FindAsync(id);
            if (practics == null)
            {
                return HttpNotFound();
            }
            return View(practics);
        }

        // POST: Practics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "practicsName")] Practics practics)
        {
            if (ModelState.IsValid)
            {
                db.Entry(practics).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(practics);
        }

        // GET: Practics/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Practics practics = await db.Practics.FindAsync(id);
            if (practics == null)
            {
                return HttpNotFound();
            }
            return View(practics);
        }

        // POST: Practics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Practics practics = await db.Practics.FindAsync(id);
            db.Practics.Remove(practics);
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
