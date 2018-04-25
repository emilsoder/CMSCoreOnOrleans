namespace CMSCore.Shared.Abstractions.Types.Account
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }

    public class GetUserViewModel
    {
        public string Id { get; set; }
        public string IdentityUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}