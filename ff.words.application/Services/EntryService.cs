namespace ff.words.application.Services
{
    using global::AutoMapper;

    using ff.words.application.Common;
    using ff.words.application.Interfaces;
    using ff.words.data.Interfaces;
    using ff.words.data.Models;
    using System.Threading.Tasks;
    using ff.words.application.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class EntryService : BaseService<EntryModel>, IEntryService
    {
        public EntryService(IUnitOfWork unitOfWork, 
            IMapper mapper,
            IEntryRepository entryRepository) 
            : base(unitOfWork, mapper, entryRepository)
        {
        }

        public async Task<IEnumerable<EntryViewModel>> ListAsync(ListRequestModel request)
        {
            var result = await Repository.GetAllAsync();
            return Mapper.Map<IEnumerable<EntryViewModel>>(result.OrderByDescending(e => e.CreatedDate));
        }
    }
}