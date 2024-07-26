
using MailKit.Net.Smtp;
using BlogPlatform.Data.DTOs.AuthenticateUser;
using MimeKit;

namespace BlogPlatform.Services.SendEmailService
{
    public class SendEmailService : ISendEmailService
    {
        private readonly EmailConfiguration _emailConfig;
        public SendEmailService(EmailConfiguration _emailConfig)
        {
            this._emailConfig = _emailConfig;
        }
        public string SendEmail(MessageDto message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
            //var recipients = string.Join(", ", message.To);
            return $"Email sent successfully to {message.To}";
        }

        private MimeMessage CreateEmailMessage(MessageDto message)
        {
            MimeMessage emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", _emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }

        private void Send(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfig.SmtpServer, _emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                client.Send(message);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}