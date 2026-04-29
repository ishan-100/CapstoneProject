namespace BikeDealerMgmtAPI.Models
{
    public class Dealer
    {
        public int DealerId { get; set; }
        public string DealerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long ZipCode { get; set; }
        public int StorageCapacity { get; set; }
        public int Inventory { get; set; }
    }
}
