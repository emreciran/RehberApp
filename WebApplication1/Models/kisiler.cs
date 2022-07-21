﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication1.Models
{
    public class kisiler
    {
        [Key]
        public int KISI_ID { get; set; }
        public string KISI_AD { get; set; }
        public string KISI_SOYAD { get; set; }

        [Required(ErrorMessage = "Telefon numarası alanı zorunludur.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Geçersiz telefon numarası")]
        public string KISI_TELEFON { get; set; }

        public string KISI_FOTO { get; set; }

        public sehirler sehir { get; set; }
    }
}
