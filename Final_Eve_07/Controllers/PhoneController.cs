using Final_Eve_07.Models;
using Final_Eve_07.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Final_Eve_07.Controllers
{
    [Authorize]
    public class PhoneController : Controller
    {
        PhoneDbContext db=new PhoneDbContext();
        // GET: Phones
        public ActionResult Index()
        {
            return View(db.Phones.Include(x=>x.SpeciFications).ToList());
        }
        //Create//
        public ActionResult Create() 
        {
            ViewBag.PostList = new List<SelectListItem>
            {
                new SelectListItem{ Text="Select", Value="", Selected=true},
                new SelectListItem{Text="Android", Value="Android"},
                new SelectListItem{Text="Stock-Android", Value="Stock-Android"},
                new SelectListItem{Text="Custom-Android", Value="Custom-Android"},
                new SelectListItem{Text="iOS", Value="iOS"},
                new SelectListItem{Text="Windows", Value="Windows"},
                new SelectListItem{Text="Symbian", Value="Symbian"}
            };
            return View();
        }
        [HttpPost]
        public ActionResult Create(PhoneInputModel data)
        {
            if (ModelState.IsValid)
            {
                Phone c = new Phone
                {
                    PhoneName = data.PhoneName,
                    LaunchDate = data.LaunchDate,
                    OperatingSystem = data.OperatingSystem,
                    PhonePrice = data.PhonePrice,
                    Available = data.Available,
                    Picture = data.Picture
                };
                foreach(var q in data.SpeciFications)
                {
                    c.SpeciFications.Add(q);
                }
                db.Phones.Add(c);
                db.SaveChanges();
                return Json(new {id=c.PhoneId});
            }
            return Json(null);
        }
        public PartialViewResult AddSpeciFicationToForm(PhoneInputModel c = null, int? index=null)
        {
            if (c == null) c = new PhoneInputModel();
            if (index.HasValue)
            {
                if (index < c.SpeciFications.Count)
                {
                    c.SpeciFications.RemoveAt(index.Value);
                }
            }
            else 
            {
                c.SpeciFications.Add(new SpeciFication());
            }
            return PartialView("_SpeciFicationForm", c);
        }
        public ActionResult UploadImage(int id, ImageUpload pic)
        {
            if (ModelState.IsValid)
            {
                if (pic.Picture != null)
                {
                    Phone c = db.Phones.First(x => x.PhoneId == id);
                    string ext = Path.GetExtension(pic.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(pic.Picture.FileName) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    pic.Picture.SaveAs(savePath);
                    c.Picture = fileName;
                    db.SaveChanges();
                    return Json(fileName);
                }
            }
            return Json(null);
        }
        //Create Edit//
        public ActionResult Edit (int id)
        {
            ViewBag.PostList = new List<SelectListItem>
            {
                new SelectListItem{ Text="Select", Value="", Selected=true},
                new SelectListItem{Text="Android", Value="Android"},
                new SelectListItem{Text="Stock-Android", Value="Stock-Android"},
                new SelectListItem{Text="Custom-Android", Value="Custom-Android"},
                new SelectListItem{Text="iOS", Value="iOS"},
                new SelectListItem{Text="Windows", Value="Windows"},
                new SelectListItem{Text="Symbian", Value="Symbian"}
            };
            var phone=db.Phones.Include(c=> c.SpeciFications).First(c=>c.PhoneId == id);
            return View(new PhoneEditModel
            {
                PhoneId= phone.PhoneId,
                PhoneName= phone.PhoneName,
                LaunchDate= phone.LaunchDate,
                OperatingSystem= phone.OperatingSystem,
                PhonePrice= phone.PhonePrice,
                Available= phone.Available,
                Picture= phone.Picture,
                SpeciFications=phone.SpeciFications.ToList(),
            });
        }
        [HttpPost]
        public ActionResult Edit(PhoneEditModel model)
        {
            var existing = db.Phones.First(c=>c.PhoneId==model.PhoneId);
            if ( ModelState.IsValid )
            {
                existing.PhoneName= model.PhoneName;
                existing.LaunchDate= model.LaunchDate;
                existing.OperatingSystem= model.OperatingSystem;
                existing.PhonePrice= model.PhonePrice;
                existing.Available= model.Available;
                db.SpeciFications.RemoveRange(existing.SpeciFications.ToList());
                foreach(var q in model.SpeciFications)
                {
                    q.PhoneId= existing.PhoneId;
                    db.SpeciFications.Add(q);
                }
                db.SaveChanges();
                return Json(existing.PhoneId);
            }
            return Json(null);
        }
        public PartialViewResult AddSpeciFicationToEditForm(PhoneEditModel c, int? index = null)
        {
            if (index.HasValue)
            {
                if (index<c.SpeciFications.Count)
                {
                    c.SpeciFications.RemoveAt(index.Value);
                }
            }
            return PartialView("_SpeciFicationEditForm", c);
        }
        public PartialViewResult AddMore(PhoneEditModel c, int? index = null)
        {
            if (c == null) c = new PhoneEditModel();
            if (index.HasValue)
            {
                if (index < c.SpeciFications.Count)
                {
                    c.SpeciFications.RemoveAt(index.Value);
                }
            }
            else
            {
                c.SpeciFications.Add(new SpeciFication());
            }

            return PartialView("_SpeciFicationEditForm", c);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var existing = db.Phones.FirstOrDefault(c=>c.PhoneId== id);
            if (existing != null)
            {
                db.Phones.Remove(existing);
                db.SaveChanges();
                return Json(existing.PhoneId);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}