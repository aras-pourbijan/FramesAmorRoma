using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using FramesAmorRoma.Models;

namespace FramesAmorRoma.Controllers
{
    public class bookingsController : Controller
        
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: bookings
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            var bookings = db.bookings.Include(b => b.package).Include(b => b.spot).Include(b => b.User);
            return View(bookings.ToList());
        }

        // GET: bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: bookings/Create
        public ActionResult Create()
        {
            ViewBag.IDpackage = new SelectList(db.packages, "IDpackage", "PackageName");
            ViewBag.IDspot = new SelectList(db.spots, "IDspot", "locationName");
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName");
            return View();
        }

        // POST: bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDbook,IDuser,IDspot,daterequest,prefertHour,IDpackage,clientName,clientEmail,clientTel,NumOfPersons")] booking booking)
        {
            if (ModelState.IsValid)
            {
                booking.bookTime= DateTime.Now.Date; 

                db.bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDpackage = new SelectList(db.packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.spots, "IDspot", "locationName", booking.IDspot);
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", booking.IDuser);
            return View(booking);
        }



        public ActionResult CreateByID(int ID)
        {
            ViewBag.IDpackage = new SelectList(db.packages, "IDpackage", "PackageName");
            ViewBag.IDspot = new SelectList(db.spots, "IDspot", "locationName");
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName");
            return View();
        }

        [HttpPost]

        public ActionResult CreateByID([Bind(Include = "IDbook,IDuser,IDspot,daterequest,prefertHour,IDpackage,clientName,clientEmail,clientTel,NumOfPersons")] booking booking,int ID)
        {
            if (ModelState.IsValid)
            {

                booking.IDuser = ID;
                booking.bookTime = DateTime.Now.Date;

                db.bookings.Add(booking);
                db.SaveChanges();

                MailAddress Sender = new MailAddress(booking.clientEmail);
                MailAddress Reciver = new MailAddress("framesamor@virgilio.it");

                MailMessage mailMessage = new MailMessage();
                mailMessage.Subject = "your request for photoshoot with Frames Amor ";
                mailMessage.Body = "hrhrhrhrh";
                mailMessage.From = Reciver;
                mailMessage.To.Add(Reciver);

                SmtpClient client= new SmtpClient("out.virgilio.it");
                client.Host = "out.virgilio.it";
                client.Port = 465;
                
                client.Credentials = new NetworkCredential("framesamor@virgilio.it", "1234Admin@");
                client.Send(mailMessage);

                return RedirectToAction("BookDone");
            }

            ViewBag.IDpackage = new SelectList(db.packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.spots, "IDspot", "locationName", booking.IDspot);
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", booking.IDuser);
            return View(booking);
        }


        
        public ActionResult BookDone()
        {
            
            return View();
        }


        [Authorize(Users = "admin")]
        // GET: bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDpackage = new SelectList(db.packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.spots, "IDspot", "locationName", booking.IDspot);
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", booking.IDuser);
            return View(booking);
        }

        // POST: bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin")]
        public ActionResult Edit([Bind(Include = "IDbook,IDuser,IDspot,bookTime,daterequest,prefertHour,IDpackage,clientName,clientEmail,clientTel,NumOfPersons")] booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDpackage = new SelectList(db.packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.spots, "IDspot", "locationName", booking.IDspot);
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", booking.IDuser);
            return View(booking);
        }

        // GET: bookings/Delete/5
        [Authorize(Users = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Users = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            booking booking = db.bookings.Find(id);
            db.bookings.Remove(booking);
            db.SaveChanges();
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
