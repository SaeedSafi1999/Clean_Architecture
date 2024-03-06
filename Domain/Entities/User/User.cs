using Core.Domain.BaseEntity;
using Core.Domain.Entities.Role;
using Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Entities.UsersEntity
{
    public class User : BaseEntity<long>
    {
        public User()
        {
            IsActive = false;
        }

        public string FullName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Mobile { get; set; }
        public string? Email { get; set; }
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

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        //relations
        public virtual Role Role { get; set; }


        //set variables for update
        internal void SetMobile(string mobile) => Mobile = mobile;
        internal void SetEmail(string email) => Email = email;
        internal void SetFirstName(string firstName) => FirstName = firstName;
        internal void SetLastName(string lastName) => LastName = lastName;
        internal void SetAge(int age) => Age = age;
        internal void SetGender(GenderType type) => Gender = type;
        internal void SetRoleId(int roleId) => RoleId = roleId;
        internal void SetProfileImage(string url) => AvatarImage = url;

        internal void SetSocialMedia(string? telegram, string? discord, string? facebook)
        {
            Telegram = telegram;
            Discord = discord;
            FaceBook = facebook;
        }
    }

    /// <summary>
    /// enums
    /// </summary>
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
        Profile = 1,
        Licence,
        Docs,
    }
    public enum GenderType
    {
        [Display(Name = "مرد")] Male = 1,

        [Display(Name = "زن")] Female = 2
    }
}