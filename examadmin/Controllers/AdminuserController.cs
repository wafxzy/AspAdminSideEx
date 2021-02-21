using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace examadmin.Controllers
{
    public class AdminuserController : Controller
    {
       service1Entities db = new service1Entities();
        // GET: Adminuser
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult userlist()
        {
            return View(db.usses.ToList());
        }
        public   ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            uss usser = db.usses.Find(id);
            if (usser == null)
            {
                return HttpNotFound();
            }
            return View(usser);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            uss USSE = db.usses.Find(id);


            // или
            /*if (student.StudentCard != null)
            {
                db.StudentCards.Remove(student.StudentCard);
            }*/

            db.usses.Remove(USSE);
            db.SaveChanges();
            return RedirectToAction("userlist");
        }
        public ActionResult Edit(int id = 0)
        {
            uss user = db.usses.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(uss us)
        {
            if (ModelState.IsValid)
            {
                db.Entry(us).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("userlist");
            }
            return View(us);
        }
    }
}