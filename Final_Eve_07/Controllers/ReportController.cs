using Final_Eve_07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Final_Eve_07.ViewModels;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Final_Eve_07.Controllers
{
    public class ReportController : Controller
    {
        PhoneDbContext db = new PhoneDbContext();
        // GET: Report
        public ActionResult Index()
        {
            return View(db.Phones.ToList());
        }
        public ActionResult PhoneReport()
        {
            var data = db.Phones.Include(x => x.SpeciFications).Select(x => new PhoneReportViewModel
            {
                PhoneName = x.PhoneName,
                PhoneId = x.PhoneId,
                LaunchDate = x.LaunchDate,
                PhonePrice = x.PhonePrice,
                Available = x.Available,
                OperatingSystem= x.OperatingSystem,
            }).ToList();
            return View(data);
        }
        public ActionResult PhonePDF()
        {
            var data = db.Phones.AsEnumerable().Select(x => new PhoneReportViewModel
            {
                PhoneName = x.PhoneName,
                PhoneId = x.PhoneId,
                LaunchDate = x.LaunchDate,
                PhonePrice = x.PhonePrice,
                Available = x.Available,
                OperatingSystem = x.OperatingSystem,
                Picture = x.Picture,
                Image = System.IO.File.ReadAllBytes(System.IO.Path.Combine(Server.MapPath("~/Pictures"), x.Picture))
            }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport1.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            rd.PrintOptions.PaperOrientation =
                CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "Phone.pdf");
        }
        //
        public ActionResult PhoneExcel()
        {
            var data = db.Phones.AsEnumerable().Select(x => new PhoneReportViewModel
            {
                PhoneName = x.PhoneName,
                PhoneId = x.PhoneId,
                LaunchDate = x.LaunchDate,
                PhonePrice = x.PhonePrice,
                Available = x.Available,
                OperatingSystem = x.OperatingSystem,
                Picture = x.Picture,
                Image = System.IO.File.ReadAllBytes(System.IO.Path.Combine(Server.MapPath("~/Pictures"), x.Picture))
            }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport1.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            rd.PrintOptions.PaperOrientation =
                CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/vnd.ms-excel", "Phone.xls");
        }
        //
        public ActionResult SpeciFicationReports()
        {
            var data = db.SpeciFications.Include(x => x.Phone).Select(x => new SpeciFicationViewModel
            {
                CompanyName = x.CompanyName,
                SpeciFicationId = x.SpeciFicationId,
                Display = x.Display,
                Camera= x.Camera,
                RAM= x.RAM,
                PhoneId= x.PhoneId,
                PhoneName = x.Phone.PhoneName
                
            }).ToList();
            return View(data);
        }
        public ActionResult SpeciFicationPDF()
        {
            var data = db.SpeciFications.Include(x=>x.Phone).Select(x => new SpeciFicationViewModel
            {
                CompanyName = x.CompanyName,
                SpeciFicationId = x.SpeciFicationId,
                Display = x.Display,
                Camera = x.Camera,
                RAM = x.RAM,
                PhoneId = x.PhoneId,
                PhoneName = x.Phone.PhoneName
            }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport2.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            rd.PrintOptions.PaperOrientation =
                CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            return File(stream, "application/pdf", "SpeciFication.pdf");
        }

        public ActionResult SpeciFicationExcel()
        {
            var data = db.SpeciFications.Include(x => x.Phone).Select(x => new SpeciFicationViewModel
            {
                CompanyName = x.CompanyName,
                SpeciFicationId = x.SpeciFicationId,
                Display = x.Display,
                Camera = x.Camera,
                RAM = x.RAM,
                PhoneId = x.PhoneId,
                PhoneName = x.Phone.PhoneName
            }).ToList();
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Reports"), "CrystalReport2.rpt"));
            rd.SetDataSource(data);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            rd.PrintOptions.PaperOrientation =
                CrystalDecisions.Shared.PaperOrientation.Landscape;
            rd.PrintOptions.ApplyPageMargins(new CrystalDecisions.Shared.PageMargins(5, 5, 5, 5));
            rd.PrintOptions.PaperSize = CrystalDecisions.Shared.PaperSize.PaperA4;

            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.Excel);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/vnd.ms-excel", "SpeciFication.xls");
        }
    }
}
   