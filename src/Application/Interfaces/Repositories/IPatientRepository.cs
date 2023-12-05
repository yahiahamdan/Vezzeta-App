namespace Application.Interfaces.Repositories
{
    public interface IPatientRepository
    {
        public Task<int> GetCountOfPatients(string roleId);
    }
}
