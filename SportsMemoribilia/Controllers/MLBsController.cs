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
    public class MLBsController : Controller
    {
        private SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();

        // GET: MLBs
        public ActionResult Index(string searchString)
        {
            var type = from a in db.MLBs
                       select a;
            if (!String.IsNullOrEmpty(searchString))
            {
                type = type.Where(s => s.Type.Contains(searchString));
            }
            return View(type);
        }

        // GET: MLBs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MLB mLB = db.MLBs.Find(id);
            if (mLB == null)
            {
                return HttpNotFound();
            }
            return View(mLB);
        }

        [Authorize(Roles = "Administrator")]
        // GET: MLBs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Baseball/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] MLB mLB, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    SportsMemoribiliaStoreEntities db = new SportsMemoribiliaStoreEntities();
                    mLB.Image = new byte[file.ContentLength];
                    file.InputStream.Read(mLB.Image, 0, file.ContentLength);

                    db.MLBs.Add(mLB);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("../MLBs");
        }

        [Authorize(Roles = "Administrator")]
        // GET: MLBs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MLB mLB = db.MLBs.Find(id);
            if (mLB == null)
            {
                return HttpNotFound();
            }
            return View(mLB);
        }

        // POST: Baseball/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PhotoID,Image,Title,Description,Type,Price")] MLB mLB, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    mLB.Image = new byte[file.ContentLength];
                    file.InputStream.Read(mLB.Image, 0, file.ContentLength);

                    db.Entry(mLB).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }

            return View("mLB");
        }

        [Authorize(Roles = "Administrator")]
        // GET: MLBs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MLB mLB = db.MLBs.Find(id);
            if (mLB == null)
            {
                return HttpNotFound();
            }
            return View(mLB);
        }

        // POST: MLBs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MLB mLB = db.MLBs.Find(id);
            db.MLBs.Remove(mLB);
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
