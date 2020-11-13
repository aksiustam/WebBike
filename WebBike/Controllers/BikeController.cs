using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBike.Models;

namespace WebBike.Controllers
{
    public class BikeController : Controller
    {
        private BikeDBEntities db = new BikeDBEntities();

        // GET: Bike
        public ActionResult Index()
        {
            var bike = db.Bike.Include(b => b.Marka).Include(b => b.Tip);
            return View(bike.ToList());
        }

        // GET: Bike/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bike.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }

            var query = from st in db.ResimDatas
                        where st.ResimId == id
                        select st;
            var resim = query.FirstOrDefault<ResimData>();
            if(resim != null)
            {
                ViewBag.Resim = resim.Path.ToString();
            }
            
            

            return View(bike);
        }

        // GET: Bike/Create
        public ActionResult Create()
        {
            ViewBag.MarkaId = new SelectList(db.Marka, "Id", "Ad");
            ViewBag.TipId = new SelectList(db.Tip, "Id", "Ad");
            return View();
        }

        // POST: Bike/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TipId,MarkaId,ResimId,Ad,Fiyat,Skor,Tarih,Hiz")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Bike.Add(bike);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarkaId = new SelectList(db.Marka, "Id", "Ad", bike.MarkaId);
            ViewBag.TipId = new SelectList(db.Tip, "Id", "Ad", bike.TipId);
            return View(bike);
        }

        // GET: Bike/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bike.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarkaId = new SelectList(db.Marka, "Id", "Ad", bike.MarkaId);
            ViewBag.TipId = new SelectList(db.Tip, "Id", "Ad", bike.TipId);
            return View(bike);
        }

        // POST: Bike/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TipId,MarkaId,ResimId,Ad,Fiyat,Skor,Tarih,Hiz")] Bike bike)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bike).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarkaId = new SelectList(db.Marka, "Id", "Ad", bike.MarkaId);
            ViewBag.TipId = new SelectList(db.Tip, "Id", "Ad", bike.TipId);
            return View(bike);
        }

        // GET: Bike/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bike bike = db.Bike.Find(id);
            if (bike == null)
            {
                return HttpNotFound();
            }
            return View(bike);
        }

        // POST: Bike/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bike bike = db.Bike.Find(id);
            db.Bike.Remove(bike);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        [HttpPost]
        
        public ActionResult Upload(FormCollection fc, HttpPostedFileBase file)
        {
            ResimData data = new ResimData();
            var allowedExtensions = new[] {  ".Jpg", ".png", ".jpg", "jpeg"  };
            data.ResimId = int.Parse(fc["Id"]); // Formdaki veriyi alma yöntemlerden birisi
            data.Name = fc["Name"].ToString(); // Formdaki veriyi alma yöntemlerden birisi

            //var fileName = Path.GetFileName(file.FileName);  // Dosya adını alır
            var ext = Path.GetExtension(file.FileName); // Dosyanın uzantısını alır


            if (allowedExtensions.Contains(ext)) //Dosyanın uzantısını kontrol eder
            {
                //string name = Path.GetFileNameWithoutExtension(fileName);  //Dosyanın uzantısız adı
                string name = data.Name; //Biz atadık 
                ext = ".png"; // Uzantısı ayni olsun
                string myfile = name + "_" + data.ResimId + ext; // Dosyanın Tam adını oluşturulur 

                // Dosyayı Kaydetme işlemi ( Resim klasörüne )  
                var path = Path.Combine(Server.MapPath("~/Resim"), myfile); 
                data.Path = myfile; // Veritabanındaki resmin yolu
                db.ResimDatas.Add(data);
                db.SaveChanges();
                file.SaveAs(path); 
            }
            else
            {
                ViewBag.message = "Please choose only Image file";
            }

            return RedirectToAction("Details", "Bike", new { id = data.ResimId });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
