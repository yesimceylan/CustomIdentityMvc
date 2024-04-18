namespace Reservation.mvcproject.Interfaceses
{
    public interface IMailService
    {
        Task SendEmailAsync(string toMail, string subject, string body);
    }
}
