namespace Application.Interfaces.Repositories
{
    public interface IDoctorRepository
    {
        public Task<int> GetCountOfDoctors(string roleName);
        public Task<object> GetDoctorById(string doctorId);
    }
}
