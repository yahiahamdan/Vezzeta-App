namespace Core.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Price { get; set; }
        public int FinalPrice { get; set; }
        public int StatusId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public Discount Discount { get; set; }
        public int? DiscountId { get; set; }
        public AppointmentTime AppointmentTime { get; set; }
        public int AppointmentTimeId { get; set; }
        public ApplicationUser Patient { get; set; }
        public string PatientId { get; set; }
    }
}
