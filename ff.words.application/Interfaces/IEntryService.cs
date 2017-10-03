namespace ff.words.application.Interfaces
{
    using ff.words.application.Common;
    using ff.words.application.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEntryService : IDisposable
    {
        Task<TViewModel> CreateAsync<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;

        Task<TViewModel> UpdateAsync<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel;

        Task<bool> DeleteAsync(int id);

        Task<TViewModel> GetByIdAsync<TViewModel>(int id) where TViewModel : BaseViewModel;

        Task<ListResponseModel<TViewModel>> ListAsync<TViewModel>(ListRequestModel request) where TViewModel : BaseViewModel;

        Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>() where TViewModel : BaseViewModel;

        Task<IEnumerable<EntryViewModel>> ListAsync(ListRequestModel request);
    }
}