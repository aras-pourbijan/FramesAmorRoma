using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FramesAmorRoma.Models;

namespace FramesAmorRoma.Controllers
{
    [Authorize(Users = "admin")]
    public class spotsController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: spots
        public ActionResult Index()
        {
            return View(db.Spots.ToList());
        }

        // GET: spots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // GET: spots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: spots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDspot,locationName,describtion,Locationaddress,spotIMG,spot1img,spot2img,spot3img")] spot spot, HttpPostedFileBase locationIMG, HttpPostedFileBase locationIMG1, HttpPostedFileBase locationIMG2, HttpPostedFileBase locationIMG3)
        {
            if (ModelState.IsValid)
            {
                string filename = locationIMG.FileName;
                string path = Server.MapPath("/content/img/spots/" + filename);
                locationIMG.SaveAs(path);
                spot.spotIMG = filename;

                string filename1 = locationIMG1.FileName;
                string path1 = Server.MapPath("/content/img/spots/" + filename1);
                locationIMG1.SaveAs(path1);
                spot.spot1img = filename1;

                string filename2 = locationIMG2.FileName;
                string path2 = Server.MapPath("/content/img/spots/" + filename2);
                locationIMG2.SaveAs(path2);
                spot.spot2img = filename2;

                string filename3 = locationIMG3.FileName;
                string path3 = Server.MapPath("/content/img/spots/" + filename3);
                locationIMG3.SaveAs(path3);
                spot.spot3img = filename3;



                db.Spots.Add(spot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(spot);
        }

        // GET: spots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: spots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDspot,locationName,describtion,Locationaddress,spotIMG,spot1img,spot2img,spot3img")] spot spot, HttpPostedFileBase spotJPG,
            HttpPostedFileBase IMG1,HttpPostedFileBase IMG2, HttpPostedFileBase IMG3)
        {
            if (ModelState.IsValid)
            {
                spot newSpot = db.Spots.Find(spot.IDspot);
                if (spotJPG != null)
                {
                    string filename = spotJPG.FileName;
                    string path = Server.MapPath("/content/img/spots/" + filename);
                    spotJPG.SaveAs(path);
                    spot.spotIMG= filename;
                }
                else
                {   spot s = db.Spots.Find(spot.IDspot);
                    spot.spotIMG = s.spotIMG;
                }

                if (IMG1 != null)
                {
                    string filename1 = IMG1.FileName;
                    string path1 = Server.MapPath("/content/img/spots/" + filename1);
                    IMG1.SaveAs(path1);
                    spot.spot1img = filename1;
                }
                else
                {
                    spot s1 = db.Spots.Find(spot.IDspot);
                    spot.spot1img = s1.spot1img;
                }

                if (IMG2 != null)
                {
                    string filename2 = IMG2.FileName;
                    string path2 = Server.MapPath("/content/img/spots/" + filename2);
                    IMG2.SaveAs(path2);
                    spot.spot2img = filename2;
                }
                else
                {
                    spot s2 = db.Spots.Find(spot.IDspot);
                    spot.spot2img = s2.spot2img;
                }

                if (IMG3 != null)
                {
                    string filename3 = IMG3.FileName;
                    string path3 = Server.MapPath("/content/img/spots/" + filename3);
                    IMG3.SaveAs(path3);
                    spot.spot3img = filename3;
                }
                else
                {
                    spot s3 = db.Spots.Find(spot.IDspot);
                    spot.spot3img = s3.spot3img;
                }

                newSpot.locationName= spot.locationName;
                newSpot.describtion= spot.describtion;
                newSpot.Locationaddress= spot.Locationaddress;
                newSpot.spotIMG= spot.spotIMG;
                newSpot.spot1img= spot.spot1img;    
                newSpot.spot2img= spot.spot2img;
                newSpot.spot3img = spot.spot3img;


                db.Entry(newSpot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(spot);
        }

        // GET: spots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            spot spot = db.Spots.Find(id);
            if (spot == null)
            {
                return HttpNotFound();
            }
            return View(spot);
        }

        // POST: spots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            spot spot = db.Spots.Find(id);
            db.Spots.Remove(spot);
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
