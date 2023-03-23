using Final_Eve_07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Eve_07.Controllers
{
    [Authorize]
    public class PlatformController : Controller
    {
        PhoneDbContext db= new PhoneDbContext();
        // GET: Platform
        public ActionResult Index()
        {
            return View(db.Platforms.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.AdditionalFeature = db.AdditionalFeatures.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Platform p)
        {
            if(ModelState.IsValid)
            {
                db.Platforms.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdditionalFeature = db.AdditionalFeatures.ToList();
            return View(p);
          
        }
        public ActionResult Edit(int id)
        {
            var Platforms = db.Platforms.FirstOrDefault(x=>x.PlatformId == id);
            ViewBag.AdditionalFeature = db.AdditionalFeatures.ToList();
            return View(Platforms);
        }
        [HttpPost]
        public ActionResult Edit (Platform p)
        {
            if(ModelState.IsValid)
            {
                db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdditionalFeature= db.AdditionalFeatures.ToList();
            return View(p);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var p = db.Platforms.FirstOrDefault(x => x.PlatformId == id);
            if(p != null)
            {
                db.Platforms.Remove(p);
                db.SaveChanges();
                return Json(p.PlatformId);
            }
            return Json(null);
        }
    }
}