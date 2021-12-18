using Application.Configurations;
using Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using SharedR.Requests;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly MailConfiguration _config;

        public MailService(IOptions<MailConfiguration> config)
        {
            _config = config.Value;
        }

        public async Task SendAsync(MailRequest request)
        {
            try
            {
                var email = new MimeMessage
                {
                    Sender = new MailboxAddress(_config.DisplayName, request.From ?? _config.From),
                    Subject = request.Subject,
                    Body = new BodyBuilder
                    {
                        HtmlBody = request.Body
                    }.ToMessageBody()
                };
                email.To.Add(MailboxAddress.Parse(request.To));
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.UserName, _config.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex);
            }
        }

        public async Task SendWelcomeEmailAsync(MailRequest request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\Emails\\Welcome.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", request.FirstName).Replace("[email]", request.To).Replace("[regUrl]", request.Body);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_config.From);
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = $"Welcome {request.FirstName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_config.UserName, _config.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
