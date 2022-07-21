using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using WebApplication1.Models;
using System.Linq.Dynamic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace WebApplication1.Controllers
{
    public class RehberController : Controller
    {
        Context c = new Context();
        public IActionResult Index()
        {
            var degerler = c.kisilers.Include(x => x.sehir).ToList();
            return View(degerler);
        }


        [HttpPost]
        public JsonResult GetList()// int start
        {
            // Server side parameter
            var draw = Request.Form["draw"];
            int start = Convert.ToInt32(Request.Form["start"]);
            int length = Convert.ToInt32(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"].FirstOrDefault();
            string sortColumnName = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            string sortDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            List<kisiler> kisiList = c.kisilers.Include(m => m.sehir).ToList();
            kisiList = new List<kisiler>();

            kisiList = c.kisilers.ToList<kisiler>();
            recordsTotal = kisiList.Count();
            if (!string.IsNullOrEmpty(sortColumnName) && string.IsNullOrEmpty(sortDirection))
            {
                kisiList = (List<kisiler>)kisiList.OrderBy(sortColumnName + " " + sortDirection);
            }
            if (!string.IsNullOrEmpty(searchValue))
            {
                kisiList = kisiList.Where(m => m.KISI_AD.ToLower().Contains(searchValue.ToLower())
                                                || m.KISI_SOYAD.ToLower().Contains(searchValue.ToLower())
                                                || m.KISI_TELEFON.Contains(searchValue)).ToList();
            }
            int recordAfterFiltering = kisiList.Count();
            
            var data = kisiList.Skip(skip).Take(pageSize).ToList();
            var jsonData = Json(new { draw = draw, recordsFiltered = recordAfterFiltering, recordsTotal = recordsTotal, data = data });
            return jsonData;
            
        }


        [HttpGet]
        public JsonResult GetSelect(string searchTerm, int page)
        {
            var sehirList = c.sehirlers.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
                sehirList = c.sehirlers.Where(x => x.SEHIR_AD.ToLower().Contains(searchTerm.ToLower())).ToList();

            var modifiedData = sehirList.Select(x => new { SEHIR_ID = x.SEHIR_ID, SEHIR_AD = x.SEHIR_AD }).ToList();

            var data = modifiedData.Skip(0).Take(page).ToList();

            return Json(new { searchTerm = searchTerm, data = data });
        }

        [HttpGet]
        public IActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EkleAsync(kisiekle p)
        {
            kisiler kisi = new kisiler();

            if(p.KISI_FOTO != null)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                var imagename = Path.GetFileName(p.KISI_FOTO.FileName);
                var extension = Path.GetExtension(p.KISI_FOTO.FileName);
                if (!supportedTypes.Contains(extension))
                {
                    string error = "Kabul Edilen Dosya Türleri / .jpg, .jpeg, .png";
                    ViewBag.error = error;
                    return View("Ekle");
                }
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", imagename);
                using(var fileStream = new FileStream(location, FileMode.Create))
                {
                    await p.KISI_FOTO.CopyToAsync(fileStream);
                }
                kisi.KISI_FOTO = imagename;
            }

            if (!ModelState.IsValid)
            {
                return View("Ekle");
            }

            var sehir = c.sehirlers.Where(x => x.SEHIR_ID == p.sehir.SEHIR_ID).FirstOrDefault();
            kisi.sehir = sehir;

            kisi.KISI_AD = p.KISI_AD;
            kisi.KISI_SOYAD = p.KISI_SOYAD;
            kisi.KISI_TELEFON = p.KISI_TELEFON;

            c.kisilers.Add(kisi);
            c.SaveChanges();

            return RedirectToAction("", "Rehber");
        }

        public IActionResult Sil(int id)
        {
            var aranan = c.kisilers.Find(id);
            c.kisilers.Remove(aranan);
            c.SaveChanges();
            return RedirectToAction("", "Rehber");
        }

        public IActionResult Guncelle(int id)
        {
            var degerler = c.kisilers.Include(m => m.sehir).ToList();
            var veriler = degerler.Where(m => m.KISI_ID == id).FirstOrDefault();
            return View("Guncelle", veriler);
        }

        public async Task<IActionResult> RehberGuncelle(kisiekle p, kisiler k)
        {
            kisiler kisi = new kisiler();

            if (!ModelState.IsValid)
            {
                return View("Guncelle");
            }

            if (p.KISI_FOTO != null)
            {
                var supportedTypes = new[] { ".jpg", ".jpeg", ".png" };
                var imagename = Path.GetFileName(p.KISI_FOTO.FileName);
                var extension = Path.GetExtension(p.KISI_FOTO.FileName);
                if (!supportedTypes.Contains(extension))
                {
                    string error = "Kabul Edilen Dosya Türleri / .jpg, .jpeg, .png";
                    ViewBag.error = error;
                    return View("Ekle");
                }
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images/", imagename);
                using (var fileStream = new FileStream(location, FileMode.Create))
                {
                    await p.KISI_FOTO.CopyToAsync(fileStream);
                }
                kisi.KISI_FOTO = imagename;
            }

            var aranan = c.kisilers.Where(m => m.KISI_ID == k.KISI_ID).FirstOrDefault();
            aranan.KISI_AD = p.KISI_AD;
            aranan.KISI_SOYAD = p.KISI_SOYAD;
            aranan.KISI_TELEFON = p.KISI_TELEFON;

            if (p.KISI_FOTO == null)
            {
                aranan.KISI_FOTO = aranan.KISI_FOTO;
            }

            var sehir = c.sehirlers.Where(m => m.SEHIR_ID == p.sehir.SEHIR_ID).FirstOrDefault();
            p.sehir = sehir;
            aranan.sehir = p.sehir;

            if (!ModelState.IsValid)
            {
                return View();
            }

            c.SaveChanges();
            return RedirectToAction("", "Rehber");
        }

        //public JsonResult RehberGuncelle(int id, string ad, string soyad, string telefon)
        //{
        //    var aranan = c.kisilers.Where(m => m.KISI_ID == id).FirstOrDefault();
        //    aranan.KISI_AD = ad;
        //    aranan.KISI_SOYAD = soyad;
        //    aranan.KISI_TELEFON = telefon;

        //    c.SaveChanges();
        //    return Json(new { redirectToUrl = Url.Action("", "Rehber") });
        //}

        public IActionResult Listele(int id)
        {
            var degerler = c.kisilers.Include(m => m.sehir).ToList();
            var veriler = degerler.Where(m => m.KISI_ID == id).FirstOrDefault();
            return View("Listele", veriler);
        }

        // View
        public ViewResult viewresult()
        {
            return View();
        }

        // PartialView 
        public PartialViewResult partialview()
        {
            return PartialView();
        }

        // Json formatında veri döndürmek için
        public JsonResult liste()
        {
            return Json(new
            {
                ad = "emre",
                yas = 21
            });
        }

        // boş değer döndürmek için
        public EmptyResult empty()
        {
            return new EmptyResult();
        }

        // ContentResult string değer dönmek için
        public ContentResult content()
        {
            return Content("string değer");
        }


        // Geriye döndürülecek action türleri değişkenlik gösterildiği durumlarda
        public ActionResult d()
        {
            if (true)
            {
                // ...
                return Json(new object());
            }
            else if (true)
            {
                // ...
                return Content("asd");
            }

            return View();
        }
    }
}
