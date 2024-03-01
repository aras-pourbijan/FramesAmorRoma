using FramesAmorRoma.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
namespace FramesAmorRoma.Controllers
{
    public class BookingsController : Controller

    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: bookings
        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            var bookings = db.Bookings.Include(b => b.package).Include(b => b.spot).Include(b => b.User);
            return View(bookings.OrderByDescending(x => x.IDbook).ToList());
        }

        // GET: bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: bookings/Create
        public ActionResult Create()
        {
            ViewBag.IDpackage = new SelectList(db.Packages, "IDpackage", "PackageName");
            ViewBag.IDspot = new SelectList(db.Spots, "IDspot", "locationName");
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
                booking.bookTime = DateTime.Now;

                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDpackage = new SelectList(db.Packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.Spots, "IDspot", "locationName", booking.IDspot);
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", booking.IDuser);
            return View(booking);
        }



        public ActionResult CreateByID(int ID)
        {
            ViewBag.IDpackage = new SelectList(db.Packages, "IDpackage", "PackageName");
            ViewBag.IDspot = new SelectList(db.Spots, "IDspot", "locationName");
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName");
            return View();
        }

        [HttpPost]

        public ActionResult CreateByID(booking booking, int ID)
        {
            if (ModelState.IsValid)
            {

                booking.IDuser = ID;
                booking.bookTime = DateTime.Now.Date;

                db.Bookings.Add(booking);
                var User = db.Users.Find(ID);
                var spot = db.Spots.Find(booking.IDspot);
                var package = db.Packages.Find(booking.IDpackage);
                db.SaveChanges();

                MailAddress customer = new MailAddress(booking.clientEmail);
                MailAddress AdminMail = new MailAddress("framesamor@virgilio.it");
                MailAddress Photographer = new MailAddress(User.email);


                MailMessage mailMessage = new MailMessage();
                mailMessage.Subject = "Your request for photoshoot with Photographer " + User.FirstName + " from Amor Frames";
                mailMessage.Body = "Thank You for being our esteemed customer. Your support and trust in us are much cherished. \nThank You once again! \n---------------------------------------------\n" +
                    "---------------------------------------------\n" + "Your Appointment With: " + User.FirstName + " " + User.LastName + "\nTel: " + User.tel +
                    "\n---------------------------------------------\nBooking number: " + booking.IDbook + "\nBooking date: " + booking.daterequest.ToShortDateString() + "\nLocation: " + spot.locationName + " - " + spot.Locationaddress + "\nPrefert Hour: " +
                    booking.prefertHour.ToShortTimeString() + "\nPackage: " + package.PackageName + " - " + ((int)package.price) + "" + "€ - included images: " + package.PicsIncluded +
                    "\nCustomer Name: " + booking.clientName +
                    "\nCustomer Mail: " + booking.clientEmail +
                    "\nCustomer contact number: " + booking.clientTel +
                    "\n" + booking.NumOfPersons + " Perosn(s)" +
                    "---------------------------------------------\n" +
                    "You Will Be Contacted Shortly by Photographer";
                mailMessage.From = AdminMail;
                mailMessage.To.Add(AdminMail);
                mailMessage.To.Add(customer);
                mailMessage.To.Add(Photographer);

                SmtpClient client = new SmtpClient("out.virgilio.it");
                client.Host = "out.virgilio.it";
                client.Port = 587;

                client.Credentials = new NetworkCredential("framesamor@virgilio.it", "1234Admin@");
                client.Send(mailMessage);

                return RedirectToAction("BookDone");
            }

            ViewBag.IDpackage = new SelectList(db.Packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.Spots, "IDspot", "locationName", booking.IDspot);
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
            booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDpackage = new SelectList(db.Packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.Spots, "IDspot", "locationName", booking.IDspot);
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
            ViewBag.IDpackage = new SelectList(db.Packages, "IDpackage", "PackageName", booking.IDpackage);
            ViewBag.IDspot = new SelectList(db.Spots, "IDspot", "locationName", booking.IDspot);
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
            booking booking = db.Bookings.Find(id);
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
            booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
