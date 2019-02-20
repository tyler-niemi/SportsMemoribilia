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
    public class NFLsController : Controller
    {
        private SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();

        // GET: NFLs
        public ActionResult Index(string searchString)
        {
            var type = from a in db.NFLs
                       select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                type = type.Where(s => s.Type.Contains(searchString));
            }
            return View(type);
        }

        // GET: NFLs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NFL nFL = db.NFLs.Find(id);
            if (nFL == null)
            {
                return HttpNotFound();
            }
            return View(nFL);
        }

        [Authorize(Roles = "Administrator")]
        // GET: NFLs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Baseball/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] NFL nFl, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();
                    nFl.Image = new byte[file.ContentLength];
                    file.InputStream.Read(nFl.Image, 0, file.ContentLength);

                    db.NFLs.Add(nFl);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("../NFLs");
        }

        [Authorize(Roles = "Administrator")]
        // GET: NFLs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NFL nFL = db.NFLs.Find(id);
            if (nFL == null)
            {
                return HttpNotFound();
            }
            return View(nFL);
        }

        // POST: Baseball/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] NFL nFl, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    nFl.Image = new byte[file.ContentLength];
                    file.InputStream.Read(nFl.Image, 0, file.ContentLength);

                    db.Entry(nFl).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View("nFl");
        }

        [Authorize(Roles = "Administrator")]
        // GET: NFLs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NFL nFL = db.NFLs.Find(id);
            if (nFL == null)
            {
                return HttpNotFound();
            }
            return View(nFL);
        }

        // POST: NFLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NFL nFL = db.NFLs.Find(id);
            db.NFLs.Remove(nFL);
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
