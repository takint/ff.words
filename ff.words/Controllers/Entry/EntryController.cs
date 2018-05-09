namespace ff.words.Controllers.Entry
{
    using ff.words.application.Common;
    using ff.words.application.Interfaces;
    using ff.words.application.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
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
        [Route("GetEntry/{id:int}")]
        public async Task<IActionResult> GetEntry(int id)
        {
            var result = await _entryService.GetByIdAsync<EntryViewModel>(id);
            return new JsonResult(result);
        }

        [HttpGet]
        [Authorize]
        [Route("GetEntries")]
        public async Task<IActionResult> GetEntries()
        {
            var result = await _entryService.ListAsync(new ListRequestModel());
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("AddEntry")]
        public async Task<IActionResult> AddEntry([FromBody]EntryViewModel entry)
        {
            entry.CreatedDate = DateTime.Now;
            entry.CreatedUser = "TakiNT";
            var result = await _entryService.CreateAsync(entry);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("UpdateEntry")]
        public async Task<IActionResult> UpdateEntry([FromBody]EntryViewModel entry)
        {
            entry.UpdatedDate = DateTime.Now;
            entry.UpdatedUser = "TakiNT";
            var result = await _entryService.UpdateAsync(entry);
            return new JsonResult(result);
        }

        [HttpPost]
        [Route("DeleteEntry")]
        public async Task<IActionResult> DeleteEntry(int entryId)
        {
            var result = await _entryService.DeleteAsync(entryId);
            return new JsonResult(result);
        }
    }
}
