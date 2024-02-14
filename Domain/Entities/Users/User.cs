

using Core.Domain.BaseEntity;
using Core.Domain.Entities.Role;
using System.ComponentModel.DataAnnotations;

namespace Entities.Users
{
    public class User :BaseEntity<long>
    {
        public User()
        {
            IsActive = true;
        }
        public string FullName { get; set; }
        public DateTime CreateDate { get; private set; } = DateTime.Now;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public DateTimeOffset? FirstLoginDate { get; set; }
        public UserType UserType { get; set; }
        public string? Discord { get; set; }
        public string? FaceBook { get; set; }
        public string? Telegram { get; set; }
        public string? AvatarImage { get; set; }
        public string? About { get; set; }
        public bool IsReadRules { get; set; }
        public bool? IsGoogleAuthenticatorEnable { get; set; }
        public string? GoogleAuthenticatorSecret { get; set; }
        public byte[]? TemporaryCodeForGoogleAuthenticator { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }

    public enum UserStatus
    {
        InActive = 1,
        Active,
        PreRegister,
        Limited
    }

    public enum UserContactType
    {
        Phone = 1,
        PhoneNumber,
        Email,
        Discord,
        Telegram,
    }

    public enum UserImageType 
    {
        Profile =1,
        Licence ,
        Docs,
        

    }
    public enum GenderType
    {
        [Display(Name = "مرد")]
        Male = 1,

        [Display(Name = "زن")]
        Female = 2
    }
}
