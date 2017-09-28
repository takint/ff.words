namespace ff.words.application.Common
{
    using ff.words.data.Common;
    using ff.words.data.Interfaces;
    using ff.words.data.Models;
    using global::AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class BaseService<TModel>
            where TModel : BaseEntity
    {
        protected readonly IUnitOfWork UoW;
        protected readonly IMapper Mapper;
        protected readonly IRepository<TModel> Repository;

        public BaseService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IRepository<TModel> entryRepository)
        {
            UoW = unitOfWork;
            Mapper = mapper;
            Repository = entryRepository;
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
            var models = await this.Repository.GetAllAsync();
            return this.Mapper.Map<IEnumerable<TViewModel>>(models);
        }

        public async Task<TViewModel> GetByIdAsync<TViewModel>(int id) where TViewModel : BaseViewModel
        {
            var model = await this.Repository.GetAsync(id, true);

            return this.Mapper.Map<TViewModel>(model);
        }

        public virtual async Task<ListResponseModel<TViewModel>> ListAsync<TViewModel>(BaseListRequestModel request) where TViewModel : BaseViewModel
        {
            return await this.ListAsync<TViewModel>(request, null);
        }

        public virtual async Task<ListResponseModel<TViewModel>> ListAsync<TViewModel>(BaseListRequestModel request, Expression<Func<TModel, bool>> customFilter = null) where TViewModel : BaseViewModel
        {
            var state = JsonConvert.DeserializeObject<StateRequestModel>(request.State);

            if (state.ShowDeleted.HasValue)
            {
                state.Filter = state.Filter ?? new FilterRequest();

                state.Filter.Filters.Insert(0,
                    new FilterDetail()
                    {
                        Field = "Deleted",
                        Operator = "eq",
                        Value = state.ShowDeleted.Value.ToString()
                    });
            }

            var filter = new Filter<TModel>(state.Filter, this.Repository.FilterMaps);
            if (customFilter != null)
            {
                filter.AddExpression(customFilter);
            }
            var orderby = new OrderBy<TModel>(state.Sort, this.Repository.OrderByColumnMaps);

            var recordCount = await this.Repository.CountAsync(filter.Expression);

            var response = new ListResponseModel<TViewModel>
            {
                RecordCount = recordCount,
                ListResult = new List<TViewModel>()
            };

            if (recordCount > 0)
            {
                var models = await this.Repository.QueryPageAsync(state.Skip, state.Take, filter.Expression, orderby.Expression, true);

                response.ListResult = this.Mapper.Map<IEnumerable<TViewModel>>(models);
            }

            return response;
        }

        public virtual async Task<TViewModel> UpdateAsync<TViewModel>(TViewModel viewModel) where TViewModel : BaseViewModel
        {
            viewModel.ValidateAndThrow();

            var model = this.Mapper.Map<TModel>(viewModel);

            var error = await this.ValidateDatabaseBeforeAddOrUpdateAsync(model);
            if (!string.IsNullOrEmpty(error))
            {
                throw new AppException(error);
            }

            this.Repository.Update(model);
            await this.UnitOfWork.SaveChangesAsync();

            this.OnEntityUpdated(model);

            //model = await this.Repository.FindAsync(model.Id);

            viewModel = this.Mapper.Map<TViewModel>(model);
            return viewModel;
        }

        /// <summary>
        /// Changes life cycle status async
        /// </summary>
        /// <param name="rq">The request.</param>
        /// <returns>
        /// The <see cref="ChangeLifeCycleStatusResponse"/>.</returns>
        public async Task<ChangeLifeCycleStatusResponse> ChangeLifeCycleStatusAsync(ChangeLifeCycleStatusRequest rq)
        {
            ChangeLifeCycleStatusResponse rs = null;

            var model = await this.Repository.GetAsync(rq.Id, true);

            if (model != null)
            {
                var errorMessage = await this.ValidateChangeLifeCycleStatusValidAsync(model, rq);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    throw new AppException(errorMessage);
                }

                if (rq.IsDelete)
                {
                    model.Deleted = true;
                }
                else
                {
                    model.Inactivated = !model.Inactivated;
                }

                model.UpdatedDate = rq.UpdatedDate;
                model.UpdatedUser = rq.UpdatedUser;

                this.Repository.Update(model);

                await OnLifeCycleStatusChangedAsync(model);

                await this.UnitOfWork.SaveChangesAsync();

                model = await this.Repository.FindAsync(model.Id);

                rs = new ChangeLifeCycleStatusResponse
                {
                    LifeCycleStatusName = this.GetLifeCycleStatusName(model),
                    Inactivated = model.Inactivated,
                    Deleted = model.Deleted,
                    RowVersion = ByteArrayConverter.ToString(model.RowVersion)
                };
            }

            return rs;
        }

        protected virtual Task<string> ValidateChangeLifeCycleStatusValidAsync(TModel model, ChangeLifeCycleStatusRequest rq)
        {
            return Task.FromResult(string.Empty);
        }

        protected virtual Task OnLifeCycleStatusChangedAsync(TModel model)
        {
            return Task.CompletedTask;
        }

        protected virtual Task<string> ValidateDatabaseBeforeAddOrUpdateAsync(TModel model)
        {
            return Task.FromResult(string.Empty);
        }

        protected virtual void OnEntityCreated(TModel model)
        {
        }

        protected virtual void OnEntityUpdated(TModel model)
        {
        }

        protected virtual void OnEntityDeleted(int id)
        {
        }

        private string GetLifeCycleStatusName(TModel model)
        {
            if (model.Deleted)
            {
                return AppResources.Deleted;
            }

            if (model.Inactivated)
            {
                return AppResources.Inactive;
            }

            if (model.Id > 0)
            {
                return AppResources.Active;
            }

            return string.Empty;
        }
    }
}