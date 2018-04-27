using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMSCore.Content.Data;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.Models;
using CMSCore.Shared.Abstractions.Extensions;
using CMSCore.Shared.Types.Content.EntityHistory;
using CMSCore.Shared.Types.Content.Feed;
using CMSCore.Shared.Types.Extensions.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;

namespace CMSCore.Content.Grains
{
    public class ContentReaderGrain : Grain, IContentReaderGrain
    {
        private readonly ContentDbContext _context;
        private readonly ILogger<ContentGrain> _logger;

        public ContentReaderGrain(ContentDbContext context, ILogger<ContentGrain> logger = null)
        {
            _context = context;
            _logger = logger ?? ServiceProvider.GetService<ILogger<ContentGrain>>();
        }

        private string ProvidedPrimaryKey => (this).GetPrimaryKeyString();

        public async Task<IEnumerable<PageTreeViewModel>> PagesToList()
        {
            try
            {
                return (await _context.Set<Page>().ToListAsync())?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<PageViewModel> PageById()
        {
            try
            {
                return (await (_context.Set<Page>()
                        .Include(x => x.StaticContent)
                        .Include(x => x.Feed)
                        .ThenInclude(x => x.FeedItems))
                    .Include(x => x.EntityHistory)
                    .FirstOrDefaultAsync(x => x.Id == ProvidedPrimaryKey))
                    ?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<PageViewModel> PageByName()
        {
            try
            {
                return (await _context.Set<Page>()
                        .Include(x => x.StaticContent)
                        .Include(x => x.Feed)
                        .ThenInclude(x => x.FeedItems)
                        .FirstOrDefaultAsync(x => x.NormalizedName == ProvidedPrimaryKey))
                    ?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<FeedViewModel>> FeedsToList()
        {
            try
            {
                return (await _context.Set<Feed>().ToListAsync())
                    ?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<FeedItemPreviewViewModel>> FeedItemsByFeedId()
        {
            try
            {
                return (await _context.Set<FeedItem>()
                    .Include(x => x.StaticContent)
                    .Where(x => x.FeedId == ProvidedPrimaryKey).ToListAsync())
                    ?.ViewModel();
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
                return (await _context.Set<FeedItem>()
                    .Include(x => x.StaticContent)
                    .FirstOrDefaultAsync(x => x.Id == ProvidedPrimaryKey))
                    ?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }


        public async Task<IEnumerable<EntityHistoryViewModel>> EntityHistoryByEntityId()
        {
            try
            {
                return (await _context.Set<EntityHistory>()
                    .Where(x => x.EntityId == ProvidedPrimaryKey)
                    .ToListAsync())
                    ?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }

        public async Task<IEnumerable<EntityHistoryViewModel>> EntityHistoryToList()
        {
            try
            {
                return (await _context.Set<EntityHistory>()
                    .ToListAsync())
                    ?.ViewModel();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex);
                return null;
            }
        }
    }
}