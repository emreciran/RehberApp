using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class sehirler
    {
        [Key]
        public int SEHIR_ID { get; set; }
        public string SEHIR_AD { get; set; }

        public int SEHIR_PLAKA_KOD { get; set; }

    }
}
