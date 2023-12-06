namespace Application.Interfaces.Services
{
    public interface IDoctorService
    {
        public Task<int> GetCountOfDoctors(string roleName);
    }
}
