using System;
using System.ComponentModel.DataAnnotations;

namespace CMSCore.Content.GrainInterfaces.Types
{
    #region Read

    [Orleans.Concurrency.Immutable]
    public class UserViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityUserId { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsRemoved { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    #endregion

    #region Write

    public class CreateUserViewModel
    {
        [Required(ErrorMessage = nameof(IdentityUserId) + " is required")]
        public string IdentityUserId { get; set; }

        [Required(ErrorMessage = nameof(FirstName) + " is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = nameof(LastName) + " is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = nameof(Email) + " is required")]
        public string Email { get; set; }
    }

    public class UpdateUserViewModel
    {
        [Required(ErrorMessage = nameof(FirstName) + " is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = nameof(LastName) + " is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = nameof(Email) + " is required")]
        public string Email { get; set; }
    }

    public class DeleteUserViewModel
    {
        public DeleteUserViewModel(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public static DeleteUserViewModel Initialize(string id) => new DeleteUserViewModel(id);
    }

    #endregion
}