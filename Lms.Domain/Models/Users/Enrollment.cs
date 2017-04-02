using Lms.Domain.Models.Contents;
using Lms.Domain.Models.Courses;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Users
{
    public enum EnrollStatusEnum
    {
        Enrolled,
        Withdrawn,
        Pending
    }

    public enum EnrollResultEnum
    {
        Pending,
        Passed,
        Failed
    }

    public class Enrollment
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        public Enrollment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ScormData = new HashSet<ScormData>();
            this.QuizData = new HashSet<QuizData>();
        }

        public Enrollment(string userId, string sessionId) : this()
        {
            this.SessionId = sessionId;
            this.UserId = userId;
            this.EnrollTs = DateTime.UtcNow;
            
        }

        

        public void Withdraw()
        {
            this.EnrollStatus = EnrollStatusEnum.Withdrawn;
            this.CompletedTs = DateTime.UtcNow;
        }

        public bool IsActiveEnrollment()
        {
            logger.Info("[userId: {0}] has [enrollment: {1}]...[session expiry: {2}], [enrollment result: {3}]...[{4}, {5}]",
                this.UserId, this.Id, this.Session.SessionEnd, this.EnrollStatus,
                this.Session.SessionEnd < DateTime.UtcNow, this.Result == EnrollResultEnum.Pending);

            if (this.EnrollStatus == EnrollStatusEnum.Enrolled)
            {
                if (this.Result == EnrollResultEnum.Pending)
                {
                    if (this.Session.SessionEnd < DateTime.UtcNow)
                        return false;
                    else
                        return true;
                }
                else if (this.Result == EnrollResultEnum.Passed)
                {
                    return false;
                }
                else if (this.Result == EnrollResultEnum.Failed)
                {
                    return false;
                }
            }
            
            return false;
        }

        public void SetEnrollmentResult(EnrollResultEnum result)
        {
            this.Result = result;
            this.CompletedTs = DateTime.UtcNow;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(128)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(128)]
        public string SessionId { get; set; }

        public DateTime EnrollTs { get; set; }
        public DateTime? CompletedTs { get; set; }

        public EnrollStatusEnum EnrollStatus { get; set; }
        public EnrollResultEnum Result { get; set; }

        public virtual User User { get; set; }
        public virtual Session Session { get; set; }
        public virtual ICollection<ScormData> ScormData { get; set; }
        public virtual ICollection<QuizData> QuizData { get; set; }
    }
}
