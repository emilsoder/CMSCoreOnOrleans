using System;

namespace CMSCore.Content.Models
{
    
    public class User : EntityBase
    {
        public User()
        {
        }

        public User(string identityUserId) => IdentityUserId = identityUserId;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string IdentityUserId { get; set; }
    }
}