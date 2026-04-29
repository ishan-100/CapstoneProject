using System.Collections.Generic;
using BikeDealerMgmtAPI.Models;

namespace BikeDealerMgmtAPI.Services.Interfaces
{
    public interface IDealerMasterService
    {
        int AddDM(DealerMaster dealerMaster);
        List<DealerMaster> GetDMs();
        DealerMaster FindDMById(int id);
        DealerMaster FindDMName(string name);
        int UpdateDM(int id, DealerMaster dealerMaster);
        int DeleteDM(int id);
    }
}
