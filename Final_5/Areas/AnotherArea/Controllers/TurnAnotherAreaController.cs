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
            var len = y.Length;
            if(len != 5)
                return RedirectToAction("Inde2", "Home2");

            string actualpracticsName = y[0];
            string actualTime = y[1];
            string day = y[3];
            string month = y[4];

            if (day.Equals("one") || day.Equals("1th"))
                day = "1";
            if (day.Equals("two") || day.Equals("to") || day.Equals("2th"))
                day = "2";
            if (day.Equals("three") || day.Equals("3th"))
                day = "3";
            if (day.Equals("four") || day.Equals("for") || day.Equals("4th"))
                day = "4";
            if (day.Equals("five") || day.Equals("5th"))
                day = "5";
            if (day.Equals("six") || day.Equals("6th"))
                day = "6";
            if (day.Equals("seven") || day.Equals("7th"))
                day = "7";
            if (day.Equals("eight") || day.Equals("8th"))
                day = "8";
            if (day.Equals("nine") || day.Equals("9th"))
                day = "9";
            if (day.Equals("ten") || day.Equals("10th"))
                day = "10";
            if (day.Equals("eleven") || day.Equals("11th"))
                day = "11";
            if (day.Equals("twelve") || day.Equals("12th"))
                day = "12";
            if (day.Equals("thirteen") || day.Equals("13th"))
                day = "13";
            if (day.Equals("fourteen") || day.Equals("14th"))
                day = "14";
            if (day.Equals("fifteen") || day.Equals("15th"))
                day = "15";
            if (day.Equals("sixteen") || day.Equals("16th"))
                day = "16";
            if (day.Equals("seventeen") || day.Equals("17th"))
                day = "17";
            if (day.Equals("eighteen") || day.Equals("18th"))
                day = "18";
            if (day.Equals("nineteen") || day.Equals("19th"))
                day = "19";
            if (day.Equals("20th"))
                day = "20";
            if (day.Equals("21th"))
                day = "21";
            if (day.Equals("22th"))
                day = "22";
            if (day.Equals("23th"))
                day = "23";
            if (day.Equals("24th"))
                day = "24";
            if (day.Equals("25th"))
                day = "25";
            if (day.Equals("26th"))
                day = "26";
            if (day.Equals("27th"))
                day = "27";
            if (day.Equals("28th"))
                day = "28";
            if (day.Equals("29th"))
                day = "29";
            if (day.Equals("30th"))
                day = "30";
            if (day.Equals("31th"))
                day = "31";

            if (month.Equals("january") || month.Equals("January"))
            {
                dayString = "1";
            }
            if (month.Equals("february") || month.Equals("February"))
            {
                dayString = "2";
            }
            if (month.Equals("march") || month.Equals("March"))
            {
                dayString = "3";
            }
            if (month.Equals("april") || month.Equals("April"))
            {
                dayString = "4";
            }
            if (month.Equals("may") || month.Equals("May"))
            {
                dayString = "5";
            }
            if (month.Equals("june") || month.Equals("June"))
            {
                dayString = "6";
            }
            if (month.Equals("july") || month.Equals("July"))
            {
                dayString = "7";
            }
            if (month.Equals("august") || month.Equals("August"))
            {
                dayString = "8";
            }
            if (month.Equals("september") || month.Equals("September"))
            {
                dayString = "9";
            }
            if (month.Equals("october") || month.Equals("October"))
            {
                dayString = "10";
            }
            if (month.Equals("november") || month.Equals("November"))
            {
                dayString = "11";
            }
            if (month.Equals("december") || month.Equals("December"))
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
                t.city = res[0].city;
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



        public async Task<ActionResult> CreateBlind(string str)
        {
            SendData t = new SendData();
            var userId = Session["userId"].ToString();

            var resultIdDoc = db.Users.Where(x => x.userId.Equals(userId)).ToList();
            t.userId = Session["userId"].ToString();
            t.toUserId = resultIdDoc[0].idDoctor;
            t.idDoctor = resultIdDoc[0].idDoctor;
            t.dateInsert = DateTime.Now;
            t.sendData1 = str;
            t.isSign = false;
            db.SendData.Add(t);
            await db.SaveChangesAsync();
            return RedirectToAction("sendMessage");

          
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

        public ActionResult sendMessage()
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