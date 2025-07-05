using Application.Abstractions;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendForgotPasswordEmail(string toEmail,string token)
        {
            var subject = "Reset Password";
            var body = $@"
            <!DOCTYPE html>
            <html>
            <head>
                <style>
                    .container {{
                        font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                        max-width: 600px;
                        margin: auto;
                        padding: 20px;
                        border: 1px solid #eaeaea;
                        border-radius: 10px;
                        background-color: #ffffff;
                        color: #333333;
                    }}
                    .header {{
                        text-align: center;
                        padding-bottom: 10px;
                    }}
                    .token {{
                        display: inline-block;
                        margin: 20px 0;
                        padding: 15px 25px;
                        background-color: #f0f0f0;
                        font-size: 18px;
                        font-weight: bold;
                        color: #007BFF;
                        border-radius: 5px;
                    }}
                    .footer {{
                        font-size: 12px;
                        color: #888888;
                        margin-top: 30px;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <div class='header'>
                        <h2>🔐 Reset Your Password</h2>
                    </div>

                    <p>Hello,</p>

                    <p>We received a request to reset the password associated with your account.</p>

                    <p>Please use the following token to reset your password:</p>

                    <div class='token'>{token}</div>

                    <p>This token is valid for the next <strong>5 minutes</strong>. If you didn't request a password reset, you can safely ignore this email.</p>

                    <p>Need help? Just reply to this email and our support team will assist you.</p>

                    <p>Cheers,<br><strong>Support Team</strong></p>

                    <div class='footer'>
                        <p>You're receiving this email because you requested a password reset. If you did not, please disregard this message.</p>
                    </div>
                </div>
            </body>
            </html>";

            var fromEmail = "rehanboyaju@gmail.com";
            var password = "xryl bniy xhjl oaeb";

            var message = new MailMessage();
            message.To.Add(toEmail);
            message.Subject = subject;
            message.Body = body;
            message.From = new MailAddress(fromEmail);
            message.IsBodyHtml = true;

            using var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail,password),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);

        }
    }
}
