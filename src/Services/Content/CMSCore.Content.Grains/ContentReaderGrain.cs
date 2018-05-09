using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.Data;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Models;
using CMSCore.Shared.Abstractions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using static CMSCore.Content.Grains.Extensions.ContentReaderExtensions;

namespace CMSCore.Content.Grains
{
    public class ContentReaderGrain : Grain, IContentReaderGrain
    {
        private readonly ContentDbContext _context;
        private readonly ILogger<ContentReaderGrain> _logger;

        public ContentReaderGrain(ContentDbContext context, ILogger<ContentReaderGrain> logger = null)
        {
            _context = context;
            _logger = logger ?? ServiceProvider.GetService<ILogger<ContentReaderGrain>>();
            _context.LoadRelatedEntities();
        }

        private string ProvidedPrimaryKey => (this).GetPrimaryKeyString();

        #region Page

        public Task<IEnumerable<PageTreeViewModel>> PagesToList()
        {
            try
            {
                var pages = _context.GetActiveEntities<Page>();
                var result = GetPageTreeViewModels(pages);
                return Task.FromResult(result);
                //return (await _context.Set<Page>().ToListAsync())?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<PageViewModel> PageById()
        {
            var page = await _context.FindActiveEntityAsync<Page>(ProvidedPrimaryKey);

            var viewModel = ConvertToPageViewModel(page);

            return viewModel;
        }

        public Task<PageViewModel> PageByName()
        {
            return null;
            //try
            //{
            //    var set = _context.Pages;

            //    var page = set.FirstOrDefault(x => x.NormalizedName == ProvidedPrimaryKey && x.IsRemoved == false);

            //    return Task.FromResult(ConvertToPageViewModel(page));
            //}
            //catch (Exception ex)
            //{
            //    _logger?.LogError(ex);
            //    return null;
            //}
        }

        public async Task<bool> CreateComment(CommentViewModel comment)
        {
            try
            {
                var feedItem = await _context.FindActiveEntityAsync<FeedItem>(ProvidedPrimaryKey);
                if (feedItem == null || !feedItem.CommentsEnabled) throw new Exception("Cannot add comment.");
                var _comment = new Comment(comment.Text, comment.Text);
                var result = _context.CreateEntityAsync(_comment, Guid.NewGuid().ToString());
                return true;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return false;
            }
        }

        #endregion

        #region Feeds

        public Task<IEnumerable<FeedViewModel>> FeedsToList()
        {
            try
            {
                var feeds = _context.Feeds;
                var lst = new List<FeedViewModel>();

                foreach (var feed in feeds)
                {
                    lst.Add(GetFeedViewModel(feed));
                }

                IEnumerable<FeedViewModel> vm = lst;
                return Task.FromResult(vm);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public Task<IEnumerable<FeedItemPreviewViewModel>> FeedItemsByFeedId()
        {
            try
            {
                var filtered = _context.FeedItems.Where(x => x.FeedId == ProvidedPrimaryKey);
                var vm = GetFeedItemPreviewModels(filtered);
                return Task.FromResult(vm);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<FeedItemViewModel> FeedItemById()
        {
            try
            {
                var feedItem = await _context.FindActiveEntityAsync<FeedItem>(ProvidedPrimaryKey);
                var vm = GetFeedItemViewModel(feedItem);
                return vm;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        #endregion

         
    }
}