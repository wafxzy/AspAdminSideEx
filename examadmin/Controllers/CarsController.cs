using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace examadmin.Controllers
{
    public class CarsController : Controller
    {
        service1Entities db = new service1Entities();
        // GET: Cars
        public ActionResult carlist()
        {
            return View(db.carinfos.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(carinfo imageModel)
        {
            if (ModelState.IsValid)
            {
  string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + extension;
           imageModel.imgpath = "~/IMG/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/IMG/"), fileName);
            imageModel.ImageFile.SaveAs(fileName); 
                db.carinfos.Add(imageModel);
                db.SaveChanges();
                return RedirectToAction("carlist");
            }
                 return View(imageModel);  
           
            
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            carinfo car = db.carinfos.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            carinfo student = db.carinfos.Find(id);

        
            // или
            /*if (student.StudentCard != null)
            {
                db.StudentCards.Remove(student.StudentCard);
            }*/

            db.carinfos.Remove(student);
            db.SaveChanges();
            return RedirectToAction("carlist");
        }

        public ActionResult Edit(int id=0)
        {
            carinfo car1 = db.carinfos.Find(id);
            if (car1==null)
            {
                return HttpNotFound();
            }
            return View(car1);
        }

        [HttpPost]
        public ActionResult Edit(carinfo car1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(car1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("carlist");
            }
            return View(car1);
        }
    }
}