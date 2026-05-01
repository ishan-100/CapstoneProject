using System.Collections.Generic;
using BikeDealerMgmtAPI.Models;

namespace BikeDealerMgmtAPI.Services.Interfaces
{
    public interface IDealerService
    {
        int AddDealer(Dealer dealer);
        List<Dealer> GetDealers();
        Dealer? FindDealerById(int id);
        Dealer? FindDealerByName(string name);
        int UpdateDealer(int id, Dealer dealer);
        int DeleteDealer(int id);
    }
}