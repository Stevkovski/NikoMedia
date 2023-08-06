using DataAccess.Entities;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NikoMedia.Models;
using Services;
using Services.Models;
using Services.Queueservice;
using Services.XMLParser;
using System.Diagnostics;

namespace NikoMedia.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQueueService _queueService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IXMLParseService _xMLParseService;

        public HomeController(IQueueService queueService, IUnitOfWork unitOfWork, IXMLParseService xMLParseService)
        {
            _queueService = queueService;
            _unitOfWork = unitOfWork;
            _xMLParseService = xMLParseService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            _unitOfWork.Client.Add(client);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home"); // Redirect to the home page after successful creation

            return View(client);
        }

        public IActionResult CreateTemplate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTemplate(Template template)
        {

            _unitOfWork.Template.Add(template);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home");

        }

        public IActionResult RegisterConfiguration()
        {
            // Get the list of clients and templates from the database
            List<Client> clients = _unitOfWork.Client.GetAll().ToList();
            List<Template> templates = _unitOfWork.Template.GetAll().ToList();

            // Create dropdown lists for selecting a client and a template
            SelectList clientList = new SelectList(clients, "Id", "Name");
            SelectList templateList = new SelectList(templates, "Id", "Subject");

            ViewBag.ClientList = clientList;
            ViewBag.TemplateList = templateList;

            return View();
        }

        [HttpPost]
        public IActionResult RegisterConfiguration(Configuration configuration)
        {

            // Find the selected client and template by their IDs
            Client selectedClient = _unitOfWork.Client.GetById(configuration.Client.Id);
            Template selectedTemplate = _unitOfWork.Template.GetById(configuration.Template.Id);

            // Associate the selected client and template with the new configuration
            configuration.Client = selectedClient;
            configuration.Template = selectedTemplate;

            _unitOfWork.Configuration.Add(configuration);
            _unitOfWork.Save();

            return RedirectToAction("Index", "Home"); // Redirect to the home page after successful creation
        }

        public IActionResult RenderConfigurations()
        {
            var configurations = _unitOfWork.Configuration.GetAll().ToList();
            var viewModel = new RenderConfigurationsViewModel
            {
                Configurations = configurations
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult SendEmail(RenderConfigurationsViewModel viewModel)
        {
            var config = _unitOfWork.Configuration.GetConfigurationWithRelatedDataById(viewModel.SelectedConfigurationId);

            var sendMessage = new SendMessageModel
            {
                SendTo = viewModel.SendTo,
                Subject = config.Template.Subject,
                Body = config.Template.Body
            };

            _queueService.SendMessageAsync(sendMessage, "emailqueue");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult SendXmlEmail()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendXmlEmail(string xmlBody)
        {
            var xmlData = _xMLParseService.ParseXml(xmlBody);

            foreach (var item in xmlData)
            {
                var template = _unitOfWork.Template.GetById(item.TemplateId);

                var sendMessage = new SendMessageModel
                {
                    //SendTo = viewModel.SendTo, //OVDEKA BI SE ZEMALA EMAIL ADRESSATA OD CLIENT,NO VEKJE MI E NAPAVENA LOGIkATA DA SE VNESUVA EMAIL ADRESA DOPOLNITELNO.. Mozeme da go diskutirame ova, za test imam saveno moj email adresa..
                    SendTo = "niki_orce@yahoo.com",
                    Subject = template.Subject,
                    Body = item.MarketingData
                };

                _queueService.SendMessageAsync(sendMessage, "emailqueue");
            }

            return View();
        }

    }
}