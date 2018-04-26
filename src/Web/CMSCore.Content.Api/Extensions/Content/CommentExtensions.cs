using System.Collections.Generic;
using System.Linq;
using CMSCore.Content.Models;

namespace CMSCore.Content.Api.Controllers
{
 
    public static class CommentExtensions
    {
        public static Comment CreateComment(this CommentViewModel model)
        {
            return new Comment(model.Text, model.FullName);
        }

        public static CommentViewModel ViewModel(this Comment model)
        {
            return new CommentViewModel
            {
                Text = model.Text,
                FullName = model.FullName
            };
        }

        public static IEnumerable<CommentViewModel> ViewModel(this ICollection<Comment> models)
        {
            return models?.Select(ViewModel);
        }
    }
}