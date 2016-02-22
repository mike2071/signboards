using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SignBoards.Models;

namespace SignBoards.Controllers
{
    public class CompanyAddressesController : Controller
    {
        private SignBoardsContext db = new SignBoardsContext();

        // GET: CompanyAddresses
        public ActionResult Index()
        {
            return View(db.CompanyAddresses.ToList());
        }

        // GET: CompanyAddresses/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAddress companyAddress = db.CompanyAddresses.Find(id);
            if (companyAddress == null)
            {
                return HttpNotFound();
            }
            return View(companyAddress);
        }

        // GET: CompanyAddresses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanyAddresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address1,Address2,City,Postcode,CreateDate,CreatedByUserId,UpdatedDate,UpdatedByUserId")] CompanyAddress companyAddress)
        {
            if (ModelState.IsValid)
            {
                companyAddress.Id = Guid.NewGuid();
                db.CompanyAddresses.Add(companyAddress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(companyAddress);
        }

        // GET: CompanyAddresses/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAddress companyAddress = db.CompanyAddresses.Find(id);
            if (companyAddress == null)
            {
                return HttpNotFound();
            }
            return View(companyAddress);
        }

        // POST: CompanyAddresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address1,Address2,City,Postcode,CreateDate,CreatedByUserId,UpdatedDate,UpdatedByUserId")] CompanyAddress companyAddress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(companyAddress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(companyAddress);
        }

        // GET: CompanyAddresses/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CompanyAddress companyAddress = db.CompanyAddresses.Find(id);
            if (companyAddress == null)
            {
                return HttpNotFound();
            }
            return View(companyAddress);
        }

        // POST: CompanyAddresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            CompanyAddress companyAddress = db.CompanyAddresses.Find(id);
            db.CompanyAddresses.Remove(companyAddress);
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
