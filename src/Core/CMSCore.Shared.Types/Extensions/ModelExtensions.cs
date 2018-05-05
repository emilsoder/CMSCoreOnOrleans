using System.Linq;
using CMSCore.Content.Models;
using CMSCore.Shared.Types.Content.Account;
using CMSCore.Shared.Types.Content.Feed;
using CMSCore.Shared.Types.Extensions.Account;
using CMSCore.Shared.Types.Extensions.Content;

namespace CMSCore.Shared.Types.Extensions
{
    public static class ModelExtensions
    {
        public static TEntity UpdateModelDynamic<TEntity, TViewModel>(this TEntity entity, TViewModel model)
            where TEntity : EntityBase
            where TViewModel : class
        {
            switch (model)
            {
                case UpdatePageViewModel m:
                {
                    if (entity is Page page)
                        return page.UpdateModel(m) as TEntity;
                    break;
                }
                case UpdateFeedViewModel m:
                {
                    if (entity is Feed feed)
                        return feed.UpdateModel(m) as TEntity;
                    break;
                }
                case UpdateFeedItemViewModel m:
                {
                    if (entity is FeedItem feedItem)
                        return feedItem.UpdateModel(m) as TEntity;
                    break;
                }
                case UserViewModel m:
                {
                    if (entity is User feedItem)
                        return feedItem.UpdateModel(m) as TEntity;
                    break;
                }
            }

            return entity;
        }
    }
}