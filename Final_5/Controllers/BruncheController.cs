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
    public class BruncheController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: Brunche
        public async Task<ActionResult> Index()
        {
            return View(await db.Brunch.ToListAsync());
        }

        // GET: Brunche/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brunch brunch = await db.Brunch.FindAsync(id);
            if (brunch == null)
            {
                return HttpNotFound();
            }
            return View(brunch);
        }

        // GET: Brunche/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brunche/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "brunchNameId,practicsName,city,street")] Brunch brunch)
        {
            if (ModelState.IsValid)
            {
                db.Brunch.Add(brunch);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(brunch);
        }

        // GET: Brunche/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brunch brunch = await db.Brunch.FindAsync(id);
            if (brunch == null)
            {
                return HttpNotFound();
            }
            return View(brunch);
        }

        // POST: Brunche/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "brunchNameId,practicsName,city,street")] Brunch brunch)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brunch).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(brunch);
        }

        // GET: Brunche/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brunch brunch = await db.Brunch.FindAsync(id);
            if (brunch == null)
            {
                return HttpNotFound();
            }
            return View(brunch);
        }

        // POST: Brunche/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Brunch brunch = await db.Brunch.FindAsync(id);
            db.Brunch.Remove(brunch);
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
