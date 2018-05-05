using System.ComponentModel.DataAnnotations;

namespace CMSCore.Content.GrainInterfaces.Types
{
    public class CommentViewModel
    {
        public string Text { get; set; }
        public string FullName { get; set; }
    }

    public class CreateCommentViewModel
    {
        [Required(ErrorMessage = nameof(Text) + " is required"),
         MinLength(5, ErrorMessage = "Text must be longer than 5 characters")]
        public string Text { get; set; }

        [Required(ErrorMessage = nameof(FullName) + " is required")]
        public string FullName { get; set; }
    }
}