using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SimpleDeveloperCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleDeveloperCore.IServices
{
    public interface IExceptionLogging
    {
        Task<Response> SendErrorToMailAsync(Exception ex);
        Response SendErrorToMail(Exception ex);
        Response SendMail(SendGridMessage msg);
        (SendGridMessage, string) SetSendGridMessage(string errortomail);
    }
    public class ExceptionLogging : IExceptionLogging
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailOptions _emailOptions;
        private string ErrorlineNo, Errormsg, ErrorLocation, extype, exurl, Frommail, Password, ToMail, Sub, HostAdd, EmailHead, EmailSing;

        public ExceptionLogging(IHttpContextAccessor httpContextAccessor, IOptions<EmailOptions> emailOptions)
        {
            _httpContextAccessor = httpContextAccessor;
            _emailOptions = emailOptions.Value;
        }

        public Response SendMail(SendGridMessage msg)
        {
            if (msg == null) throw new ArgumentNullException(nameof(msg));
            try
            {
                var client = CreateClient();
                return client.SendEmailAsync(msg).Result;
            }
            catch (Exception em)
            {
                em.ToString();
            }
            throw new Exception("something went wrong.");
        }

        public Response SendErrorToMail(Exception exmail)
        {
            if (exmail == null) throw new ArgumentNullException(nameof(exmail));
            try
            {
                var (client, msg) = CreateClient(exmail);
                return client.SendEmailAsync(msg).Result;
            }
            catch (Exception em)
            {
                em.ToString();
            }
            throw new Exception("something went wrong.");
        }
        public async Task<Response> SendErrorToMailAsync(Exception exmail)
        {
            if (exmail == null) throw new ArgumentNullException(nameof(exmail));
            try
            {
                var (client, msg) = CreateClient(exmail);
                return await client.SendEmailAsync(msg);
            }
            catch (Exception em)
            {
                em.ToString();
            }
            throw new Exception("something went wrong.");
        }

        private (SendGridClient, SendGridMessage) CreateClient(Exception ex)
        {
            string errortomail = ParseEmailValues(ex);
            var (msg, apiKey) = SetSendGridMessage(errortomail);
            SendGridClient client = new SendGridClient(apiKey);
            return (client, msg);
        }

        private SendGridClient CreateClient()
        {

            SendGridClient client = new SendGridClient(_emailOptions.ApiKey);
            return client;
        }

        public (SendGridMessage, string) SetSendGridMessage(string errortomail)
        {
            var apiKey = _emailOptions.ApiKey;
            var from = new EmailAddress(_emailOptions.FromMail);
            var subject = Sub;
            var to = new EmailAddress(_emailOptions.ToMail);
            var htmlContent = errortomail;
            SendGridMessage msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            return (msg, apiKey);
        }

        private string ParseEmailValues(Exception exmail)
        {
            var newline = "<br/>";
            ErrorlineNo = exmail?.StackTrace?.Substring(exmail.StackTrace.Length - 7, 7) ?? "-- stacktrace was null --";
            Errormsg = exmail!.GetType().Name.ToString();
            extype = exmail.GetType().ToString();
            exurl = _httpContextAccessor?.HttpContext?.Request?.GetDisplayUrl() ?? "Not able to get the request URL.";
            ErrorLocation = exmail.Message.ToString();
            EmailHead = "<b>Dear Team,</b>" + "<br/>" + "An exception occurred in a Application Url" + " " + exurl + " " + "With following Details" + "<br/>" + "<br/>";
            EmailSing = newline + "Thanks and Regards" + newline + "    " + "     " + "<b>Application Admin </b>" + "</br>";
            Sub = "Exception occurred" + " " + "in Application" + " " + exurl;
            HostAdd = _emailOptions.Host;
            var details = exmail.ToString();
            string errortomail = EmailHead + "<b>Log Written Date: </b>" + " " + DateTime.Now.ToString() + newline + "<b>Error Line No :</b>" + " " + ErrorlineNo + "\t\n" + " " + newline + "<b>Error Message:</b>" + " " + Errormsg + newline + "<b>Exception Type:</b>" + " " + extype + newline + "<b> Error Details :</b>" + " " + ErrorLocation + newline + "<b>Error Page Url:</b>" + " " + exurl + newline + "<b>Error stack:</b>" + "<code>" + details + "</code>" + newline + newline + newline + newline + EmailSing;
            return errortomail;
        }
    }
}
