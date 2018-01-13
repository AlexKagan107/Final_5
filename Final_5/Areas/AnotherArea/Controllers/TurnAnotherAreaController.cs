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

namespace Final_5.Areas.AnotherArea.Controllers
{
    public class TurnAnotherAreaController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: AnotherArea/TurnAnotherArea
        public async Task<ActionResult> Index()
        {
            // var turn = db.Turn.Include(t => t.Practics).Include(t => t.Medicine);
            // return View(await turn.ToListAsync());

            string userId = "";
            if (Session["userId"] != null)
            {
                userId = Session["userId"].ToString();
                var result = db.Turn.Where(x => x.userId.Equals(userId)).ToList();
                var turn = db.Turn.Include(t => t.Practics);
                return View(result);
            }
            else
            {
                return View();
            }
        }

        // GET: AnotherArea/TurnAnotherArea/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turn turn = await db.Turn.FindAsync(id);
            if (turn == null)
            {
                return HttpNotFound();
            }
            return View(turn);
        }

        // GET: AnotherArea/TurnAnotherArea/Create
        public ActionResult Create()
        {
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName");
            return View();
        }

        // POST: AnotherArea/TurnAnotherArea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,userId,practicsName,date,hour,city,comment,medicineName")] Turn turn)
        {
            if (ModelState.IsValid)
            {
                db.Turn.Add(turn);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", turn.practicsName);
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName", turn.medicineName);
            return View(turn);
        }

        // GET: AnotherArea/TurnAnotherArea/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turn turn = await db.Turn.FindAsync(id);
            if (turn == null)
            {
                return HttpNotFound();
            }
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", turn.practicsName);
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName", turn.medicineName);
            return View(turn);
        }

        // POST: AnotherArea/TurnAnotherArea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,userId,practicsName,date,hour,city,comment,medicineName")] Turn turn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(turn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", turn.practicsName);
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName", turn.medicineName);
            return View(turn);
        }

        // GET: AnotherArea/TurnAnotherArea/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turn turn = await db.Turn.FindAsync(id);
            if (turn == null)
            {
                return HttpNotFound();
            }
            return View(turn);
        }

        // POST: AnotherArea/TurnAnotherArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Turn turn = await db.Turn.FindAsync(id);
            db.Turn.Remove(turn);
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
