namespace Application.Interfaces.Services
{
    public interface IPatientService
    {
        public Task<int> GetCountOfPatients(string token);
    }
}
