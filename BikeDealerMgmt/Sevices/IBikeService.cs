using BikeDealerMgmtAPI.Models;

namespace BikeDealerMgmtAPI.Sevices
{
    public interface IBikeService
    {
        int AddBike(Bike bike);
        int UpdateBike(int id, Bike bike);
        int DeleteBike(int id);
        List<Bike> GetBikes();
        Bike FindBikeById(int id);
        Bike FindBikeByName(string name);
    }
}
