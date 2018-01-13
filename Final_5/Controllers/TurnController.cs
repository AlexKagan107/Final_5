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
using System.Threading;

namespace Final_5.Controllers
{
    public class TurnController : Controller
    {
        private Database1Entities db = new Database1Entities();
        // GET: Turn
        public async Task<ActionResult> Index()
        {
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

        // GET: Turn/Details/5
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

        // GET: Turn/Create
        public ActionResult Create()
        {
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName");

            if (Session["userId"] != null)
            {
                Turn tt = new Turn();
                tt.userId = Session["userId"].ToString();
                ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName");
                ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName");

                return View(tt);
            }
            else
            {
                return View();

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Turn turn)
        {

            if (Session["userId"] != null)
            {
                turn.userId = Session["userId"].ToString();
                return RedirectToAction("CreateUserByBrunc", new { practicsName = turn.practicsName });
            }
            else
            {
                ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", turn.practicsName);
                ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName");

                return View();
            }

        }


        [HttpGet]
        public async Task<ActionResult> CreateUserByBrunc(string practicsName)
        {
            Turn t = new Turn();
            if (Session["userId"] != null)
            {
                t.userId = Session["userId"].ToString();
                t.practicsName = practicsName;

                var result = db.Brunch.Where(x => x.practicsName.Equals(practicsName)).ToList();
                var resultDate = db.DateTimeByDoctor.Where(x => x.practicsName.Equals(practicsName)).ToList();

                List<String> resSelect = new List<string>();
                List<DateTime> resDate = new List<DateTime>();
                for (int i = 0; i < result.Count; i++)
                {

                    resSelect.Add(result[i].city);
                }


                for (int i = 0; i < resultDate.Count; i++)
                {
                    if (resultDate[i].turnId.Equals("0"))
                    {
                        resDate.Add(resultDate[i].insertDate);
                    }
                    else
                    {
                        continue;
                    }
                }

                ViewBag.city = new SelectList(resSelect, "city");
                ViewBag.date = new SelectList(resDate, "date");

                return View(t);
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public async Task<ActionResult> CreateUserByBrunc(Turn t, DateTime? date)
        {
            if (date.HasValue)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        t.hour = "";
                        db.Turn.Add(t);
                        await db.SaveChangesAsync();

                        var resultSelect = (db.DateTimeByDoctor
                                            .Where(x => x.practicsName.Equals(t.practicsName))
                                            .Where(x => x.turnId.Equals("0"))
                                            .Where(x => x.insertDate == date.Value)
                                           ).ToList();

                        if (resultSelect.Count == 1)
                        {
                            int idDateTimeByDoc = resultSelect[0].Id;
                            DateTimeByDoctor dateTimeByDoctor = await db.DateTimeByDoctor.FindAsync(idDateTimeByDoc);
                            dateTimeByDoctor.turnId = "1";
                            if (ModelState.IsValid)
                            {
                                db.Entry(dateTimeByDoctor).State = EntityState.Modified;
                                await db.SaveChangesAsync();
                                return RedirectToAction("Index");
                            }
                        }
                    }
                    catch (Exception)
                    {
                        return View();
                    }

                }
            }

            ViewBag.practicsName = new SelectList(db.Practics, "practicsNameId", "practicsName", t.practicsName);
            var resultDate2 = db.DateTimeByDoctor.Where(x => x.practicsName.Equals(t.practicsName)).ToList();
            List<DateTime> resDate = new List<DateTime>();

            for (int i = 0; i < resultDate2.Count; i++)
            {
                if (resultDate2[i].turnId.Equals("0"))
                {
                    resDate.Add(resultDate2[i].insertDate);
                }
                else
                {
                    continue;
                }
            }


            var result = db.Brunch.Where(x => x.practicsName.Equals(t.practicsName)).ToList();

            List<String> resSelect = new List<string>();
            for (int i = 0; i < result.Count; i++)
            {

                resSelect.Add(result[i].city);
            }

            ViewBag.city = new SelectList(resSelect, "city");

            ViewBag.date = new SelectList(resDate, "date");
            return View(t);

        }


        // GET: Turn/Edit/5
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

            var result = db.Brunch.Where(x => x.practicsName.Equals(turn.practicsName)).ToList();
            List<string> resSelect = new List<string>();
            for (int i = 0; i < result.Count; i++)
            {
                resSelect.Add(result[i].city);
            }
            ViewBag.city = new SelectList(resSelect, "city");

            return View(turn);
        }

        // POST: Turn/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,userId,practicsName,date,hour,city,comment,medicineName")] Turn turn)
        {
            if (ModelState.IsValid)
            {
                turn.hour = "";
                db.Entry(turn).State = EntityState.Modified;
                await db.SaveChangesAsync();
                Database1Entities db2 = new Database1Entities();

                MedicalSheet m = new MedicalSheet();
                m.userId = turn.userId;
                m.turnId = turn.Id;
                m.dateTime = DateTime.Now.Date;
                m.comment = turn.comment;
                m.medicineName = turn.medicineName;
                db2.MedicalSheet.Add(m);
                await db2.SaveChangesAsync();

                return RedirectToAction("allTurnByUser", "Turn", new { userId = turn.userId });

            }
            ViewBag.practicsName = new SelectList(db.Practics, "practicsName", "practicsName", turn.practicsName);
            ViewBag.medicineName = new SelectList(db.Medicine, "medicineName", "medicineName", turn.medicineName);

            return View(turn);
        }

        // GET: Turn/Delete/5
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

        // POST: Turn/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Turn turn = await db.Turn.FindAsync(id);
            db.Turn.Remove(turn);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> allTurnByUser(string userId)
        {
            if (userId != null)
            {
                var result = db.Turn.Where(x => x.userId.Equals(userId)).ToList();
                var turn = db.Turn.Include(t => t.Practics);
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
