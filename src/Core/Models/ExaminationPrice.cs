namespace Core.Models
{
    public class ExaminationPrice
    {
        public string Price { get; set; }
        public string DoctorId { get; set; }
        public ApplicationUser Doctor { get; set; }
    }
}
