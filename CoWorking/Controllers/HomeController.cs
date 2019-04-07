using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoWorking.Models;
using System.Web.Security;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace CoWorking.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ViewResult index() {

            DataEventEntities db = new DataEventEntities();
            var acara = (from c in db.Event
                         orderby c.Id_event descending
                         select c).Take(2);
            return View(acara);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                using (DataEventEntities db = new DataEventEntities())
                {
                    var obj = db.Admin.Where(a => a.username.Equals(admin.username) &&
                        a.password.Equals(admin.password)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["AdminID"] = obj.id_admin.ToString();
                        Session["Username"] = obj.username.ToString();
                        return RedirectToAction("Dashboard");
                    }

                }
            }
            return View(admin);
        }

        public ActionResult DataEvent()
        {
            DataEventEntities db = new DataEventEntities();
            List<Event> p = db.Event.ToList();
            return View("DataEvent", p);   

        }


        public ActionResult DataKomunitas()
        {
            DataKomunitasEntities db = new DataKomunitasEntities();
            List<Komunitas> p = db.Komunitas.ToList();
            return View("DataKomunitas", p);
        }

        [HttpPost]
        public ActionResult FormAcara(IsianEvent isian)
        {
            if (ModelState.IsValid)
            {
                DataEventEntities Data = new DataEventEntities();
                Event param = new Event();

                /* upload file */
                var file = isian.ImageFile;
                byte[] imagebyte = null;
                file.SaveAs(Server.MapPath("~/Upload/" + file.FileName));
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);
                param.namaFile = file.FileName;
                param.pathFile = "~/Upload/" + file.FileName;
                param.byteFIle = imagebyte;

                /*insert table */
                param.nama_event  = isian.Nama ;
                param.jenis_event = isian.Jenis;
                param.waktu_event = isian.Waktu;
                param.deskripsi   = isian.Deskripsi;

                Data.Event.Add(param);
                Data.SaveChanges();
                return View("Sukses", isian);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ViewResult FormAcara()
        {
            return View();
        }

        public ViewResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FormKomunitas(IsianKomunitas isian)
        {
            if (ModelState.IsValid)
            {
                DataKomunitasEntities Data = new DataKomunitasEntities();
                Komunitas kom = new Komunitas();

                /* upload file */
                var file = isian.ImageFile;
                byte[] imagebyte = null;
                file.SaveAs(Server.MapPath("~/Upload/" + file.FileName));
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);
                kom.namaFile = file.FileName;
                kom.pathFile = "~/Upload/" + file.FileName;
                kom.byteFile = imagebyte;

                /*insert table */
                kom.nama_komunitas = isian.NamaKomunitas;
                kom.jenis_komunitas = isian.JenisKomunitas;
                kom.kegiatan = isian.Kegiatan;
             

                Data.Komunitas.Add(kom);
                Data.SaveChanges();
                return View("SuksesKomunitas", isian);
            }
            else
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            DataEventEntities db = new DataEventEntities();
            var idEvent = (from item in db.Event where item.Id_event == id select item).First();
            return View("Edit", idEvent);
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event Event)
        {
            DataEventEntities db = new DataEventEntities();
            var idEvent = (from item in db.Event where item.Id_event == Event.Id_event select item).First();
            idEvent.nama_event     = Event.nama_event;
            idEvent.jenis_event    = Event.jenis_event;
            idEvent.waktu_event    = Event.waktu_event;
            idEvent.deskripsi      = Event.deskripsi;
            db.SaveChanges();
            var list = db.Event.ToList();
            return View("DataEvent", list);

        }

        public ActionResult Delete(int id)
        {
            DataEventEntities db = new DataEventEntities();
            var idEvent = (from item in db.Event where item.Id_event == id select item).First();
            db.Event.Remove(idEvent);
            db.SaveChanges();
            var list = db.Event.ToList();
            return View("DataEvent", list);
        }

        public ActionResult EditKomunitas(int id)
        {
            DataKomunitasEntities db = new DataKomunitasEntities();
            var idKomunitas = (from item in db.Komunitas where item.Id_komunitas == id select item).First();
            return View("EditKomunitas", idKomunitas);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditKomunitas(Komunitas Komunitas)
        {
            DataKomunitasEntities db = new DataKomunitasEntities();
            var idKomunitas = (from item in db.Komunitas where item.Id_komunitas == Komunitas.Id_komunitas select item).First();
            idKomunitas.nama_komunitas  = Komunitas.nama_komunitas;
            idKomunitas.jenis_komunitas = Komunitas.jenis_komunitas;
            idKomunitas.kegiatan        = Komunitas.kegiatan;
            db.SaveChanges();
            var list = db.Komunitas.ToList();
            return View("DataKomunitas", list);

        }

        public ActionResult DeleteKomunitas(int id)
        {
            DataKomunitasEntities db = new DataKomunitasEntities();
            var idKomunitas = (from item in db.Komunitas where item.Id_komunitas == id select item).First();
            db.Komunitas.Remove(idKomunitas);
            db.SaveChanges();
            var list = db.Komunitas.ToList();
            return View("DataKomunitas", list);
        }

        public ViewResult FormKomunitas()
        {
            return View();
        }

        public ViewResult Sukses()
        {
            return View();
        }

        public ViewResult SuksesKomunitas()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ViewResult Program()
        {
            DataKomunitasEntities db = new DataKomunitasEntities();
            var acara = from c in db.Komunitas
                        where c.jenis_komunitas =="Web Programming"
                         orderby c.Id_komunitas ascending
                         select c;
            return View(acara);
        }

        public ViewResult Design()        
        {
            DataKomunitasEntities db = new DataKomunitasEntities();
            var acara = from c in db.Komunitas
                        where c.jenis_komunitas == "Design"
                        orderby c.Id_komunitas ascending
                        select c;
            return View(acara);      
        }

        public ViewResult Photo()
        {

            DataKomunitasEntities db = new DataKomunitasEntities();
            var acara = from c in db.Komunitas
                        where c.jenis_komunitas == "Photography"
                        orderby c.Id_komunitas ascending
                        select c;
            return View(acara);       
	    }
    }

}