namespace Core.Models
{
    public class Specialization
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<ApplicationUser> Doctors { get; set; }
    }
}
