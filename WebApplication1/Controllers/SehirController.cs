using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using WebApplication1.Models;
using System.Linq.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    public class SehirController : Controller
    {
        Context c = new Context();

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            var draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"].FirstOrDefault();
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            int pageSize = length != null ? length : 0;
            int skip = start != null ? start : 0;
            int recordsTotal = 0;

            List<sehirler> sehirList = c.sehirlers.ToList<sehirler>();
            recordsTotal = sehirList.Count();

            if(!string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(sortDirection))
            {
                sehirList = (List<sehirler>)sehirList.OrderBy(sortColumnName + " " + sortDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                sehirList = sehirList.Where(m => m.SEHIR_AD.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            int recordAfterFiltering = sehirList.Count();

            var data = sehirList.Skip(skip).Take(pageSize).ToList();

            var jsonData = Json(new { draw = draw, recordsFiltered = recordAfterFiltering, recordsTotal = recordsTotal, data = data });
            return jsonData;
        }

        [HttpGet]
        public IActionResult YeniSehir()
        {
            return View();

        }

        [HttpPost]
        public IActionResult YeniSehir(sehirler p)
        {
            c.sehirlers.Add(p);
            c.SaveChanges();
            return RedirectToAction("", "Sehir");
        }

        public IActionResult Sil(int id)
        {
            var aranan = c.sehirlers.Find(id);
            c.sehirlers.Remove(aranan);
            c.SaveChanges();
            return RedirectToAction("", "Sehir");
        }

        public ViewResult Guncelle(int id)
        {
            var degerler = c.sehirlers.Where(m => m.SEHIR_ID == id).FirstOrDefault();
            return View(degerler);
        }

        public IActionResult SehirGuncelle(sehirler p)
        {
            var aranan = c.sehirlers.Where(m => m.SEHIR_ID == p.SEHIR_ID).FirstOrDefault();

            aranan.SEHIR_AD = p.SEHIR_AD;
            aranan.SEHIR_PLAKA_KOD = p.SEHIR_PLAKA_KOD;

            c.SaveChanges();
            return RedirectToAction("", "Sehir");
        }
        public ViewResult Listele(int id)
        {
            var degerler = c.sehirlers.Find(id);
            return View("Listele", degerler);
        }

        public ViewResult SehirDetay(int id)
        {
            var degerler = c.kisilers.Where(m => m.sehir.SEHIR_ID == id).ToList();
            var sehir = c.sehirlers.Where(m => m.SEHIR_ID == id).FirstOrDefault();
            ViewBag.dgr = sehir.SEHIR_AD;
            return View("SehirDetay", degerler);
        }
    }
}
