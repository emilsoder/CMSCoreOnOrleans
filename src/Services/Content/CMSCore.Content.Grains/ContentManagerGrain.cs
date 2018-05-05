using System.Threading.Tasks;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Grains.Extensions;
using CMSCore.Content.Models;
using CMSCore.Shared.Abstractions.Types.Results;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using CMSCore.Content.Services;

namespace CMSCore.Content.Grains
{
    public class ContentManagerGrain : Grain, IContentManagerGrain
    {
        private readonly IRepositoryManager _repository;
        private readonly ILogger<ContentManagerGrain> _logger;

        public ContentManagerGrain(IRepositoryManager repository, ILogger<ContentManagerGrain> logger = null)
        {
            _repository = repository;
            _logger = logger ?? ServiceProvider.GetService<ILogger<ContentManagerGrain>>();
        }

        //private string UserId => this.GetPrimaryKeyString();
        public string _userId;

        public string UserId
        {
            get => _userId ?? this?.GetPrimaryKeyString();
            set => _userId = value;
        }

        #region Create

        public async Task<IOperationResult> Create(CreatePageViewModel model)
        {
            var entity = model.CreateModel();
            return await _repository.CreateAsync(entity, UserId);
        }

        public async Task<IOperationResult> Create(CreateFeedItemViewModel model)
        {
            var entity = model.CreateModel();
            return await _repository.CreateAsync(entity, UserId);
        }

        #endregion

        #region Update

        public async Task<IOperationResult> Update(UpdatePageViewModel model, string entityId)
        {
            return await _repository.UpdatePage(model, entityId, UserId);
        }

        public async Task<IOperationResult> Update(UpdateFeedViewModel model, string entityId)
        {
            return await _repository.UpdateFeed(model, entityId, UserId);
        }

        public async Task<IOperationResult> Update(UpdateFeedItemViewModel model, string entityId)
        {
            return await _repository.UpdateFeedItem(model, entityId, UserId);
        }

        #endregion

        #region DeleteAsync

        public async Task<IOperationResult> Delete(DeletePageViewModel model)
        {
            return await _repository.DeleteAsync<Page>(model.Id, UserId);
        }

        public async Task<IOperationResult> Delete(DeleteFeedViewModel model)
        {
            return await _repository.DeleteAsync<Feed>(model.Id, UserId);
        }

        public async Task<IOperationResult> Delete(DeleteFeedItemViewModel model)
        {
            return await _repository.DeleteAsync<FeedItem>(model.Id, UserId);
        }

        #endregion
    }
}