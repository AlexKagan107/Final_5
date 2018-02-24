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

        public async Task<ActionResult> turn(string str)
        {
            Turn t = new Turn();
            DateTimeByDoctor d = new DateTimeByDoctor();
            var dayString = "";
            string userId = Session["userId"].ToString();

            List<Models.Turn> turn = await db.Turn.ToListAsync();
            // IEnumerable<string> userNames = turn.Select(x => x.practicsName);
            var y = str.Split(' ');
            string actualpracticsName = y[0];
            string actualTime = y[1];
            string day = y[3];
            string month = y[4];

            if (day.Equals("one"))
                day = "1";

            if ( month.Equals("january"))
            {
                dayString = "1";
            }
            if (month.Equals("february"))
            {
                dayString = "2";
            }
            if (month.Equals("march"))
            {
                dayString = "3";
            }
            if (month.Equals("april"))
            {
                dayString = "4";
            }
            if (month.Equals("may"))
            {
                dayString = "5";
            }
            if (month.Equals("june"))
            {
                dayString = "6";
            }
            if (month.Equals("july"))
            {
                dayString = "7";
            }
            if (month.Equals("august"))
            {
                dayString = "8";
            }
            if (month.Equals("september"))
            {
                dayString = "9";
            }
            if (month.Equals("october"))
            {
                dayString = "10";
            }
            if (month.Equals("november"))
            {
                dayString = "11";
            }
            if (month.Equals("december"))
            {
                dayString = "12";
            }

            // var practisTable = db.Practics.Where(x => x.practicsName.Equals(actualpracticsName)).ToList();
            var res = db.DateTimeByDoctor.Where(x => x.practicsName.Equals(actualpracticsName))
                                               .Where(x => x.turnId.Equals("0"))
                                               .Where(x => x.insertDate.ToString().Contains(actualTime)).ToList()
                                               .Where(x => x.insertDate.Day.ToString().Equals(day)).ToList()
                                               .Where(x => x.insertDate.Month.ToString().Equals(dayString)).ToList();
            //var yyy = practisTable.Count;
            // var zzz = res.Count;
            if (res.Count == 1)
            {
                string date = day.ToString()+"/"+ dayString.ToString()+"/18"+ " " + actualTime;
                DateTime dt = Convert.ToDateTime(date);
                t.userId = userId;
                t.hour = "";
                t.practicsName = actualpracticsName;
                t.date = dt.ToString();
                t.city = "";
                t.comment = "";
                t.medicineName = "none";
                db.Turn.Add(t);


                int idDateTimeByDoc = res[0].Id;
                DateTimeByDoctor dateTimeByDoctor = await db.DateTimeByDoctor.FindAsync(idDateTimeByDoc);
                dateTimeByDoctor.turnId = "1";
                db.Entry(dateTimeByDoctor).State = EntityState.Modified;// מעדכן

                await db.SaveChangesAsync();
                return RedirectToAction("succefullyInsert");
            }
            else
                return RedirectToAction("failedInsert");


            //bool exists = userNames.Contains(actualpracticsName);

            //if (exists)
            //{
            //    var time = turn.Where(x => x.hour == actualTime).First();
            //    bool timeExits = time.hour.Contains(actualTime);
            //    if (timeExits)
            ////    {
            //t.userId = userId;
            //        t.hour = actualTime;
            //        t.practicsName = actualpracticsName;
            //        db.Turn.Add(t);
            //        await db.SaveChangesAsync();
            //        return Content("The doctor is free at this time");
            //    }
            //    else
            //    {
            //        return Content("The doctor is full");
            //    }

            //}
            //else
            //{
            //    return Content("No such doctor");
            //}

        }





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


        public ActionResult succefullyInsert()
        {
      
            
                return View();
            
        }

        public ActionResult failedInsert()
        {


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

//bdika
//bdika2