using System;
using System.Collections.Generic;

namespace CMSCore.Content.Api.Models
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityUserId { get; set; }

        public string Id { get; set; }
        public bool IsDisabled { get; set; } = false;

        public bool IsRemoved { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public List<EntityHistoryViewModel> EntityHistory { get; set; }
    }
}