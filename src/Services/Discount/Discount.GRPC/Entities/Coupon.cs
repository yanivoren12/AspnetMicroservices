using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discount.GRPC.Entities
{
    [Table("coupon")]
    public class Coupon : CouponDTO
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
    }
}
