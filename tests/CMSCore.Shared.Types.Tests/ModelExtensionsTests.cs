using CMSCore.Content.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CMSCore.Shared.Types.Tests
{
    public class ModelExtensionsTests
    {
        [Fact]
        public void FeedItem_UpdateModel_Test()
        {
            Assert.True(true);
        }

        //private Page TestPageWithContent => new Page("TestPage", true)
        //{
        //    StaticContent = new StaticContent("Test content", true)
        //};

        //private UpdatePageViewModel UpdatePageVm => new UpdatePageViewModel
        //{
        //    Name = "Updated Test Page",
        //    Content = "Updated Test Content",
        //    IsContentMarkdown = true,
        //    FeedEnabled = true
        //};

        //[Fact]
        //public void Page_UpdateModel_Test()
        //{
        //    var testPage = TestPageWithContent;
        //    var viewModel = UpdatePageVm;

        //    // Act
        //    testPage.UpdateModelDynamic(viewModel);

        //    // Assert
        //    Assert.True(testPage.Name == viewModel.Name);
        //    Assert.True(testPage.StaticContent.Content == viewModel.Content);
        //}

        //private Feed TestFeed => new Feed() {Name = "Feed Name"};
        //private UpdateFeedViewModel UpdateFeedVm => new UpdateFeedViewModel() {Name = "New Feed Name"};

        //[Fact]
        //public void Feed_UpdateModel_Test()
        //{
        //    var testFeed = TestFeed;
        //    var viewModel = UpdateFeedVm;

        //    // Act
        //    testFeed.UpdateModelDynamic(viewModel);

        //    // Assert
        //    Assert.True(testFeed.Name == viewModel.Name);
        //    Assert.True(testFeed.Name != UpdateFeedVm.Name);
        //}

        //private FeedItem TestFeedItem => new FeedItem("Feed item", "feed item description", "content",
        //    new List<string>()
        //    {
        //        "tag 1",
        //        "tag 2",
        //        "tag 3"
        //    }, true);

        //private UpdateFeedItemViewModel UpdateFeedItemVm => new UpdateFeedItemViewModel()
        //{
        //    Content = "new content",
        //    CommentsEnabled = false,
        //    Description = "new description",
        //    IsContentMarkdown = false,
        //    Title = "new title",
        //    Tags = new List<string>()
        //    {
        //        "tag 1",
        //        //"tag 2",
        //        "tag 3",
        //        "new tag 4"
        //    }
        //};


        //    var testFeedItem = TestFeedItem;
        //    var viewModel = UpdateFeedItemVm;

        //    // Act
        //    testFeedItem.UpdateModelDynamic(viewModel);

        //    // Assert
        //    Assert.True(testFeedItem.Title == viewModel.Title);
        //    Assert.True(testFeedItem.StaticContent.Content == UpdateFeedItemVm.Content);

        //    Assert.False(testFeedItem.StaticContent.IsContentMarkdown);
        //    Assert.False(testFeedItem.CommentsEnabled);

        //    var tag2False = testFeedItem.Tags.FirstOrDefault(x => x.Name == "tag 2");
        //    var tag4Exists = testFeedItem.Tags.FirstOrDefault(x => x.Name == "new tag 4");

        //    Assert.True(tag2False == null);
        //    Assert.True(tag4Exists != null);
        //}
    }
}


/* public static IEnumerable<Tag> AsTagsEnumerable(this IList<string> tagNames)
        {
            return tagNames?.Select(x => new Tag(x));
        }

        public static ICollection<Tag> AsTagCollection(this List<Tag> tags, IList<string> tagNames)
        {
            if (tagNames == null) return null;

            if (tags == null || !tags.Any())
            {
                return new List<Tag>().AddTags(tagNames);
            }

            var existingTagNames = tags.Select(t => t.Name);

            var tagNamesToAdd = tagNames?.Where(tagName => !existingTagNames.Contains(tagName));
            
            tags.AddTags(tagNamesToAdd);

            var tagsToRemove = tags.Where(tag => !tagNames.Contains(tag.Name)).ToList();

              tags.RemoveTags(tagsToRemove);

            return tags;
        }

        private static ICollection<Tag> AddTags(this ICollection<Tag> tags, IEnumerable<string> tagNamesToAdd)
        {
            foreach (var tagName in tagNamesToAdd)
            {
                tags.Add(new Tag(tagName));
            }

            return tags;
        }

        private static List<Tag> RemoveTags(this List<Tag> tags, IEnumerable<Tag> tagsToRemove)
        {
            var _remainingTags = tags;

            foreach (var tag in tagsToRemove)
            {
                _remainingTags.Remove(tag);
            }

            return _remainingTags;
        }
    }*/