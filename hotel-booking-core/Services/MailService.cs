﻿using hotel_booking_core.Interface;
using hotel_booking_models.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace hotel_booking_core.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
<<<<<<< HEAD
        
        public MailService(IOptions<MailSettings> mailSettings)
=======
        private readonly ILogger<MailService> _logger;
        public MailService(IOptions<MailSettings> mailSettings, ILogger<MailService> logger)
>>>>>>> 5719c972d0d847c0b993ef13ece4225f9941d3e9
        {
            _logger = logger;
            _mailSettings = mailSettings.Value;
        }


        public async Task<bool> SendEmailAsync(MailRequest mailRequest)
        {
<<<<<<< HEAD
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                if (mailRequest.Attachments != null)
                {
                    byte[] fileBytes;
                    foreach (var file in mailRequest.Attachments)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                file.CopyTo(ms);
                                fileBytes = ms.ToArray();
                            }
                            builder.Attachments.Add((file.FileName + Guid.NewGuid().ToString()), fileBytes, ContentType.Parse(file.ContentType));
                        }
=======
            var email = new MimeMessage {Sender = MailboxAddress.Parse(_mailSettings.Mail)};
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                foreach (var file in mailRequest.Attachments.Where(file => file.Length > 0))
                {
                    byte[] fileBytes;
                    await using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        fileBytes = ms.ToArray();
>>>>>>> 5719c972d0d847c0b993ef13ece4225f9941d3e9
                    }
                    builder.Attachments.Add((file.FileName + Guid.NewGuid().ToString()), fileBytes, ContentType.Parse(file.ContentType));
                }
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;

            }
<<<<<<< HEAD
            catch (Exception)
            {

                return false;
            }                
          
=======
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            try
            {
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Source, e.InnerException, e.Message, e.ToString());
                throw;
            }
            
>>>>>>> 5719c972d0d847c0b993ef13ece4225f9941d3e9
        }
    }
}
