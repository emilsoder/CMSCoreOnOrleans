using System;
using System.Collections.Generic;
using CMSCore.Shared.Types.Content.EntityHistory;

namespace CMSCore.Shared.Types.Content.Account
{
    [Serializable]
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityUserId { get; set; }

        public bool IsDisabled { get; set; } = false;

        public bool IsRemoved { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public List<EntityHistoryViewModel> EntityHistory { get; set; }
    }

    [Serializable]
    public class CreateUserViewModel
    {
        public string IdentityUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class UpdateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    [Serializable]
    public class DeleteUserViewModel
    {
        public DeleteUserViewModel(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public static DeleteUserViewModel Initialize(string id) => new DeleteUserViewModel(id);
    }
}