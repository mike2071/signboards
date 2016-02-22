using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SignBoards.Models;

namespace SignBoards.Controllers
{
    [Authorize]
    public class SignBoardBidsController : Controller
    {
        private SignBoardsContext db = new SignBoardsContext();

        public SignBoardBidsController()
        {
        }

        [Authorize]
        // GET: SignBoardBids
        public ActionResult Index()
        {
            var signBoardBids = db.SignBoardBids
                .Include(s => s.Contractor)
                .Include(s => s.SignBoard);

            return View(signBoardBids.ToList());
        }


        // GET: SignBoards
        public async Task<ViewResult> Bids(Guid userId)
        {
            var signBoardBids =
                db.SignBoardBids
                .Include(s => s.Contractor)
                    .Include(s => s.SignBoard)
                    .Where(board => board.CreatedByUserId == userId);

           await SetBidWinner(signBoardBids);
            return View(signBoardBids.ToList());
        }


        // GET: SignBoardBids/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignBoardBid signBoardBid = db.SignBoardBids.Find(id);
            if (signBoardBid == null)
            {
                return HttpNotFound();
            }
            return View(signBoardBid);
        }

        // GET: SignBoardBids/Create
        public async Task<ActionResult> Create(Guid signBoardBidId)
        {
            var userId = new Guid(User.Identity.GetUserId());
            var company = db.Companies.FirstOrDefault(com => com.CreatedByUserId == userId);

            if (company != null)
            {
                return RedirectToAction("Index", "Home");
            }

            var contractor = db.Contractors.FirstOrDefault(con => con.CreatedByUserId == userId);

            if (contractor != null)
            {
                return RedirectToAction("Create", "Contractors");
            }

            var signBoardBidCreateViewModel = new SignBoardBidCreateViewModel
            {
                SignBoardBidId = signBoardBidId,
                ContractorIdId = contractor.Id
            };
            return View(signBoardBidCreateViewModel);
        }

        // POST: SignBoardBids/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SignBoardBidCreateViewModel signBoardBidCreateViewModel)
        {
            if (ModelState.IsValid)
            {
                signBoardBidCreateViewModel.SignBoardBid.Id = Guid.NewGuid();
                signBoardBidCreateViewModel.SignBoardBid.CreateDate = DateTime.Now;
                signBoardBidCreateViewModel.SignBoardBid.BidDate = DateTime.Now;
                signBoardBidCreateViewModel.SignBoardBid.CreatedByUserId = Guid.Parse(User.Identity.GetUserId());
                signBoardBidCreateViewModel.SignBoardBid.ContractorId = signBoardBidCreateViewModel.ContractorIdId;
                signBoardBidCreateViewModel.SignBoardBid.SignBoardId = signBoardBidCreateViewModel.SignBoardBidId;

                db.SignBoardBids.Add(signBoardBidCreateViewModel.SignBoardBid);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(signBoardBidCreateViewModel);
        }

        // GET: SignBoardBids/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignBoardBid signBoardBid = db.SignBoardBids.Find(id);
            if (signBoardBid == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "Id", "Id", signBoardBid.ContractorId);
            ViewBag.SignBoardId = new SelectList(db.SignBoards, "Id", "FittingInstructions", signBoardBid.SignBoardId);
            return View(signBoardBid);
        }

        // POST: SignBoardBids/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SignBoardId,ContractorId,BidAmount,BidDate,IsBidWinner,CreateDate,CreatedByUserId,UpdatedDate,UpdatedByUserId")] SignBoardBid signBoardBid)
        {
            if (ModelState.IsValid)
            {
                db.Entry(signBoardBid).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContractorId = new SelectList(db.Contractors, "Id", "Id", signBoardBid.ContractorId);
            ViewBag.SignBoardId = new SelectList(db.SignBoards, "Id", "FittingInstructions", signBoardBid.SignBoardId);
            return View(signBoardBid);
        }

        // GET: SignBoardBids/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SignBoardBid signBoardBid = db.SignBoardBids.Find(id);
            if (signBoardBid == null)
            {
                return HttpNotFound();
            }
            return View(signBoardBid);
        }

        // POST: SignBoardBids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SignBoardBid signBoardBid = db.SignBoardBids.Find(id);
            db.SignBoardBids.Remove(signBoardBid);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SignBoards
        private async Task SetBidWinner(IEnumerable<SignBoardBid> signboardBids)
        {
            var highestBidId = Guid.Empty;
            var highestBidAmount = 0m;
            foreach (var signboardBid in signboardBids)
            {
                if (signboardBid.CreateDate <= DateTime.UtcNow.AddDays(-1))
                {
                    if (signboardBid.BidAmount > highestBidAmount)
                    {
                        highestBidAmount = signboardBid.BidAmount;
                        highestBidId = signboardBid.Id;
                    }
                }
            }

            if (highestBidId != Guid.Empty)
            {
                var signBoardBid =
                    await db.SignBoardBids
                        .SingleOrDefaultAsync(bid => bid.Id == highestBidId);

                signBoardBid.IsBidWinner = true;
               db.Entry(signBoardBid).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }
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
