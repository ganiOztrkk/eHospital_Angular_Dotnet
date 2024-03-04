using GenericEmailService;

namespace E_HospitalServer.Business.Services;

public static class EmailHelper
{
    public static async Task<string> SendEmailAsync(string email, string subject, string body)
    {
        EmailConfigurations emailConfigurations = new(
            "smtp-mail.outlook.com",
            "148951753Gg.",
            587,
            false,
            true);

        EmailModel<Stream> emailModel = new(
            emailConfigurations,
            "gani.developer@hotmail.com",
            new List<string> { email },
            subject,
            body,
            null);


        string emailSendResponse = await EmailService.SendEmailWithMailKitAsync(emailModel);

        return emailSendResponse;
    }
}