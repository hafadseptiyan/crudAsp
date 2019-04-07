using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoWorking.Models
{
    public class IsianEvent
    {
        [Required(ErrorMessage = " Masukkan nama event")]
        public string Nama { get; set; }

        [Required(ErrorMessage = " Masukkan jenis event")]
        public string Jenis { get; set; }

        [Required(ErrorMessage = " Masukkan waktu event")]
        public string Waktu { get; set; }
        [Required(ErrorMessage = " Masukkan deskripsi event")]
        public string Deskripsi { get; set; }

        public string namaFile { get; set; }
        public string pathFile { get; set; }

        public HttpPostedFileBase ImageFile { get;set; }
    }
}