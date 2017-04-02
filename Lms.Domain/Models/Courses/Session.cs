using Lms.Domain.Models.Users;
using Lms.Domain.Models.Workflows;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Courses
{
    public class Session
    {
        public Session()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Session(string sessionName, string description, double? cost, DateTime sessionStartDate, DateTime sessionEndDate, DateTime enrollmentStartDate, DateTime enrollmentEndDate)
            : this()
        {
            this.Name = sessionName;
            this.Description = description;
            this.Cost = cost;
            this.SessionStart = sessionStartDate;
            this.SessionEnd = sessionEndDate;
            this.EnrollStart = enrollmentStartDate;
            this.EnrollEnd = enrollmentEndDate;
            this.UpdatedTs = DateTime.UtcNow;
        }

        public Session(string sessionName, string description, double? cost, DateTime sessionStartDate, DateTime sessionEndDate, DateTime enrollmentStartDate, DateTime enrollmentEndDate,
            int maxLearner)
            : this()
        {
            this.Name = sessionName;
            this.Description = description;
            this.Cost = cost;
            this.SessionStart = sessionStartDate;
            this.SessionEnd = sessionEndDate;
            this.EnrollStart = enrollmentStartDate;
            this.EnrollEnd = enrollmentEndDate;
            this.MaxLearner = maxLearner;
            this.UpdatedTs = DateTime.UtcNow;
        }

        public bool IsEnrolledUser(string userId)
        {
            return this.Enrollments.Any(x => x.UserId == userId);
        }

        public string GetEnrollStatus(string userId)
        {
            return this.Enrollments.Single(x => x.UserId == userId).EnrollStatus.ToString();
        }

        [Key]
        [StringLength(128)]        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string CourseId { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime SessionStart { get; set; }
        public DateTime SessionEnd { get; set; }
        public DateTime EnrollStart { get; set; }
        public DateTime EnrollEnd { get; set; }

        public DateTime UpdatedTs { get; set; }
        public bool IsDeleted { get; set; }

        public double? Cost { get; set; }
        public int? MaxLearner { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ICollection<SessionWorkflow> SessionWorkflows { get; set; }
    }
}
