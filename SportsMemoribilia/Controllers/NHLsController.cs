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
    public class NHLsController : Controller
    {
        private SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();

        // GET: NHLs
        public ActionResult Index(string searchString)
        {
            var type = from a in db.NHLs
                       select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                type = type.Where(s => s.Type.Contains(searchString));
            }
            return View(type);
        }

        // GET: NHLs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHL nHL = db.NHLs.Find(id);
            if (nHL == null)
            {
                return HttpNotFound();
            }
            return View(nHL);
        }

        [Authorize(Roles = "Administrator")]
        // GET: NHLs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Baseball/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] NHL nHL, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();
                    nHL.Image = new byte[file.ContentLength];
                    file.InputStream.Read(nHL.Image, 0, file.ContentLength);

                    db.NHLs.Add(nHL);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("../NHLs");
        }

        [Authorize(Roles = "Administrator")]
        // GET: NHLs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHL nHL = db.NHLs.Find(id);
            if (nHL == null)
            {
                return HttpNotFound();
            }
            return View(nHL);
        }

        // POST: Baseball/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] NHL nHL, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    nHL.Image = new byte[file.ContentLength];
                    file.InputStream.Read(nHL.Image, 0, file.ContentLength);

                    db.Entry(nHL).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View("nHL");
        }

        [Authorize(Roles = "Administrator")]
        // GET: NHLs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHL nHL = db.NHLs.Find(id);
            if (nHL == null)
            {
                return HttpNotFound();
            }
            return View(nHL);
        }

        // POST: NHLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHL nHL = db.NHLs.Find(id);
            db.NHLs.Remove(nHL);
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
