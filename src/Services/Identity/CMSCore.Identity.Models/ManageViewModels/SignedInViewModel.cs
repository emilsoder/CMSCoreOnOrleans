namespace CMSCore.Identity.Models.ManageViewModels
{
    public class SignedInViewModel
    {
        public string IdentityUserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public IdentityRoleViewModel[] Roles { get; set; }

        public string JwtToken { get; set; }

    }
}
