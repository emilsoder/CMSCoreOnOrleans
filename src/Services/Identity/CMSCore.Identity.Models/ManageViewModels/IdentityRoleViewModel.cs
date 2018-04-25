namespace CMSCore.Identity.Models.ManageViewModels
{
    public class IdentityRoleViewModel
    {
        public IdentityRoleViewModel()
        {
        }

        public IdentityRoleViewModel(string normalizedName) => NormalizedName = normalizedName;

        public IdentityRoleViewModel(string normalizedName, string roleId) : this(normalizedName) => RoleId = roleId;

        public string RoleId { get; set; }
        public string NormalizedName { get; set; }
    }
}