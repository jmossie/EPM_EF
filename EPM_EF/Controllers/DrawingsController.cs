using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPM_EF.DAL;
using EPM_EF.Models;

namespace EPM_EF.Controllers
{
    public class DrawingsController : Controller
    {
        private EPMContext db = new EPMContext();

        // GET: Drawings
        public ActionResult Index(string sortOrder, string searchString, string ShowAllRevs)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "DrawingNumber" : "";
            ViewBag.DateSortParm = sortOrder == "Creation Date" ? "CreateDate" : "Date";
            ViewBag.RevVisibilityParm = String.IsNullOrEmpty(ShowAllRevs) || ShowAllRevs == "All" ? "All" : "Latest";
            if (!String.IsNullOrEmpty(searchString))
            {
                var drawings = db.Drawings.Where(d => d.DrawingNumber.Contains(searchString)
                                       || d.DrawingName.Contains(searchString)).ToList();
                return View(drawings);
            }
            var numDrawings = db.Drawings.Count();
            ViewBag.NumOfDrawings = numDrawings;
            return View(db.Drawings.ToList());
        }

        // GET: Drawings/Details/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drawing drawing = db.Drawings.Find(id);
            if (drawing == null)
            {
                return HttpNotFound();
            }
            return View(drawing);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,DrawingNumber,DrawingName,CreationDate,LatestRev,Notes")] Drawing drawing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(drawing).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Your entry was successfully updated";
                return RedirectToAction("Index");
            }
            return View(drawing);
        }
        public ActionResult RevCreate(string DrawingId)
        {
            string lastrevision = db.DrawingRevisions.Where(dr => dr.DrawingID == DrawingId).ToList().OrderBy(dr => dr.RevisionNumber).LastOrDefault().RevisionNumber;
            var charrevision = lastrevision.ToCharArray()[0];
            charrevision++;
            ViewBag.drawingNumber = db.Drawings.Where(d => d.ID == DrawingId).SingleOrDefault().DrawingNumber;
            string uID = Guid.NewGuid().ToString();    
            var nextrevision = charrevision.ToString();
            var revision = new DrawingRevision()
            {
                CreationDate = DateTime.Today,
                DrawingID = DrawingId,
                RevisionNumber = nextrevision,
                ID = uID
            };
            SetupStatusSelectListItems();
            return View(revision);
        }

        [HttpPost]
        public ActionResult RevCreate([Bind(Include = "ID,RevisionNumber,RevisionName,CreationDate,DrawingID,ReleaseStatus_ID,Notes")] DrawingRevision revision)
        {
            //ValidateEntry(drawing);
            if (ModelState.IsValid)
            {
                db.DrawingRevisions.Add(revision);
                db.SaveChanges();
                TempData["Message"] = "Your entry was successfully added";
                return RedirectToAction("Index");
            }

            //SetupStatusSelectListItems();
            return View(revision);
        }

        // GET: Drawings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drawings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DrawingNumber,DrawingName,CreationDate,LatestRev,Notes")] Drawing drawing)
        {
            if (ModelState.IsValid)
            {
                db.Drawings.Add(drawing);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(drawing);
        }

       

        // GET: Drawings/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drawing drawing = db.Drawings.Find(id);
            if (drawing == null)
            {
                return HttpNotFound();
            }
            return View(drawing);
        }

        // POST: Drawings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Drawing drawing = db.Drawings.Find(id);
            db.Drawings.Remove(drawing);
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
        private void SetupStatusSelectListItems()
        {
            ViewBag.StatusSelectListItems = new SelectList(
               db.ReleaseStatus, "ID", "StatusName");
        }
    }
}
