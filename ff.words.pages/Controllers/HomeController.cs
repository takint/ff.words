﻿namespace ff.words.pages.Controllers
{
    using ff.words.application.Interfaces;
    using ff.words.application.ViewModels;
    using ff.words.pages.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeController : BaseController
    {
        private readonly IEntryService _entryService;

        public HomeController(IEntryService entryService)
        {
            _entryService = entryService;
        }
        
        public async Task<IActionResult> Index()
        {
            HomeModel vm = new HomeModel();
            vm.ListEntries =  await _entryService.GetAllAsync<EntryViewModel>();
            vm.ListEntries = vm.ListEntries.OrderByDescending(e => e.CreatedDate);

            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
