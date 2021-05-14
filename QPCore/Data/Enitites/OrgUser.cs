using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace QPCore.Data.Enitites
{
    public partial class OrgUser
    {
        public OrgUser()
        {
            this.RefreshTokens = new HashSet<RefreshToken>();
            this.AppUsers = new HashSet<AppUser>();
            this.UserRoles = new HashSet<UserRole>();
        }
        public int UserId { get; set; }
        public int OrgId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LoginName { get; set; }
        public BitArray UseWindowsAuth { get; set; }
        public string Password { get; set; }
        public DateTime? PasswordReset { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public virtual Organization Org { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
