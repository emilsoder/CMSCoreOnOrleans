namespace CMSCore.Content.Api.Models
{
    public class CreateUserViewModel
    {
        public string IdentityUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class UpdateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}