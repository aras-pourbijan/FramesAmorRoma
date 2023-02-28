using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FramesAmorRoma.Models;


namespace FramesAmorRoma.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: Users
        [AllowAnonymous]
        public ActionResult login()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult login(User U)
        {
            if (ModelState.IsValid && db.Users.Where(x => x.UserName == U.UserName && x.psw == U.psw && x.UserName != "admin").Count() == 1)
            {
                FormsAuthentication.SetAuthCookie(U.UserName, true);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            else if(ModelState.IsValid && db.Users.Where(x => x.UserName == "admin" && x.psw == U.psw).Count() == 1)
            {
                FormsAuthentication.SetAuthCookie(U.UserName, true);
                return Redirect("Managment");
            }
            else
            {
                ViewBag.loginerr = "Try again! username or password is incorrect!";
            }

            return View(U);
        } 

        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("index","Home");
        }
        public ActionResult Managment()
        {

            return View();
        }

        public ActionResult UserHomePage()
        {
            ModelDBcontext db = new ModelDBcontext();
            User ThisUser=db.Users.Where(u=>u.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.UserAuthIMG = ThisUser.imgURL;
            ViewBag.UserName = ThisUser.FirstName;
            ViewBag.IDuser = ThisUser.IDuser;

            portfolio Thisportfolio = db.portfolios.Where(p=>p.User.UserName == User.Identity.Name).FirstOrDefault();

            ViewBag.IDportfolio = Thisportfolio.IDportfolio;

            
            return View();
        }


        [Authorize(Users = "admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        [Authorize(Users = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin")]
        public ActionResult Create([Bind(Include = "UserName,email,psw,tel")] User user)
        {
            if (ModelState.IsValid)
            {   
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,IDuser,email,psw,FirstName,LastName,imgURL,Website,instagram,tel,experience,aboutME")] User user, HttpPostedFileBase profileIMG)
        {
            if (ModelState.IsValid)
            {
                User UserNew = db.Users.Find(user.IDuser);

                if (profileIMG != null) { 
                    string filename = profileIMG.FileName;
                    string path = Server.MapPath("/content/img/profiles/"+ filename);
                    profileIMG.SaveAs(path);
                    user.imgURL= filename;
                }
                else
                {
                    User u = db.Users.Find(user.IDuser);
                    user.imgURL = u.imgURL;
                }

                UserNew.imgURL = user.imgURL;
                UserNew.UserName= user.UserName;
                UserNew.email= user.email;
                UserNew.psw = user.psw; 
                UserNew.FirstName=user.FirstName;
                UserNew.LastName=user.LastName;
                UserNew.Website= user.Website;  
                UserNew.instagram= user.instagram;
                UserNew.tel= user.tel;
                UserNew.experience= user.experience;
                UserNew.aboutME= user.aboutME;


                
               

                db.Entry(UserNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [Authorize(Users = "admin")]

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
