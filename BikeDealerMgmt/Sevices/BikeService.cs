using BikeDealerMgmtAPI.Models;

namespace BikeDealerMgmtAPI.Sevices
{
    public class BikeService : IBikeService
    {
        private readonly BikeDealerDbContext _context;

        public BikeService(BikeDealerDbContext context)
        {
            _context = context;
        }

        public int AddBike(Bike bike)
        {
            _context.BikeStores.Add(bike);
            return _context.SaveChanges();
        }

        public List<Bike> GetBikes()
        {
            return _context.BikeStores.ToList();
        }

        public Bike FindBikeById(int id)
        {
            return _context.BikeStores.Find(id);
        }

        public Bike FindBikeByName(string name)
        {
            return _context.BikeStores.FirstOrDefault(b => b.ModelName == name);
        }

        public int UpdateBike(int id, Bike bike)
        {
            var existing = _context.BikeStores.Find(id);
            if (existing == null) return 0;

            existing.ModelName = bike.ModelName;
            existing.EngineCC = bike.EngineCC;
            existing.ModelYear = bike.ModelYear;
            existing.Manufacturer = bike.Manufacturer;

            return _context.SaveChanges();
        }

        public int DeleteBike(int id)
        {
            var bike = _context.BikeStores.Find(id);
            if (bike == null) return 0;
            _context.BikeStores.Remove(bike);
            return _context.SaveChanges();
        }
    }
}
