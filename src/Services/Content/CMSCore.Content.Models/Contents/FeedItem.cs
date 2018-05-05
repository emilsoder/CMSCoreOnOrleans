using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CMSCore.Content.Models
{
    
    public class FeedItem : EntityBase
    {
        public FeedItem()
        {
            StaticContent = new StaticContent();
        }

        public FeedItem(string title, string description, string content)
            : this()
        {
            Title = title;
            Description = description;
            StaticContent.Content = content;
        }

        public FeedItem(string title, string description, string content, IList<string> tagNames)
            : this(title, description, content)
        {
            Tags = Tags.AsTagCollection(tagNames);
        }

        public FeedItem(string title, string description, string content, IList<string> tagNames, bool commentsEnabled)
            : this(title, description, content, tagNames)
        {
            CommentsEnabled = commentsEnabled;
        }

        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NormalizedTitle = _title.NormalizeToSlug();
            }
        }

        public string NormalizedTitle { get; set; }
        public string Description { get; set; }

        public virtual StaticContent StaticContent { get; set; }


        public string FeedId { get; set; }
        public virtual Feed Feed { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public bool CommentsEnabled { get; set; } = true;
        public virtual ICollection<Comment> Comments { get; set; }
    }

    public static class NormalizationExtensions
    {
        public static string NormalizeToSlug(this string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) return input;

                var arr = input.ToCharArray();
                arr = Array.FindAll(arr, c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c));
                return Regex.Replace(new string(arr), @"\s+", "-").ToLower().Normalize();
            }
            catch (Exception)
            {
                return input;
            }
        }
    }

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

            var tagsToRemove = tags.Where(tag => !tagNames.Contains(tag.Name))?.ToList();

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