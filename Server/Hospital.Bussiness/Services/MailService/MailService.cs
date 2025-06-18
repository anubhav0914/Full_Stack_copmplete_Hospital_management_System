using Hospital.Business.DTOs;
using Hpospital.Bussiness.Services.MailServices;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Hospital.Bussiness.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task<bool> SendEmailDoctorAsync(string toEmail, string password, string name)
        {
            try
            {

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Thanks for Joining Us";

                var builder = new BodyBuilder
                {
                    HtmlBody = $@"
                        <p>Dear Dr. {name},</p>
                        <p>Welcome to our hospital team! We are excited to have you onboard.</p>
                        <p>Your login credentials for the Hospital Management System are as follows:</p>
                        <ul>
                            <li><strong>Email:</strong> {toEmail}</li>
                            <li><strong>Password:</strong> {password}</li>
                        </ul>
                        <p>Please use these credentials to log in at <a href='http://your-hospital-website.com'>our portal</a>.</p>
                        <p>Make sure to change your password after your first login for security reasons.</p>
                        <br />
                        <p><strong>– Hospital Management Team</strong></p>"
                };


                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
                return false;
            }
        }

        public async Task<bool> SendEmailEmployeeAsync(string toEmail, string password, string name, string jobRole)
        {
            try
            {

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Thanks for Joining Us";

                var builder = new BodyBuilder
                {
                    HtmlBody = $@"
                        <p>Dear {name},</p>
                        <p>Welcome to our hospital team! We are glad to have you join us as a <strong>{jobRole}</strong>.</p>
                        <p>Your login credentials for the Hospital Management System are as follows:</p>
                        <ul>
                            <li><strong>Email:</strong> {toEmail}</li>
                            <li><strong>Password:</strong> {password}</li>
                        </ul>
                        <p>Please use these credentials to log in at <a href='http://your-hospital-website.com'>our portal</a>.</p>
                        <p>Make sure to change your password after your first login for security purposes.</p>
                        <br />
                        <p><strong>– Hospital Management Team</strong></p>"
                };


                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
                return false;
            }
        }



        public async Task<bool> SendEmailPatientAsync(string toEmail, string name)
        {
            try
            {

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Thanks for Contact  Us";

                var builder = new BodyBuilder
                {
                    HtmlBody = $@"
                                <p>Dear {name},</p>
                                <p>Thank you for choosing our hospital for your recent treatment. We hope you had a comfortable experience and that you're feeling better now.</p>
                                <p>Our team is always here to provide you with the best possible care and support. If you have any questions or need further assistance, feel free to contact us at any time.</p>
                                <p>Wishing you a speedy recovery and continued good health!</p>
                                <br />
                                <p><strong>– Hospital Management Team</strong></p>"
                };


                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
                return false;
            }
        }


        public async Task<bool> SendOTPtoAdminAsync(string toEmail, string name, string otp)
        {
             try
            {

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Mail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = "Admin Login";

                var builder = new BodyBuilder
                {
                    HtmlBody = $@"
                                <p>Dear {name},</p>
                                <p>As you are Admin . There is a two step verification for login. Your Otp is </p>
                                <h1> {otp}</h1>
                                <br />
                                <p><strong>– Hospital Management Team</strong></p>"
                };


                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
            catch (Exception err)
            {

                Console.WriteLine(err);
                return false;
            }
        }
       
    }


}
