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
    public class portfoliosController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: portfolios
        public ActionResult Index()
        {
            var portfolios = db.portfolios.Include(p => p.User);
            return View(portfolios.ToList());
        }

        // GET: portfolios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            portfolio portfolio = db.portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // GET: portfolios/Create
        public ActionResult Create()
        {
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName");
            return View();
        }

        // POST: portfolios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDportfolio,IDuser,coverIMG,img1,img2,img3,img4,img5,img6,img7,img8,img9")] portfolio portfolio,
            HttpPostedFileBase Cover, HttpPostedFileBase IMG1, HttpPostedFileBase IMG2, HttpPostedFileBase IMG3, HttpPostedFileBase IMG4, HttpPostedFileBase IMG5,
            HttpPostedFileBase IMG6, HttpPostedFileBase IMG7, HttpPostedFileBase IMG8, HttpPostedFileBase IMG9)
        {
            if (ModelState.IsValid)
            {
                string filenameCover = Cover.FileName;
                string pathcover = Server.MapPath("/content/img/portfoglio/"+portfolio.IDuser+"/" + filenameCover);
                Cover.SaveAs(pathcover);
                portfolio.coverIMG=filenameCover;

                string filename1 = IMG1.FileName;
                string path1 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename1);
                Cover.SaveAs(path1);
                portfolio.img1 = filename1;

                string filename2 = IMG2.FileName;
                string path2 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename2);
                Cover.SaveAs(path2);
                portfolio.img2 = filename2;

                string filename3 = IMG3.FileName;
                string path3 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename3);
                Cover.SaveAs(path3);
                portfolio.img3 = filename3;

                string filename4 = IMG4.FileName;
                string path4 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename4);
                Cover.SaveAs(path4);
                portfolio.img4 = filename4;

                string filename5 = IMG5.FileName;
                string path5 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename5);
                Cover.SaveAs(path5);
                portfolio.img5 = filename5;

                string filename6 = IMG6.FileName;
                string path6 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename6);
                Cover.SaveAs(path6);
                portfolio.img6 = filename6;

                string filename7 = IMG7.FileName;
                string path7 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename7);
                Cover.SaveAs(path7);
                portfolio.img7 = filename7;

                string filename8 = IMG8.FileName;
                string path8 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename8);
                Cover.SaveAs(path8);
                portfolio.img8 = filename8;

                string filename9 = IMG9.FileName;
                string path9 = Server.MapPath("/content/img/portfoglio/" + portfolio.IDuser + "/" + filename9);
                Cover.SaveAs(path9);
                portfolio.img9 = filename9;



                db.portfolios.Add(portfolio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", portfolio.IDuser);
            return View(portfolio);
        }

        // GET: portfolios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            portfolio portfolio = db.portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", portfolio.IDuser);
            return View(portfolio);
        }

        // POST: portfolios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDportfolio,IDuser,coverIMG,img1,img2,img3,img4,img5,img6,img7,img8,img9")] portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(portfolio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDuser = new SelectList(db.Users, "IDuser", "UserName", portfolio.IDuser);
            return View(portfolio);
        }

        // GET: portfolios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            portfolio portfolio = db.portfolios.Find(id);
            if (portfolio == null)
            {
                return HttpNotFound();
            }
            return View(portfolio);
        }

        // POST: portfolios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            portfolio portfolio = db.portfolios.Find(id);
            db.portfolios.Remove(portfolio);
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
