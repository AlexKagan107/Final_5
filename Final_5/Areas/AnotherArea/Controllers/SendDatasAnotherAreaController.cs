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
    public class SendDatasAnotherAreaController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: AnotherArea/SendDatasAnotherArea
        public async Task<ActionResult> Index()
        {

            string userId = "";
            if (Session["userId"] != null)
            {
                if (Session["permissions"].Equals("1") || Session["permissions"].Equals("3"))
                {
                    userId = Session["userId"].ToString();
                    var result = db.SendData.Where(x => x.idDoctor.Equals(userId)).ToList();
                    return View(result);
                }
                else if (Session["permissions"].Equals("2"))
                {
                    userId = Session["userId"].ToString();
                    var result2 = db.SendData.Where(x => x.toUserId.Equals(userId)).ToList();
                    return View(result2);
                }

            }
            return View();
        }

        // GET: AnotherArea/SendDatasAnotherArea/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendData sendData = await db.SendData.FindAsync(id);
            if (sendData == null)
            {
                return HttpNotFound();
            }
            return View(sendData);
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
            t.isSign = false;
            db.SendData.Add(t);
            await db.SaveChangesAsync();

            return View(t);
        }


        // GET: AnotherArea/SendDatasAnotherArea/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Users, "userId", "password");
            return View();
        }

        // POST: AnotherArea/SendDatasAnotherArea/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,userId,idDoctor,sendData1,isSign,dateInsert,toUserId")] SendData sendData)
        {
            if (ModelState.IsValid)
            {
                db.SendData.Add(sendData);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.userId = new SelectList(db.Users, "userId", "password", sendData.userId);
            return View(sendData);
        }

        // GET: AnotherArea/SendDatasAnotherArea/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendData sendData = await db.SendData.FindAsync(id);
            if (sendData == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.Users, "userId", "password", sendData.userId);
            return View(sendData);
        }

        // POST: AnotherArea/SendDatasAnotherArea/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,userId,idDoctor,sendData1,isSign,dateInsert,toUserId")] SendData sendData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendData).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.userId = new SelectList(db.Users, "userId", "password", sendData.userId);
            return View(sendData);
        }

        // GET: AnotherArea/SendDatasAnotherArea/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SendData sendData = await db.SendData.FindAsync(id);
            if (sendData == null)
            {
                return HttpNotFound();
            }
            return View(sendData);
        }

        // POST: AnotherArea/SendDatasAnotherArea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SendData sendData = await db.SendData.FindAsync(id);
            db.SendData.Remove(sendData);
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
