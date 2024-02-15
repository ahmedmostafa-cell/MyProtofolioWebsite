using AspNetCore.SEOHelper.Sitemap;
using AspNetCoreHero.ToastNotification.Abstractions;
using EmailService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProtofolio.Controllers
{
    public class HomeController : Controller
    {
        IEmailSender _emailSender;
        private readonly INotyfService _notyf;
        private readonly IWebHostEnvironment _env;
        public HomeController(IEmailSender emailSender , INotyfService notyf , IWebHostEnvironment env)
        {

            _emailSender = emailSender;
            _notyf = notyf;
            _env = env;
        }

        public async Task<IActionResult> IndexAsync(string name, string email, string message, IFormCollection form, IFormFileCollection files)
        {
            try
            {
                if (name != null)
                {
                    var userEmail = email;

                    var messages = new Message(new string[] { "ahmedmostafa706@gmail.com" }, "Email From Customer " + email, "This is the content from our async email. i am happy", files);
                    //var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                    //var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
                    await _emailSender.SendEmailAsync(messages, message);
                     _notyf.Success("The Message Has Been Sent");
                    return View();
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.ex = ex;
                return View();

            }


        }


        public async Task<IActionResult> ArabicIndexAsync(string name, string email, string message, IFormCollection form, IFormFileCollection files)
        {
            if (name != null)
            {
                var userEmail = email;

                var messages = new Message(new string[] {"ahmedmostafa706@gmail.com"}, "Email From Customer " + email, "This is the content from our async email. i am happy", files);
                //var filePaths = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Uploads", ImageName);
                //var files = Request.Form.Files.Any() ? Request.Form.Files : new FormFileCollection();
                await _emailSender.SendEmailAsync(messages, message);
                return View();
            }
            else
            {
                return View();
            }

        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult MesageSent()
        {
            _notyf.Success("The Message Has Been Sent");
            return View();
        }


        //not returning any view  
        public string CreateSitemapInRootDirectory()
        {
            var list = new List<SitemapNode>();
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://ahmedattya.com/", Frequency = SitemapFrequency.Daily });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.8, Url = "https://ahmedattya.com/", Frequency = SitemapFrequency.Yearly });
            list.Add(new SitemapNode { LastModified = DateTime.UtcNow, Priority = 0.7, Url = "https://ahmedattya.com/", Frequency = SitemapFrequency.Monthly });

            new SitemapDocument().CreateSitemapXML(list, _env.ContentRootPath);
            return "sitemap.xml file should be create in root directory";
        }



    }
}
