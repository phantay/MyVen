using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace SEN.Data.Utilities
{
    public static class Common
    {
        public static string ConvertToMD5(string password)
        {
            byte[] key = Encoding.UTF8.GetBytes(ConstantValues.SALT);
            byte[] input = Encoding.UTF8.GetBytes(password);
            var hMacMd5 = new HMACMD5(key);
            byte[] resultHash = hMacMd5.ComputeHash(input);
            return Convert.ToBase64String(resultHash);
        }

        public static void SendMailToNewRegister(string email)
        {
            var body = "<p>Hello : {1} ({0})</p><p>Message:</p><p>Welcome to VEN - The best Socical NetWork</p>";
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.From = new MailAddress(ConstantValues.EMAIL_FROM_US);
            mailMessage.Subject = ConstantValues.STRING_EMAIL_SUBJECT;
            mailMessage.Body = string.Format(body, email);
            mailMessage.IsBodyHtml = true;
            SmtpClient smtlClient = new SmtpClient();
            smtlClient.UseDefaultCredentials = false;
            smtlClient.Credentials = new NetworkCredential(ConstantValues.EMAIL_FROM_US, ConstantValues.PASSWORD_EMAIL_BIGSTORE);
            smtlClient.Send(mailMessage);
        }
    }
}
