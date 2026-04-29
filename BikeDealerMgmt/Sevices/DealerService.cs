using System.Collections.Generic;
using System.Linq;
using BikeDealerMgmtAPI.Models;
using BikeDealerMgmtAPI.Services.Interfaces;

namespace BikeDealerMgmtAPI.Services.Implementations
{
    public class DealerService : IDealerService
    {
        private readonly BikeDealerDbContext _context;

        public DealerService(BikeDealerDbContext context)
        {
            _context = context;
        }

        public int AddDealer(Dealer dealer)
        {
            _context.Dealers.Add(dealer);
            return _context.SaveChanges();
        }

        public List<Dealer> GetDealers()
        {
            return _context.Dealers.ToList();
        }

        public Dealer FindDealerById(int id)
        {
            return _context.Dealers.Find(id);
        }

        public Dealer FindDealerByName(string name)
        {
            return _context.Dealers
                .FirstOrDefault(d => d.DealerName == name);
        }

        public int UpdateDealer(int id, Dealer dealer)
        {
            var existingDealer = _context.Dealers.Find(id);
            if (existingDealer == null)
                return 0;

            existingDealer.DealerName = dealer.DealerName;
            existingDealer.Address = dealer.Address;
            existingDealer.City = dealer.City;
            existingDealer.State = dealer.State;
            existingDealer.ZipCode = dealer.ZipCode;
            existingDealer.StorageCapacity = dealer.StorageCapacity;
            existingDealer.Inventory = dealer.Inventory;

            return _context.SaveChanges();
        }

        public int DeleteDealer(int id)
        {
            var dealer = _context.Dealers.Find(id);
            if (dealer == null)
                return 0;

            _context.Dealers.Remove(dealer);
            return _context.SaveChanges();
        }
    }
}
