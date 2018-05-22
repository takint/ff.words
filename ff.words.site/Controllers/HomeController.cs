using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ff.words.sites.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var vm = AppViewModelBuilder();
            return View(vm);
        }

        public IActionResult Login()
        {
            return View();
        }

        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            TokenClient tokenClient = new TokenClient("http://localhost:23465/connect/token", "client", "secrect");
            TokenResponse tokenResponse = await tokenClient.RequestClientCredentialsAsync("api");

            HttpClient client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            string content = await client.GetStringAsync("http://localhost:52707/api/Entry/GetEntries");

            ViewBag.Json = JArray.Parse(content).ToString();

            return View("Login");
        }

        public async Task<IActionResult> GetUserAccessToken()
        {
            TokenClient tokenClient = new TokenClient($"{_configuration["AuthServiceUrl"]}/connect/token", "client", "secrect");
            TokenResponse tokenResponse = await tokenClient.RequestClientCredentialsAsync("api");

            return Json(tokenResponse.AccessToken);
        }

        public async Task<IActionResult> CallApiUsingClientCredentials()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            HttpClient client = new HttpClient();

            client.SetBearerToken(accessToken);
            string content = await client.GetStringAsync("http://localhost:52707/Identity");

            ViewBag.Json = JArray.Parse(content).ToString();

            return View("Login");
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        private AppViewModel AppViewModelBuilder()
        {
            var model = new AngularAppViewModel();

            model.ApiEndpoint = _configuration["ApiEndpointUrl"];
            model.AuthServiceUrl = _configuration["AuthServiceUrl"];

            return model;
        }
    }

    public abstract class AppViewModel
    { }

    public class AngularAppViewModel : AppViewModel
    {
        public string ApiEndpoint { get; set; }

        public string AuthServiceUrl { get; set; }
    }

    public class ReactAppViewModel : AppViewModel
    {

    }
}
