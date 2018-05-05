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
                var pages = _context.Pages;
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

        public Task<PageViewModel> PageById()
        {
            var pages = _context.Pages;


            var page = pages.FirstOrDefault(x => x.Id == ProvidedPrimaryKey);

            var viewModel = ConvertToPageViewModel(page);

            return Task.FromResult(viewModel);
        }

        public Task<PageViewModel> PageByName()
        {
            try
            {
                var set = _context.Pages;

                var page = set.FirstOrDefault(x => x.NormalizedName == ProvidedPrimaryKey);

                return Task.FromResult(ConvertToPageViewModel(page));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public Task<bool> CreateComment(CommentViewModel comment)
        {
            try
            {
                var feedItem = _context.FeedItems.Find(ProvidedPrimaryKey);
                if (feedItem == null || !feedItem.CommentsEnabled) throw new Exception("Cannot add comment.");
                var _comment = new Comment(comment.Text, comment.Text);
                feedItem.Comments.Add(_comment);
                _context.SaveChanges();

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return Task.FromResult(false);
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

        public Task<FeedItemViewModel> FeedItemById()
        {
            try
            {
                var feedItem = _context.FeedItems?.FirstOrDefault(x => x.Id == ProvidedPrimaryKey);
                var vm = GetFeedItemViewModel(feedItem);
                return Task.FromResult(vm);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        #endregion

        #region Entity history

        public Task<IEnumerable<EntityHistoryViewModel>> EntityHistoryByEntityId()
        {
            try
            {
                var items = _context.EntityHistory.Where(x => x.EntityId == ProvidedPrimaryKey);
                return Task.FromResult(GetEntityHistoryViewModels(items));
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public Task<IEnumerable<EntityHistoryViewModel>> EntityHistoryToList()
        {
            try
            {
                var items = _context.EntityHistory;

                return Task.FromResult(GetEntityHistoryViewModels(items));
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