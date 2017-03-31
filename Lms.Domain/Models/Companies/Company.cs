using Lms.Domain.Models.Certificates;
using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Quizzes;
using Lms.Domain.Models.Contents;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lms.Domain.Models.Workflows;

namespace Lms.Domain.Models.Companies
{
    public class Company
    {

        public Company()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Organizations = new HashSet<Organization>();
            this.CompanyAccesses = new HashSet<CompanyAccess>();
            this.Groups = new HashSet<Group>();
            this.Certificates = new HashSet<Certificate>();
            this.Scorms = new HashSet<Scorm>();
            this.Courses = new HashSet<Course>();
            this.Quizzes = new HashSet<Quiz>();
            this.Roles = new HashSet<Role>();
            this.Notifications = new HashSet<Notification>();
            this.CompanyConfigurations = new HashSet<CompanyConfiguration>();
            this.Workflows = new HashSet<Workflow>();
        }

        public Notification AddNotification(string companyId, string updaterId, string title, string details, DateTime startDate, DateTime endDate)
        {
            Notification notification = new Notification(companyId, updaterId, title, details, startDate, endDate);
            if (this.Notifications == null)
                this.Notifications = new List<Notification>();

            this.Notifications.Add(notification);

            return notification;
        }



        public void AddRole(string name, string description)
        {
            var role = new Role(name, description);
            if (this.Roles == null)
                this.Roles = new List<Role>();

            this.Roles.Add(role);
        }

        public void AddUser(string email, string firstName, string lastName, string password, UserTypeEnum userType, UserStatusEnum status, bool tempPassword,
            AcquisitionEnum acquisition, List<string> groups, List<string> roles)
        {
            var user = new User(email, firstName, lastName, password, userType, status, true, acquisition);
            if (groups != null && groups.Any())
                user.AddUserGroup(groups);
            if (roles != null && roles.Any())
                user.AddUserRole(roles);

            this.CompanyAccesses.Add(new CompanyAccess { Company = this, User = user });
        }

        public void AddQuiz(Quiz quiz)
        {
            if (this.Quizzes == null)
                this.Quizzes = new List<Quiz>();

            this.Quizzes.Add(quiz);
        }

        public void AddCertificate(Certificate certificate)
        {
            if (this.Certificates == null)
                this.Certificates = new List<Certificate>();

            this.Certificates.Add(certificate);
        }

        public void AddCompanyAccess(User user)
        {
            if (this.CompanyAccesses == null)
                this.CompanyAccesses = new List<CompanyAccess>();

            this.CompanyAccesses.Add(new CompanyAccess { Company = this, User = user });
        }

        public Company(string firstName, string lastName, string phoneNumber, string name, string email, bool isTrial, DateTime expiry, string hostName)
            : this()
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumber = phoneNumber;
            this.Name = name;
            this.Email = email;
            this.IsTrial = isTrial;
            this.Expiry = expiry;
            this.HostName = hostName;
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [Required]
        [StringLength(256)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(256)]
        public string LastName { get; set; }
        [Required]
        [StringLength(256)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Required]
        [StringLength(256)]
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Expiry { get; set; }
        [Required]
        [StringLength(256)]
        public string HostName { get; set; }
        public bool IsTrial { get; set; }

        public virtual ICollection<Organization> Organizations { get; set; }
        public virtual ICollection<CompanyAccess> CompanyAccesses { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public virtual ICollection<Certificate> Certificates { get; set; }
        public virtual ICollection<Scorm> Scorms { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<CompanyConfiguration> CompanyConfigurations { get; set; }
        public virtual ICollection<Workflow> Workflows { get; set; }
    }
}
