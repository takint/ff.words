namespace ff.words.application.Common
{
    using ff.words.data.Common;
    using ff.words.data.Interfaces;
    using ff.words.data.Models;
    using global::AutoMapper;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class BaseService<TModel>
            where TModel : BaseEntity
    {
        protected readonly IUnitOfWork UoW;
        protected readonly IMapper Mapper;
        protected readonly IRepository<TModel> Repository;

        public BaseService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<TModel> repository)
        {
            UoW = unitOfWork;
            Mapper = mapper;
            Repository = repository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public virtual async Task<TViewModel> CreateAsync<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            viewModel.ValidateAndThrow();

            TModel model = Mapper.Map<TModel>(viewModel);

            var error = await ValidateDatabaseBeforeAddOrUpdateAsync(model);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error); // TODO: Implement application exception
            }

            Repository.Add(model);
            await Repository.SaveChangesAsync();

            viewModel = Mapper.Map<TViewModel>(model);
            return viewModel;
        }

        /// <summary>Deletes by the specified identifier async.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public virtual async Task<bool> DeleteAsync(int id)
        {
            Repository.Remove(id);
            await Repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>() where TViewModel : BaseViewModel
        {
            var models = await Repository.GetAllAsync();
            return Mapper.Map<IEnumerable<TViewModel>>(models);
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(int id) where TViewModel : BaseViewModel
        {
            var model = await Repository.GetByIdAsync(id);

            return Mapper.Map<TViewModel>(model);
        }

        public virtual async Task<ListResponseModel<TViewModel>> ListAsync<TViewModel>(ListRequestModel request) where TViewModel : BaseViewModel
        {
            return await ListAsync<TViewModel>(request, null);
        }

        public virtual Task<ListResponseModel<TViewModel>> ListAsync<TViewModel>(ListRequestModel request, Expression<Func<TModel, bool>> customFilter = null) where TViewModel : BaseViewModel
        {
            //var filter = new Filter<TModel>(state.Filter, this.Repository.FilterMaps);
            //if (customFilter != null)
            //{
            //    filter.AddExpression(customFilter);
            //}
            //var orderby = new OrderBy<TModel>(state.Sort, this.Repository.OrderByColumnMaps);

            //var recordCount = await this.Repository.CountAsync(filter.Expression);

            //var response = new ListResponseModel<TViewModel>
            //{
            //    RecordCount = recordCount,
            //    ListResult = new List<TViewModel>()
            //};

            //if (recordCount > 0)
            //{
            //    var models = await this.Repository.QueryPageAsync(state.Skip, state.Take, filter.Expression, orderby.Expression, true);

            //    response.ListResult = this.Mapper.Map<IEnumerable<TViewModel>>(models);
            //}

            //return response;
            throw new NotImplementedException();
        }

        public virtual async Task<TViewModel> UpdateAsync<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            viewModel.ValidateAndThrow();

            var model = Mapper.Map<TModel>(viewModel);

            var error = await ValidateDatabaseBeforeAddOrUpdateAsync(model);
            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            Repository.Update(model);
            await Repository.SaveChangesAsync();

            model = await Repository.GetByIdAsync(model.Id);

            viewModel = Mapper.Map<TViewModel>(model);
            return viewModel;
        }

        

        protected virtual Task<string> ValidateDatabaseBeforeAddOrUpdateAsync(TModel model)
        {
            return Task.FromResult(string.Empty);
        }
    }
}