namespace Core.Models
{
    public class UserBookingTracking
    {
        public string PatientId { get; set; }
        public ApplicationUser Patient { get; set; }
        public int ApprovedBookingCount { get; set; }
    }
}
