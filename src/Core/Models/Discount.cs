namespace Core.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public bool IsActivated { get; set; }
        public int DiscountValue { get; set; }
        public Booking Booking { get; set; }
        public DiscountType DiscountType { get; set; }
        public int DiscountTypeId { get; set; }
    }
}
