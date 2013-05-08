using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft;
using System.Net.Http.Formatting;
using EmailWebApi.Models;
using System.IO;

namespace EmailWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            /*
            var serverAddress = Properties.Settings.Default.WebApiUri;
            serverAddress = "http://royalepromotions.ca";

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(serverAddress);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                // Create a new product
                //ClientEmail userObject = new ClientEmail() { Email = "mike@digitalmagic.ca", Language = "en-CA", SiteSource = "scotties" };
                //ClientEmail userObject = new ClientEmail() { Email = "mbastarache@modernmedia.ca", Language = "en-CA", SiteSource = "royale", CustomSubjectLine = "Hello world!", CustomBody = "<h1>test</h1>" };
                ClientEmail userObject = new ClientEmail() { Email = "mbastarache@modernmedia.ca", Language = "en-CA", SiteSource = "royale", CustomSubjectLine = "C# Hello world!", CustomBody = "<h1>test</h1>", CustomHeader = "<body bgcolor=red><h1>This is the header</h1>", CustomFooter = "<hr>thi sis the footer</body>" };

                // Create the JSON formatter.
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();

                // Use the JSON formatter to create the content of the request body.
                var content = new ObjectContent<ClientEmail>(userObject, jsonFormatter);

                Uri address = new Uri(client.BaseAddress, "/email/api/message/custom/");
                HttpResponseMessage resp = client.PostAsync(address.ToString(), content).Result;

                if (resp.IsSuccessStatusCode)
                {
                    Response.Write(resp.StatusCode + "<br>" + resp.ReasonPhrase + "<br>YES!!!!");
                }
                else
                {
                    Response.Write(resp.StatusCode + "<br><br>" + resp.ReasonPhrase + "<br><br>" + resp.RequestMessage + "<br><br>" + resp.Headers + "<br><br>" + resp.Content);
                }

            }
            catch (Exception exc)
            {
                Response.Write(exc.Message);
            }
            
        */
            return View();
        }

    }
}
