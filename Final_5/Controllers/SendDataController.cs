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
                userId = Session["userId"].ToString();
                var result = db.SendData.Where(x => x.idDoctor.Equals(userId)).ToList();
                return View(result);

            }
            else
            {
                return View();
            }
         
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
            ViewBag.userId = new SelectList(db.Users, "userId", "password");
            return View();
        }

        // POST: SendData/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,userId,idDoctor,sendData1,isSign")] SendData sendData)
        {
            if (ModelState.IsValid)
            {
                var resultIdDoc = db.Users.Where(x => x.userId.Equals(sendData.userId)).ToList();
                sendData.userId = Session["userId"].ToString();
                sendData.idDoctor = resultIdDoc[0].idDoctor;
                sendData.dateInsert = DateTime.Now;
                sendData.isSign = false;
                db.SendData.Add(sendData);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.userId = new SelectList(db.Users, "userId", "password", sendData.userId);
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
            ViewBag.userId = new SelectList(db.Users, "userId", "password", sendData.userId);
            return View(sendData);
        }

        // POST: SendData/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,userId,idDoctor,dateInsert,sendData1,isSign")] SendData sendData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sendData).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
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
