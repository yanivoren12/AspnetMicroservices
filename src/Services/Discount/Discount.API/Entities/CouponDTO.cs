using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.API.Entities
{
    public class CouponDTO
    {
        [Column("productname")]
        public string ProductName { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("amount")]
        public int Amount { get; set; }
    }
}
