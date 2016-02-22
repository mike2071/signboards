using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignBoards.Models;

namespace SignBoards.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private SignBoardsContext db = new SignBoardsContext();

        // GET: Companies
        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.CompanyAddress).Include(c => c.Contact);
            return View(companies.ToList());
        }

        // GET: Companies/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Companies/Create
        public ActionResult Create()
        {
            ViewBag.CompanyAddressId = new SelectList(db.CompanyAddresses, "Id", "Address1");
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name");
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanyCreateViewModel companyCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                companyCreateViewModel.Company.Id = Guid.NewGuid();
                companyCreateViewModel.Company.CreateDate = DateTime.Now;
                companyCreateViewModel.Company.CreatedByUserId =Guid.Parse(User.Identity.GetUserId());

                var contactId = Guid.NewGuid();
                companyCreateViewModel.Contact.Id = contactId;

                companyCreateViewModel.Company.ContactId = contactId;
                companyCreateViewModel.Contact.CreateDate = DateTime.Now;
                companyCreateViewModel.Contact.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());

                var companyAddressId = Guid.NewGuid();
                companyCreateViewModel.CompanyAddress.Id = companyAddressId;

                companyCreateViewModel.Company.CompanyAddressId = companyAddressId;
                companyCreateViewModel.Company.CreateDate = DateTime.Now;
                companyCreateViewModel.Company.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());

                companyCreateViewModel.CompanyAddress.CreateDate = DateTime.Now;
                companyCreateViewModel.CompanyAddress.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());

                db.Contacts.Add(companyCreateViewModel.Contact);
                db.CompanyAddresses.Add(companyCreateViewModel.CompanyAddress);
                db.Companies.Add(companyCreateViewModel.Company);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyAddressId = new SelectList(db.CompanyAddresses, "Id", "Address1", companyCreateViewModel.Company.CompanyAddressId);
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", companyCreateViewModel.Company.ContactId);
            return View(companyCreateViewModel);
        }

        // GET: Companies/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyAddressId = new SelectList(db.CompanyAddresses, "Id", "Address1", company.CompanyAddressId);
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", company.ContactId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ContactId,CompanyAddressId,CreateDate,CreatedByUserId,UpdatedDate,UpdatedByUserId")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyAddressId = new SelectList(db.CompanyAddresses, "Id", "Address1", company.CompanyAddressId);
            ViewBag.ContactId = new SelectList(db.Contacts, "Id", "Name", company.ContactId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companies.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Company company = db.Companies.Find(id);
            db.Companies.Remove(company);
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
