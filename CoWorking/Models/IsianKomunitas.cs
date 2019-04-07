using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoWorking.Models
{
    public class IsianKomunitas
    {
        [Required(ErrorMessage = " Masukkan nama event")]
        public string NamaKomunitas { get; set; }

        [Required(ErrorMessage = " Masukkan jenis komunitas")]
        public string JenisKomunitas { get; set; }

        [Required(ErrorMessage = " Masukkan kegiatan komunitas")]
        public string Kegiatan { get; set; }

        public string namaFile { get; set; }
        public string pathFile { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }

    }
}