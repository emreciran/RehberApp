using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RehberAjaxController : Controller
    {
        Context c = new Context();

        public IActionResult Index()
        {
            List<kisiler> Kisiler = c.kisilers.ToList();
            return View(Kisiler);
        }

        [HttpGet]
        public JsonResult TumRehberiGetir()
        {
            var degerler = c.kisilers.ToList();
            return Json(new { data = degerler });
        }

        [HttpGet]
        public JsonResult RehberiGetir(int id)
        {
            var degerler = c.kisilers.Where(m => m.KISI_ID == id).FirstOrDefault();
            return Json(new { data = degerler });
        }
        [HttpPost]
        public JsonResult KisiEkle(string ad, string soyad, string tel)
        {
            kisiler kisi = new kisiler();
            kisi.KISI_AD = ad;
            kisi.KISI_SOYAD = soyad;
            kisi.KISI_TELEFON = tel;
            c.kisilers.Add(kisi);
            var durum = c.SaveChanges();
            if (durum > 0)
                return Json("1");

            else
                return Json("0");
        }

        [HttpDelete]
        public JsonResult KisiSil(int id)
        {
            var aranan = c.kisilers.Find(id);
            c.kisilers.Remove(aranan);
            var durum = c.SaveChanges();
            if (durum > 0)
                return Json("1");

            else
                return Json("0");
        }

        [HttpPut]
        public JsonResult KisiGuncelle(int id, string ad, string soyad, string tel)
        {
            var aranan = c.kisilers.Where(m => m.KISI_ID == id).FirstOrDefault();
            aranan.KISI_AD = ad;
            aranan.KISI_SOYAD = soyad;
            aranan.KISI_TELEFON = tel;
            var durum = c.SaveChanges();
            if (durum > 0)
                return Json("1");

            else
                return Json("0");
        }
    }
}
