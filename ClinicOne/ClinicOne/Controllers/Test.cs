using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicOne;

namespace ClinicOne.Controllers
{
    public class Test : Controller
    {
        private ClinicOneEntities db = new ClinicOneEntities();

        // GET: Test
        public async Task<ActionResult> Index()
        {
            var waitings = db.Waitings.Include(w => w.AspNetUser).Include(w => w.Patient);
            return View(await waitings.ToListAsync());
        }

        // GET: Test/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waiting waiting = await db.Waitings.FindAsync(id);
            if (waiting == null)
            {
                return HttpNotFound();
            }
            return View(waiting);
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "AspNetUserId");
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,PatientId,Schedule,Remarks,AspNetUserId")] Waiting waiting)
        {
            if (ModelState.IsValid)
            {
                waiting.Id = Guid.NewGuid();
                db.Waitings.Add(waiting);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", waiting.AspNetUserId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "AspNetUserId", waiting.PatientId);
            return View(waiting);
        }

        // GET: Test/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waiting waiting = await db.Waitings.FindAsync(id);
            if (waiting == null)
            {
                return HttpNotFound();
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", waiting.AspNetUserId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "AspNetUserId", waiting.PatientId);
            return View(waiting);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PatientId,Schedule,Remarks,AspNetUserId")] Waiting waiting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(waiting).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AspNetUserId = new SelectList(db.AspNetUsers, "Id", "Email", waiting.AspNetUserId);
            ViewBag.PatientId = new SelectList(db.Patients, "Id", "AspNetUserId", waiting.PatientId);
            return View(waiting);
        }

        // GET: Test/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Waiting waiting = await db.Waitings.FindAsync(id);
            if (waiting == null)
            {
                return HttpNotFound();
            }
            return View(waiting);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Waiting waiting = await db.Waitings.FindAsync(id);
            db.Waitings.Remove(waiting);
            await db.SaveChangesAsync();
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
