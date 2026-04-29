using Microsoft.EntityFrameworkCore;

namespace BikeDealerMgmtAPI.Models
{
    public class BikeDealerDbContext : DbContext
    {
        public BikeDealerDbContext(DbContextOptions<BikeDealerDbContext> options)
            : base(options) { }

        public DbSet<Dealer> Dealers { get; set; }
        public DbSet<Bike> BikeStores { get; set; }
        public DbSet<DealerMaster> DealerMasters { get; set; }
    }
}
