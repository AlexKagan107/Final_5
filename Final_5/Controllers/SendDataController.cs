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
    public class SendDataController : Controller
    {
        private Database1Entities db = new Database1Entities();

        // GET: SendData
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

        public async Task<ActionResult> Chat()
        {

            //string userId = "";
            //if (Session["userId"] != null)
            //{
            //    if (Session["permissions"].Equals("1") || Session["permissions"].Equals("3"))
            //    {
            //        userId = Session["userId"].ToString();
            //        var result = db.SendData.Where(x => x.idDoctor.Equals(userId)).ToList();
            //        return View(result);
            //    }
            //    else if (Session["permissions"].Equals("2"))
            //    {
            //        userId = Session["userId"].ToString();
            //        var result2 = db.SendData.Where(x => x.toUserId.Equals(userId)).ToList();
            //        return View(result2);
            //    }
            //}
            return View();
        }

        // GET: SendData/Details/5
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

        // GET: SendData/Create
        public ActionResult Create()
        {
            ViewBag.userId = new SelectList(db.Users, "userId", "userId");
            return View();
        }

        // POST: SendData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,userId,idDoctor,sendData1,isSign,toUserId")] SendData sendData)
        {
            if (ModelState.IsValid && (!(sendData.sendData1 == null)))
            {
                var resultIdDoc = db.Users.Where(x => x.userId.Equals(sendData.userId)).ToList();
                sendData.userId = Session["userId"].ToString();
                sendData.toUserId = resultIdDoc[0].idDoctor;
                sendData.idDoctor = resultIdDoc[0].idDoctor;
                sendData.dateInsert = DateTime.Now;
                sendData.isSign = false;
                db.SendData.Add(sendData);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.userId = new SelectList(db.Users, "userId", "userId", sendData.userId);
            return View(sendData);
        }

        // GET: SendData/Edit/5
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
            ViewBag.sendDataHis1 = "Message From: " + sendData.userId;
            ViewBag.sendDataHis2 = "The Message: " + sendData.sendData1;
            sendData.sendData1 = "";
            ViewBag.userId = new SelectList(db.Users, "userId", "userId", sendData.userId);
            return View(sendData);
        }

        // POST: SendData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,userId,idDoctor,dateInsert,sendData1,isSign,toUserId")] SendData sendData)
        {
            if (ModelState.IsValid && (!(sendData.sendData1 == null)))
            {
                //db.Entry(sendData).State = EntityState.Modified;
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index");

                string fromUserId = sendData.userId;


                if (Session["permissions"].Equals("1") || Session["permissions"].Equals("3"))
                {
                    SendData s = new SendData();
                    s.userId = Session["userId"].ToString();
                    s.idDoctor = Session["userId"].ToString();
                    s.toUserId = fromUserId;
                    s.dateInsert = DateTime.Now;
                    s.sendData1 = sendData.sendData1;
                    sendData.isSign = false;
                    db.SendData.Add(s);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    SendData s = new SendData();
                    s.userId = Session["userId"].ToString();
                    s.idDoctor = sendData.idDoctor;
                    s.toUserId = fromUserId;
                    s.dateInsert = DateTime.Now;
                    sendData.isSign = false;
                    s.sendData1 = sendData.sendData1;
                    db.SendData.Add(s);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        // GET: SendData/Delete/5
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

        // POST: SendData/Delete/5
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
