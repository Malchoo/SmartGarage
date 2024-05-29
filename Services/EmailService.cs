//using System.Net.Mail;
//using SendGrid.Helpers.Mail;
//using SendGrid;

//public class EmailService : IEmailService
//{
//    private readonly SendGridClient _sendGridClient;

//    public EmailService(IConfiguration configuration)
//    {
//        string apiKey = configuration["SendGrid:ApiKey"];
//        _sendGridClient = new SendGridClient(apiKey);
//    }

//    public async Task SendEmailAsync(string to, string subject, string body)
//    {
//        var from = new EmailAddress("noreply@smartgarage.com", "SmartGarage");
//        var toEmail = new EmailAddress(to);
//        var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, body, body);
//        await _sendGridClient.SendEmailAsync(msg);
//    }
//}