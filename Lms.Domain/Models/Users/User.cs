using Lms.Domain.Models.Certificates;
using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Quizzes;
using Lms.Domain.Models.Utils;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Users
{
    public enum UserTypeEnum
    {
        Internal,
        External
    }

    public enum UserStatusEnum
    {
        Registered,
        Active,
        Inactive
    }

    public enum AcquisitionEnum
    {
        OnPremise,
        Office365,
        Adfs
    }

    public class User
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            logger.Debug("Default constructor: " + this.Id);
            UserManagers = new HashSet<UserManager>();
        }


        public User(string email, string firstName, string lastName, string password, UserTypeEnum userType, UserStatusEnum status, bool tempPassword, AcquisitionEnum acquisition)
            : this()
        {
            logger.Debug("New Guid: " + this.Id);

            this.Email = email;
            this.FirstName = CryptoUtil.EncryptToBase64(firstName, this.Id);
            this.LastName = CryptoUtil.EncryptToBase64(lastName, this.Id);
            this.Password = CryptoUtil.EncryptToBase64(password, this.Id);
            this.UserType = userType;
            this.UpdatedTs = DateTime.UtcNow;
            this.Status = status;
            this.TempPassword = tempPassword;
            this.Acquisition = acquisition;
        }

        public void AssignCertificate(Certificate certificate)
        {
            logger.Info("Assign certificate {0} to userId: {1}", certificate.Id, this.Id);
            logger.Info("Checking user has the certificate: {0}", this.UserCertificates.Any(x => x.CertificateId == certificate.Id));

            if (this.UserCertificates.Any(x => x.CertificateId == certificate.Id))
            {
                DateTime? expireTs = null;
                if (certificate.ExpiryType == CertificateExpiryType.Term && certificate.ExpiryMonth != null)
                    expireTs = DateTime.UtcNow.AddMonths(certificate.ExpiryMonth.Value);

                var userCertificate = this.UserCertificates.Single(x => x.CertificateId == certificate.Id);
                userCertificate.IssuedTs = DateTime.UtcNow;
                userCertificate.ExpireTs = expireTs;
            }
            else
            {
                if (this.UserCertificates == null)
                    this.UserCertificates = new List<UserCertificate>();

                DateTime? expireTs = null;
                if (certificate.ExpiryType == CertificateExpiryType.Term && certificate.ExpiryMonth != null)
                    expireTs = DateTime.UtcNow.AddMonths(certificate.ExpiryMonth.Value);

                logger.Debug("certificate expire date: {0}, issue date: {1}", expireTs, DateTime.UtcNow);

                
                this.UserCertificates.Add(new UserCertificate
                    {
                        CertificateId = certificate.Id,
                        UserId = this.Id,
                        IssuedTs = DateTime.UtcNow,
                        ExpireTs = expireTs,
                    });
            }
        }

        public List<Role> GetRoles()
        {
            return this.UserRoles.Select(x => x.Role).Where(x => !x.IsDeleted).ToList();
        }


        public List<Group> GetGroups()
        {
            return this.UserGroups.Select(x => x.Group).Where(x => !x.IsDeleted).ToList();
        }


        public void AddUserGroup(List<string> groups)
        {
            if (this.UserGroups == null)
                this.UserGroups = new List<UserGroup>();

            foreach (var group in groups)
                this.UserGroups.Add(new UserGroup() { GroupId = group, User = this });
        }

        public void AddUserRole(List<string> roles)
        {
            if (this.UserRoles == null)
                this.UserRoles = new List<UserRole>();

            foreach (var role in roles)
                this.UserRoles.Add(new UserRole() { RoleId = role, User = this });
        }

        public void AddUserGroup(Group group)
        {
            if (this.UserGroups == null)
                this.UserGroups = new List<UserGroup>();

            this.UserGroups.Add(new UserGroup() { Group = group, User = this });
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(128)]
        public string Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(256)]
        public string LastName { get; set; }

        public string Password { get; set; }
        public UserTypeEnum UserType { get; set; }

        public DateTime UpdatedTs { get; set; }

        public bool TempPassword { get; set; }

        public string ResetKey { get; set; }

        public UserStatusEnum Status { get; set; }

        public AcquisitionEnum Acquisition { get; set; }

        public string DecryptedFirstName
        {
            get { return CryptoUtil.DecryptFromBase64(this.FirstName, this.Id); }
        }

        public string DecryptedLastName
        {
            get { return CryptoUtil.DecryptFromBase64(this.LastName, this.Id); }
        }

        public string DecryptedFullName
        {
            get { return DecryptedFirstName + " " + DecryptedLastName; }
        }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<UserGroup> UserGroups { get; set; }
        public virtual ICollection<CompanyAccess> CompanyAccesses { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<UserCertificate> UserCertificates { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<UserManager> UserManagers { get; set; }
    }
}
