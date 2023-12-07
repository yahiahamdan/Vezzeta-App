namespace Application.Interfaces.Helpers
{
    public interface IEmailHelperService
    {
        public void SendEmail(string to, string subject, string body);
    }
}
