using Final_Eve_07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Eve_07.Controllers
{
    [Authorize]
    public class AdditionalFeatureController : Controller
    {
        PhoneDbContext db = new PhoneDbContext();
        // GET: AdditionalFeature
        public ActionResult Index()
        {
            return View(db.AdditionalFeatures.ToList());
        }
        public ActionResult Create()
        {
            ViewBag.SpeciFication =db.SpeciFications.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(AdditionalFeature E) 
        { 
            if(ModelState.IsValid)
            {
                db.AdditionalFeatures.Add(E);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpeciFication = db.SpeciFications.ToList();
            return View(E);
        }
        public ActionResult Edit(int id)
        {
            var additionalFeature = db.AdditionalFeatures.FirstOrDefault(x => x.AdditionalFeatureId == id);
            ViewBag.SpeciFication = db.SpeciFications.ToList();
            return View(additionalFeature);
        }
        [HttpPost]
        public ActionResult Edit(AdditionalFeature E)
        {
            if (ModelState.IsValid)
            {
                db.Entry(E).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpeciFication = db.SpeciFications.ToList();
            return View(E);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.AdditionalFeatures.FirstOrDefault(x => x.AdditionalFeatureId == id);
            if (existing != null)
            {
                db.AdditionalFeatures.Remove(existing);
                db.SaveChanges();
                return Json(existing.AdditionalFeatureId);
            }
            return Json(null);
        }
    }
}