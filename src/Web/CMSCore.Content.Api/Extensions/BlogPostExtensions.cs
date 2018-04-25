using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Api.Models.Content;
using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Controllers
{
    public static class BlogPostExtensions
    {
        public static StaticContent ToModel(this StaticContentViewModel model)
        {
            return new StaticContent
            {
                IsContentMarkdown = model.IsContentMarkdown,
                Content = model.Content
            };
        }

        public static BlogPost ToModel(this CreateBlogPostViewModel model)
        {
            return new BlogPost
            {
                Title = model.Title,
                Description = model.Description,
                Content = model.Content?.ToModel(),
                //BlogPostTags = model.Tags?.ToModel(),
                BlogId = model.BlogId
            };
        }

        public static BlogPost ToModel(this UpdateBlogPostViewModel model)
        {
            return new BlogPost
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Content = model.Content?.ToModel(),
                BlogPostTags = model.Tags?.ToModel(),
            };
        }

        private static BlogPostTag ToModel(this string tag)
        {
            return new BlogPostTag {TagId = tag};
        }

        public static List<BlogPostTag> ToModel(this IList<string> tags)
        {
            return tags?.Select(x => x.ToModel())?.ToList();
        }

        public static Blog ToModel(this UpdateBlogViewModel model)
        {
            return new Blog()
            {
                Title = model.Title,
                Description = model.Description
            };
        }
    }
}