using System;
using Whoever.Entities.Interfaces;
using System.Collections.Generic;
using Whoever.Entities;

namespace BaseProject.Domain
{
    public class User : UserEntity<int>, IEntity, IHasCreationTime
    {
        public User()
        {
            Claims = new HashSet<UserClaim>();
            Logins = new HashSet<UserLogin>();
            Tokens = new HashSet<UserToken>();
            Roles = new HashSet<UserRole>();
        }

        public string ResetPasswordCode { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? DeactivatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDeleted{ get; set; }
        
        public virtual DeviceToken DeviceToken { get; set; }

        public virtual ICollection<UserClaim> Claims { get; private set; }
        public virtual ICollection<UserLogin> Logins { get; private set; }
        public virtual ICollection<UserToken> Tokens { get; private set; }
        public virtual ICollection<UserRole> Roles { get; private set; }
        
    }    
}
