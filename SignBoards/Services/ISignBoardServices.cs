using System.Collections.Generic;
using SignBoards.Models;

namespace SignBoards.Services
{
    public interface ISignBoardServices
    {
        IEnumerable<SignBoardsModel> GetAllSignBoards();

        IEnumerable<SignBoardsModel> TakeSignBoards(int amount);
    }
}