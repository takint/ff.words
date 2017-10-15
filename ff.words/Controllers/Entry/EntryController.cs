namespace ff.words.Controllers.Entry
{
    using ff.words.application.Common;
    using ff.words.application.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;


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
        public async Task<IActionResult> GetEntry(int id)
        {
            var result = await _entryService.ListAsync(new ListRequestModel());
            return new JsonResult(result);
        }

        [HttpGet]
        [Route("GetEntries")]
        public async Task<IActionResult> GetEntries()
        {
            var result = await _entryService.ListAsync(new ListRequestModel());
            return new JsonResult(result);
        }
    }
}
