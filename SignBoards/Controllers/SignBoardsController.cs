using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignBoards.Models;
using SignBoards.Services;

namespace SignBoards.Controllers
{
    [Authorize]
    public class SignBoardsController : Controller
    {
        private SignBoardsContext db = new SignBoardsContext();

        private readonly ISignBoardServices _signBoardServices= new SignBoardServices();
        
        // GET: SignBoards
        public ActionResult Index()
        {
            var signBoards = _signBoardServices.GetAllSignBoards();
            return View(signBoards);
        }

        // GET: SignBoards/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignBoard signBoard = db.SignBoards.Find(id);
            if (signBoard == null)
            {
                return HttpNotFound();
            }
            return View(signBoard);
        }

        // GET: SignBoards/Create
        public ActionResult Create()
        {
            var userId = new Guid(User.Identity.GetUserId());
            var company = db.Companies.FirstOrDefault(com => com.CreatedByUserId == userId);

            if (company != null)
            {
               return RedirectToAction("Create", "Companies");
            }
            var signboardCreateViewModel = new SignBoardCreateViewModel();
            signboardCreateViewModel.FittingTypes = CreateSelectList();
            return View(signboardCreateViewModel);
        }

        // POST: SignBoards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SignBoardCreateViewModel signBoardCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                signBoardCreateViewModel.SignBoard.Id = Guid.NewGuid();
                signBoardCreateViewModel.SignBoard.CreateDate = DateTime.Now;
                signBoardCreateViewModel.SignBoard.CreatedByUserId = new Guid(userId);

                var signBoardAddressId = Guid.NewGuid();
                signBoardCreateViewModel.SignBoardAddress.Id = signBoardAddressId;

                signBoardCreateViewModel.SignBoardAddress.CreateDate = DateTime.Now;
                signBoardCreateViewModel.SignBoardAddress.CreatedByUserId = new Guid(User.Identity.GetUserId());

                var company = await db.Companies.SingleOrDefaultAsync(company1 => company1.CreatedByUserId == new Guid(userId));
                signBoardCreateViewModel.SignBoard.CompanyId = company.Id;
                signBoardCreateViewModel.SignBoard.SignBoardAddressId = signBoardAddressId;

                signBoardCreateViewModel.SignBoard.FittingTypeId = signBoardCreateViewModel.FittingType;

                if (company.CompanyAddressId != null)
                {
                    signBoardCreateViewModel.CompanyAddressId = company.CompanyAddressId.Value;
                }

                db.SignBoardAddresses.Add(signBoardCreateViewModel.SignBoardAddress);

                db.SignBoards.Add(signBoardCreateViewModel.SignBoard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(signBoardCreateViewModel);
        }

        // GET: SignBoards/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignBoard signBoard = db.SignBoards.Find(id);
            if (signBoard == null)
            {
                return HttpNotFound();
            }
            return View(signBoard);
        }

        // POST: SignBoards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyId,CompanyAddressId,SignBoardAddressId,FittingInstructions,FittingCharge,CreateDate,CreatedByUserId,UpdatedDate,UpdatedByUserId")] SignBoard signBoard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(signBoard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(signBoard);
        }

        // GET: SignBoards/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignBoard signBoard = db.SignBoards.Find(id);
            if (signBoard == null)
            {
                return HttpNotFound();
            }
            return View(signBoard);
        }

        // POST: SignBoards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SignBoard signBoard = db.SignBoards.Find(id);
            db.SignBoards.Remove(signBoard);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: SignBoards
        public ActionResult MySignBoards()
        {
            var userId = User.Identity.GetUserId();
            var signBoards = db.SignBoards
                .Include(s => s.Company)
                .Include(s => s.SignBoardAddress)
                .Where(board => board.CreatedByUserId == new Guid(userId));
            return View(signBoards.ToList());
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IEnumerable<SelectListItem> CreateSelectList()
        {
            var single = new SelectListItem
            {
                Value = LookupConstants.Signboard.Fitting.Type.Single.ToString(),
                Text = LookupConstants.Signboard.Fitting.Type.SingleText
            };

            var lifeTime = new SelectListItem
            {
                Value = LookupConstants.Signboard.Fitting.Type.LifeTime.ToString(),
                Text = LookupConstants.Signboard.Fitting.Type.LifeTimeText
            };


            var fittingTypes = new List<SelectListItem> { single, lifeTime };
            return fittingTypes;
        }

    }
}
