using ApplicationCore.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                // Validar formato del correo del destinatario
                if (!MailAddress.TryCreate(toEmail, out _))
                {
                    Console.WriteLine($"Dirección de correo no válida: {toEmail}");
                    return false;
                }

                // Configura el cliente SMTP
                var smtpClient = new SmtpClient(_configuration["EmailSettings:SMTPServer"])
                {
                    Port = int.Parse(_configuration["EmailSettings:Port"]),
                    Credentials = new NetworkCredential(
                        _configuration["EmailSettings:Email"],
                        _configuration["EmailSettings:Password"]),
                    EnableSsl = true
                };

                // Configura el mensaje
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_configuration["EmailSettings:Email"]),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };
                mailMessage.To.Add(toEmail);

                // Enviar el correo
                await smtpClient.SendMailAsync(mailMessage);
                Console.WriteLine("Correo enviado correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }
}
