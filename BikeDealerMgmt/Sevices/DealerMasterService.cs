using System.Collections.Generic;
using System.Linq;
using BikeDealerMgmtAPI.Models;
using BikeDealerMgmtAPI.Services.Interfaces;

namespace BikeDealerMgmtAPI.Services.Implementations
{
    public class DealerMasterService : IDealerMasterService
    {
        private readonly BikeDealerDbContext _context;

        public DealerMasterService(BikeDealerDbContext context)
        {
            _context = context;
        }

        public int AddDM(DealerMaster dealerMaster)
        {
            _context.DealerMasters.Add(dealerMaster);
            return _context.SaveChanges();
        }

        public List<DealerMaster> GetDMs()
        {
            return _context.DealerMasters.ToList();
        }

        public DealerMaster FindDMById(int id)
        {
            return _context.DealerMasters.Find(id);
        }

        // Example implementation (can be customized)
        public DealerMaster FindDMName(string name)
        {
            return _context.DealerMasters.FirstOrDefault();
        }

        public int UpdateDM(int id, DealerMaster dealerMaster)
        {
            var existingDM = _context.DealerMasters.Find(id);
            if (existingDM == null)
                return 0;

            existingDM.DealerId = dealerMaster.DealerId;
            existingDM.BikeId = dealerMaster.BikeId;
            existingDM.BikesDelivered = dealerMaster.BikesDelivered;
            existingDM.DeliveryDate = dealerMaster.DeliveryDate;

            return _context.SaveChanges();
        }

        public int DeleteDM(int id)
        {
            var dm = _context.DealerMasters.Find(id);
            if (dm == null)
                return 0;

            _context.DealerMasters.Remove(dm);
            return _context.SaveChanges();
        }
    }
}