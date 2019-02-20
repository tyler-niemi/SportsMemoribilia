using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportsMemoribilia;

namespace SportsMemoribilia.Controllers
{
    public class NBAsController : Controller
    {
        private SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();

        // GET: NBAs
        public ActionResult Index(string searchString)
        {
            var type = from a in db.NBAs
                       select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                type = type.Where(s => s.Type.Contains(searchString));
            }
            return View(type);
        }

        // GET: NBAs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NBA nBA = db.NBAs.Find(id);
            if (nBA == null)
            {
                return HttpNotFound();
            }
            return View(nBA);
        }

        [Authorize(Roles = "Administrator")]
        // GET: NBAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Baseball/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] NBA nBA, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();
                    nBA.Image = new byte[file.ContentLength];
                    file.InputStream.Read(nBA.Image, 0, file.ContentLength);

                    db.NBAs.Add(nBA);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("../NBAs");
        }

        [Authorize(Roles = "Administrator")]
        // GET: NBAs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NBA nBA = db.NBAs.Find(id);
            if (nBA == null)
            {
                return HttpNotFound();
            }
            return View(nBA);
        }

        // POST: Baseball/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] NBA nBA, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    nBA.Image = new byte[file.ContentLength];
                    file.InputStream.Read(nBA.Image, 0, file.ContentLength);

                    db.Entry(nBA).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View("nBA");
        }

        [Authorize(Roles = "Administrator")]
        // GET: NBAs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NBA nBA = db.NBAs.Find(id);
            if (nBA == null)
            {
                return HttpNotFound();
            }
            return View(nBA);
        }

        // POST: NBAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NBA nBA = db.NBAs.Find(id);
            db.NBAs.Remove(nBA);
            db.SaveChanges();
            return RedirectToAction("Index");
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
