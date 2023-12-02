namespace Core.Models
{
    public class ExaminationPrice
    {
        public int Price { get; set; }
        public string DoctorId { get; set; }
        public ApplicationUser Doctor { get; set; }
    }
}
