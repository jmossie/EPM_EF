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
            var numDrawings = db.Drawings.Count();
            ViewBag.NumOfDrawings = numDrawings;
            if (!String.IsNullOrEmpty(searchString))
            {
                var drawings = db.Drawings.Where(d => d.DrawingNumber.Contains(searchString)
                                       || d.DrawingName.Contains(searchString)).ToList();
                return View(drawings);
            }
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
                TempData["Message"] = "Your drawing was successfully updated";
                return RedirectToAction("Index");
            }
            return View(drawing);
        }
        public ActionResult RevCreate(string DrawingId)
        {

            var revisionList = db.DrawingRevisions.Where(dr => dr.DrawingID == DrawingId).ToList();
            string nextrevision;
            if (revisionList != null)
            {
                string lastrevision = revisionList.OrderBy(dr => dr.RevisionNumber).LastOrDefault().RevisionNumber;
                var charrevision = lastrevision.ToCharArray()[0];
                charrevision++;
                nextrevision = charrevision.ToString();
            }
            else
            {
                nextrevision = "A";
            }


            
            ViewBag.drawingNumber = db.Drawings.Where(d => d.ID == DrawingId).SingleOrDefault().DrawingNumber;
            string uID = Guid.NewGuid().ToString();                
            var revision = new DrawingRevision()
            {
                CreationDate = DateTime.UtcNow,
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
                TempData["Message"] = "Your revision was successfully added";
                return RedirectToAction("Index");
            }

            //SetupStatusSelectListItems();
            return View(revision);
        }

        // GET: Drawings/Create
        public ActionResult Create()
        {
            string uID = Guid.NewGuid().ToString();
            var drawing = new Drawing()
            {
                CreationDate = DateTime.UtcNow,
                ID = uID,
            };
            return View(drawing);
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
                DrawingRevision firstRevision = new DrawingRevision()
                {
                    DrawingID = drawing.ID,
                    RevisionNumber = "A",
                    RevisionName = drawing.DrawingName,
                    CreationDate = drawing.CreationDate, 
                    ID = Guid.NewGuid().ToString()
                    //releaseStatus = new ReleaseStatus() { ID="1"}
            };
                db.DrawingRevisions.Add(firstRevision);
                db.SaveChanges();
                TempData["Message"] = "Your drawing was successfully added";
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
            var revisionList = db.DrawingRevisions.Where(dr => dr.DrawingID == id).ToList();
            revisionList.ForEach(delegate (DrawingRevision revision)
                {
                    db.DrawingRevisions.Remove(revision);
                });
            db.SaveChanges();
            TempData["Message"] = "Your drawing and all it's revisions were successfully deleted";
            return RedirectToAction("Index");
        }
        public ActionResult RevEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrawingRevision revision = db.DrawingRevisions.Find(id);
            if (revision == null)
            {
                return HttpNotFound();
            }
            SetupStatusSelectListItems();
            return View(revision);
        }

        [HttpPost]
        public ActionResult RevEdit([Bind(Include = "ID,RevisionNumber,RevisionName,CreationDate,DrawingID,Notes")] DrawingRevision revision)
        {
            if (ModelState.IsValid)
            {
                db.Entry(revision).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Your revision was successfully updated";
                return RedirectToAction("Index");
            }
            return View(revision);
        }
        public ActionResult RevDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrawingRevision revision = db.DrawingRevisions.Find(id);
            if (revision == null)
            {
                return HttpNotFound();
            }
            return View(revision);
        }

        // POST: Drawings/Delete/5
        [HttpPost, ActionName("RevDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult RevDeleteConfirmed(string id)
        {
            DrawingRevision revision = db.DrawingRevisions.Find(id);
            db.DrawingRevisions.Remove(revision);
            db.SaveChanges();
            TempData["Message"] = "Your revision was successfully deleted";
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
