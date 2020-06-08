using Microsoft.Extensions.Logging;
using Mvc5WithCoreDI.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mvc5WithCoreDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HomeController> _logger;
        private readonly IHelloService _helloService;

        public HomeController(IHttpClientFactory httpClientFactory,ILogger<HomeController> logger,IHelloService helloService)
        {
            _httpClient = httpClientFactory.CreateClient();
            _logger = logger;
            _helloService = helloService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            var result = await _httpClient.GetStringAsync("https://dongsu.dev");
            _logger.LogDebug(result);

            ViewBag.Message = result;

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = _helloService.SayHello();

            return View();
        }
    }
}