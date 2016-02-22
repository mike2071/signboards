using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignBoards.Models;

namespace SignBoards.Controllers
{
    [Authorize]
    public class ContractorsController : Controller
    {
        private SignBoardsContext db = new SignBoardsContext();

        // GET: Contractors
        public ActionResult Index()
        {
            var contractors = db.Contractors.Include(c => c.Contact).Include(c => c.ContractorAddress);
            return View(contractors.ToList());
        }

        // GET: Contractors/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // GET: Contractors/Create
        public async Task<ActionResult> Create()
        {
            var userId = new Guid(User.Identity.GetUserId());
            var company = await db.Companies.FirstOrDefaultAsync(con => con.CreatedByUserId == userId);
            if (company != null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name");
            ViewBag.ContractorAddressId = new SelectList(db.ContractorAddresses, "Id", "Address1");
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContractorCreateViewModel contractorCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                contractorCreateViewModel.Contractor = new Contractor();
                contractorCreateViewModel.Contractor.Id = Guid.NewGuid();
                contractorCreateViewModel.Contractor.CreateDate = DateTime.Now;
                contractorCreateViewModel.Contractor.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());

                var contactId = Guid.NewGuid();
                contractorCreateViewModel.Contact.Id = contactId;

                contractorCreateViewModel.Contractor.ContactId = contactId;
                contractorCreateViewModel.Contact.CreateDate = DateTime.Now;
                contractorCreateViewModel.Contact.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());

                var companyAddressId = Guid.NewGuid();
                contractorCreateViewModel.ContractorAddress.Id = companyAddressId;

                contractorCreateViewModel.Contractor.ContractorAddressId = companyAddressId;
                contractorCreateViewModel.Contractor.CreateDate = DateTime.Now;
                contractorCreateViewModel.Contractor.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());

                contractorCreateViewModel.ContractorAddress.CreateDate = DateTime.Now;
                contractorCreateViewModel.ContractorAddress.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());

                db.Contacts.Add(contractorCreateViewModel.Contact);
                db.ContractorAddresses.Add(contractorCreateViewModel.ContractorAddress);
                db.Contractors.Add(contractorCreateViewModel.Contractor);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            
            return View(contractorCreateViewModel);
        }

        // GET: Contractors/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", contractor.ContactId);
            ViewBag.ContractorAddressId = new SelectList(db.ContractorAddresses, "Id", "Address1", contractor.ContractorAddressId);
            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ContactId,ContractorAddressId,CreateDate,CreatedByUserId,UpdatedDate,UpdatedByUserId")] Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contractor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", contractor.ContactId);
            ViewBag.ContractorAddressId = new SelectList(db.ContractorAddresses, "Id", "Address1", contractor.ContractorAddressId);
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contractor contractor = db.Contractors.Find(id);
            if (contractor == null)
            {
                return HttpNotFound();
            }
            return View(contractor);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Contractor contractor = db.Contractors.Find(id);
            db.Contractors.Remove(contractor);
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
