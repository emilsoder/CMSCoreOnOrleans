using System;
using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Data;
using CMSCore.Content.GrainInterfaces;
using CMSCore.Content.GrainInterfaces.Types;
using CMSCore.Content.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace CMSCore.Content.Grains.Tests
{
    public class Grains_IntegrationTests
    {
        private readonly AccountManagerGrain _accountManagerGrain;
        private readonly ContentManagerGrain _contentManagerGrain;
        private readonly IContentReaderGrain _contentReaderGrain;

        public Grains_IntegrationTests()
        {
            var context = new ContentDbContext(ContentDbContextOptions.DefaultPostgresOptions);
            var repository = new RepositoryManager(context);
            var logger = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider().GetService<ILoggerFactory>();

            _accountManagerGrain = new AccountManagerGrain(repository, logger.CreateLogger<AccountManagerGrain>());
            _accountManagerGrain.UserId = kalleKarlssonId;

            _contentManagerGrain = new ContentManagerGrain(repository, logger.CreateLogger<ContentManagerGrain>());
            _contentManagerGrain.UserId = kalleKarlssonId;

            _contentReaderGrain = new ContentReaderGrain(context, logger.CreateLogger<ContentReaderGrain>());
        }

        //private string adminId = "e0254192-fbc4-4054-82b4-40df643d05f6";
        private string kalleKarlssonId = "085396b3-2d64-4e02-9fe7-755c7488009a";

        [Fact]
        public void GetFeeds()
        {
            var feeds = _contentReaderGrain.FeedsToList().GetAwaiter().GetResult();

            var fds = feeds;

            Assert.Contains(fds, x => x.FeedItems.Any());
        }

        [Fact]
        public void Create_User()
        {
            var userViewModel = new CreateUserViewModel
            {
                Email = "kalle.karlsson@cmscore.com",
                FirstName = "Kalle",
                LastName = "Karlsson",
                IdentityUserId = Guid.NewGuid().ToString()
            };

            // Act
            var result = _accountManagerGrain.Create(userViewModel).GetAwaiter().GetResult();

            // Assert
            Assert.True(result.Succeeded);
        }


        [Fact]
        public void Create_Page_Test()
        {
            var viewModel = new CreatePageViewModel()
            {
                Content = "Welcome to my first page",
                FeedEnabled = true,
                Name = "My first page",
                IsContentMarkdown = true
            };

            // Act
            var result = _contentManagerGrain.Create(viewModel).GetAwaiter().GetResult();

            // Assert
            Assert.True(result.Succeeded);
        }

        [Fact]
        public void Add_FeedItem_Test()
        {
            var feedId = _contentReaderGrain.FeedsToList().GetAwaiter().GetResult().FirstOrDefault()?.Id;

            Assert.True(feedId != null);

            var viewModel = new CreateFeedItemViewModel()
            {
                Content = "Welcome to my first feed item",
                IsContentMarkdown = true,
                CommentsEnabled = true,
                Description = "First feed item",
                Tags = new List<string>() { "Tag 1", "Tag 2" },
                Title = "Feed item 1",
                FeedId = feedId
            };

            // Act
            var result = _contentManagerGrain.Create(viewModel).GetAwaiter().GetResult();

            // Assert
            Assert.True(result.Succeeded);
        }
        [Fact]
        public void Update_Feed_Test()
        {
            var feedId = _contentReaderGrain.FeedsToList().GetAwaiter().GetResult().FirstOrDefault()?.Id;

            Assert.True(feedId != null);

            var viewModel = new UpdateFeedViewModel()
            {
                Id = feedId,
                Name = "Feed name updated"
            };

            // Act
            var result = _contentManagerGrain.Update(viewModel, feedId).GetAwaiter().GetResult();

            // Assert
            Assert.True(result.Succeeded);
        }
    }
}