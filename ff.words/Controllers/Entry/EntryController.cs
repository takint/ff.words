namespace ff.words.Controllers.Entry
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using ff.words.application.Interfaces;
    using ff.words.application.ViewModels;
    using ff.words.application.Common;


    [Route("api/[controller]")]
    public class EntryController : BaseController
    {
        private readonly IEntryService _entryService;

        public EntryController(IEntryService entryService)
        {
            _entryService = entryService;
        }

        [HttpGet]
        [Route("GetEntry")]
        public async Task<IActionResult> GetEntry()
        {
            var result = await _entryService.ListAsync(new ListRequestModel());
            return new JsonResult(result);
        }
    }
}
