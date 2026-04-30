using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeDealerMgmtAPI.Models
{
    [Table("DealerMasters")]   
    public class DealerMaster
    {
        [Key]
        public int DealerMasterId { get; set; }
        public int DealerId { get; set; }
        public int BikeId { get; set; }
        public int BikesDelivered { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
