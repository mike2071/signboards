using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SignBoards.Models;

namespace SignBoards.Services
{
    public class SignBoardServices : ISignBoardServices
    {
        private SignBoardsContext db = new SignBoardsContext();

        public IEnumerable<SignBoardsModel> GetAllSignBoards()
        {
            var signBoards = db.SignBoards
            .Include(s => s.Company)
            .Include(s => s.SignBoardAddress);

            var signBoardsIndexViewModels = new List<SignBoardsModel>();
            foreach (var signBoard in signBoards)
            {
                var signboardIndexViewModel = new SignBoardsModel
                {
                    Id = signBoard.Id,
                    CreatedByUserId = signBoard.CreatedByUserId,
                    CompanyName = signBoard.Company.Name,
                    FittingAddressPostcode = signBoard.SignBoardAddress.Postcode,
                    FittingCharge = signBoard.FittingCharge,
                    FittingInstructions = signBoard.FittingInstructions
                };

                var companyAddress =
                    db.CompanyAddresses.FirstOrDefault(address => address.Id == signBoard.Company.CompanyAddressId)
;

                if (companyAddress != null) signboardIndexViewModel.CompanyFirstlineAddress = companyAddress.Address1;

                if (signBoard.FittingTypeId != Guid.Empty)
                {
                    if (signBoard.FittingTypeId == LookupConstants.Signboard.Fitting.Type.Single)
                    {
                        signboardIndexViewModel.FittingType = LookupConstants.Signboard.Fitting.Type.SingleText;
                    }
                    else if (signBoard.FittingTypeId == LookupConstants.Signboard.Fitting.Type.LifeTime)
                    {
                        signboardIndexViewModel.FittingType = LookupConstants.Signboard.Fitting.Type.LifeTimeText;
                    }
                }
                signBoardsIndexViewModels.Add(signboardIndexViewModel);
            }

            return signBoardsIndexViewModels;
        }
        public IEnumerable<SignBoardsModel> TakeSignBoards(int amount)
        {
            var signBoards = db.SignBoards
            .Include(s => s.Company)
            .Include(s => s.SignBoardAddress)
            .Take(amount);

            var signBoardsIndexViewModels = new List<SignBoardsModel>();
            foreach (var signBoard in signBoards)
            {
                var signboardIndexViewModel = new SignBoardsModel
                {
                    Id = signBoard.Id,
                    CreatedByUserId = signBoard.CreatedByUserId,
                    CompanyName = signBoard.Company.Name,
                    FittingAddressPostcode = signBoard.SignBoardAddress.Postcode,
                    FittingCharge = signBoard.FittingCharge,
                    FittingInstructions = signBoard.FittingInstructions
                };

                var companyAddress =
                    db.CompanyAddresses.FirstOrDefault(address => address.Id == signBoard.Company.CompanyAddressId)
;

                if (companyAddress != null) signboardIndexViewModel.CompanyFirstlineAddress = companyAddress.Address1;

                if (signBoard.FittingTypeId != Guid.Empty)
                {
                    if (signBoard.FittingTypeId == LookupConstants.Signboard.Fitting.Type.Single)
                    {
                        signboardIndexViewModel.FittingType = LookupConstants.Signboard.Fitting.Type.SingleText;
                    }
                    else if (signBoard.FittingTypeId == LookupConstants.Signboard.Fitting.Type.LifeTime)
                    {
                        signboardIndexViewModel.FittingType = LookupConstants.Signboard.Fitting.Type.LifeTimeText;
                    }
                }
                signBoardsIndexViewModels.Add(signboardIndexViewModel);
            }

            return signBoardsIndexViewModels;
        }
    }
}