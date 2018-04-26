using System.Collections.Generic;
using System.Linq;

namespace CMSCore.Content.Models
{
    public static class TagExtensions
    {
        public static IEnumerable<Tag> AsTagsEnumerable(this IList<string> tagNames)
        {
            return tagNames?.Select(x => new Tag(x));
        }

        public static ICollection<Tag> AsTagCollection(this ICollection<Tag> tags, IList<string> tagNames)
        {
            if (tagNames == null) return null;

            if (tags == null || !tags.Any())
            {
                return new List<Tag>().AddTags(tagNames);
            }

            var existingTagNames = tags.Select(t => t.Name);

            var tagNamesToAdd = tagNames?.Where(tagName => !existingTagNames.Contains(tagName));
            
            tags.AddTags(tagNamesToAdd);

            var tagsToRemove = tags.Where(tag => !tagNames.Contains(tag.Name));

            tags = tags.RemoveTags(tagsToRemove);

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

        private static ICollection<Tag> RemoveTags(this ICollection<Tag> tags, IEnumerable<Tag> tagsToRemove)
        {
            foreach (var tag in tagsToRemove)
            {
                tags.Remove(tag);
            }

            return tags;
        }
    }
}