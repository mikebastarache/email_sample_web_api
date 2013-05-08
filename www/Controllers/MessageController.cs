using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Text;
using System.Resources;
using System.Net;
using System.Globalization;
using Newtonsoft.Json;
using EmailWebApi.Models;
using System.Web.Http.Filters;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;

namespace EmailWebApi.Controllers
{

    public class MessageController : ApiController
    {
        public string EmailHeader { get; set; }
        public string EmailFooter { get; set; }
        public string EmailBody { get; set; }
        public string EmailContent { get; set; }
        public string EmailSubjectLine { get; set; }
        public string CrmUrl { get; set; }
        public string CrmEmail { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public string SiteSource { get; set; }
        public string CustomBody { get; set; }
        public string CustomSubjectLine { get; set; }
        public string CustomHeader { get; set; }
        public string CustomFooter { get; set; }

        [HttpGet]
        [ActionName("Default")]
        public string Get()
        {
            return "This is an email api service";
        }

        [HttpPost]
        [ActionName("Custom")]
        public HttpResponseMessage EmailCustom(JObject ClientEmail)
        {

            var response = this.Request.CreateResponse();

            Email = ClientEmail["Email"].ToString();
            Language = ClientEmail["Language"].ToString();
            SiteSource = ClientEmail["SiteSource"].ToString();
            CustomSubjectLine = ClientEmail["CustomSubjectLine"].ToString();
            CustomBody = ClientEmail["CustomBody"].ToString();

            try
            {
                CustomHeader = ClientEmail["CustomHeader"].ToString();
            }
            catch
            {
                CustomHeader = "";
            }

            try
            {
                CustomFooter = ClientEmail["CustomFooter"].ToString();
            }
            catch
            {
                CustomFooter = "";
            }
  

            if (EmailSend(Email, Language, SiteSource, CustomSubjectLine, CustomBody, CustomHeader, CustomFooter))
            {
                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }


        [HttpPost]
        [ActionName("Welcome")]
        public HttpResponseMessage EmailWelcome(JObject ClientEmail)
        {
            var response = this.Request.CreateResponse();

            Email = ClientEmail["Email"].ToString();
            Language = ClientEmail["Language"].ToString();
            SiteSource = ClientEmail["SiteSource"].ToString();

            if(EmailSend(Email, Language, SiteSource, "", "", "", "")){
                response.StatusCode = HttpStatusCode.OK;
            }  else {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return response;
        }




        private bool EmailSend(string toEmail, string language, string siteSource, string subjectLine, string body, string header, string footer)
        {
            bool response = true;

            try
            {
                //Set Email Language
                CultureInfo culture = new CultureInfo(language);
                System.Threading.Thread.CurrentThread.CurrentCulture = culture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

                //Set ROYALE PROPERTIES
                string RoyaleHeader = Resources.email.RoyaleHeader;
                string RoyaleBody = Resources.email.RoyaleBody;
                string RoyaleFooter = Resources.email.RoyaleFooter;
                string RoyaleSubjectLine = Resources.email.RoyaleSubjectLine;
                string RoyaleUrl = Resources.email.RoyaleUrl;
                string RoyaleEmail = Resources.email.RoyaleEmail;

                //Set MAJESTA PROPERTIES
                string MajestaHeader = Resources.email.MajestaHeader;
                string MajestaBody = Resources.email.MajestaBody;
                string MajestaFooter = Resources.email.MajestaFooter;
                string MajestaSubjectLine = Resources.email.MajestaSubjectLine;
                string MajestaUrl = Resources.email.MajestaUrl;
                string MajestaEmail = Resources.email.MajestaEmail;

                //Set SCOTTIES PROPERTIES
                string ScottiesHeader = Resources.email.ScottiesHeader;
                string ScottiesBody = Resources.email.ScottiesBody;
                string ScottiesFooter = Resources.email.ScottiesFooter;
                string ScottiesSubjectLine = Resources.email.ScottiesSubjectLine;
                string ScottiesUrl = Resources.email.ScottiesUrl;
                string ScottiesEmail = Resources.email.ScottiesEmail;

                //Set email properties to proper site source
                switch (siteSource)
                {
                    case "royale":
                        EmailHeader = RoyaleHeader;
                        EmailFooter = RoyaleFooter;
                        EmailBody = RoyaleBody;
                        EmailSubjectLine = RoyaleSubjectLine;
                        CrmEmail = RoyaleEmail;
                        CrmUrl = RoyaleUrl;
                        break;

                    case "majesta":
                        EmailHeader = MajestaHeader;
                        EmailFooter = MajestaFooter;
                        EmailBody = MajestaBody;
                        EmailSubjectLine = MajestaSubjectLine;
                        CrmEmail = MajestaEmail;
                        CrmUrl = MajestaUrl;
                        break;

                    case "scotties":
                        EmailHeader = ScottiesHeader;
                        EmailFooter = ScottiesFooter;
                        EmailBody = ScottiesBody;
                        EmailSubjectLine = ScottiesSubjectLine;
                        CrmUrl = ScottiesUrl;
                        CrmEmail = ScottiesEmail;
                        break;

                    default:
                        EmailHeader = RoyaleHeader;
                        EmailFooter = RoyaleFooter;
                        EmailBody = RoyaleBody;
                        EmailSubjectLine = RoyaleSubjectLine;
                        CrmEmail = RoyaleEmail;
                        CrmUrl = RoyaleUrl;
                        break;
                }

                //CHANGE EMAIL TEMPLATE IF NEW DESIGN IS PROVIDED
                if (header.Trim() != "")
                    EmailHeader = header;

                if (footer.Trim() != "")
                    EmailFooter = footer;
                
                //REPLACE SUBJECT LINE AND BODY IF PARAMATERS ARE NOT NULL
                if (subjectLine.Trim() != "")
                    EmailSubjectLine = subjectLine;

                if (body.Trim() != "")
                    EmailBody = body;

                //BUILD WELCOME EMAIL TO COMMUNITY
                EmailContent = EmailHeader;
                EmailContent += EmailBody;
                EmailContent += EmailFooter;
                EmailContent = EmailContent.Replace("[[ServerURL]]", CrmUrl);
                EmailContent = EmailContent.Replace("[[uemail]]", Email);

                //SEND WELCOME EMAIL TO NEW USER
                Email email = new Email();
                email.smtpServer = Properties.Settings.Default.SmtpServer;
                email.smtpPort = Properties.Settings.Default.SmtpPort;
                email.MailFrom = CrmEmail;
                email.MailTo = toEmail;
                email.MailSubject = EmailSubjectLine;
                email.MailBody = EmailContent;
                email.Send();

            }
            catch (InvalidCastException)
            {
                response = false;
            }

            return response;
        }

    }
}