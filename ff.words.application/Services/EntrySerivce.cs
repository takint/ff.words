namespace ff.words.application.Services
{
    using global::AutoMapper;

    using ff.words.application.Common;
    using ff.words.application.Interfaces;
    using ff.words.data.Interfaces;
    using ff.words.data.Models;
    using System.Threading.Tasks;
    using ff.words.application.ViewModels;

    public class EntrySerivce : BaseService<EntryModel>, IEntryService
    {
        public EntrySerivce(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IRepository<EntryModel> entryRepository) 
            : base(unitOfWork, mapper, entryRepository)
        {
        }

        public async Task<ListResponseModel<EntryViewModel>> ListAsync(ListRequestModel request)
        {
            var result = await Repository.GetAllAsync();
            return Mapper.Map<ListResponseModel<EntryViewModel>>(result);
        }
    }
}