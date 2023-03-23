using Final_Eve_07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Final_Eve_07.Controllers
{
    [Authorize]
    public class SpeciFicationController : Controller
    {
        PhoneDbContext db= new PhoneDbContext();
        // GET: SpeciFication
        public ActionResult Index()
        {
            return View(db.SpeciFications.ToList());
        }
    }
}