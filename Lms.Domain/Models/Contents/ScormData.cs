using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Contents
{
    public enum ScormDataResultEnum
    {
        NotStarted,
        Started,
        Passed,
        Faild,
        Pending
    }

    public class ScormData
    {


        public ScormData()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string EnrollmentId { get; set; }

        [Required]
        [StringLength(128)]
        public string LessonId { get; set; }

        public ScormDataResultEnum DataResult { get; set; }

        public string TemporaryData { get; set; }
        public string PersistentData { get; set; }

        public bool IsCompleted { get; set; }

        public float? Score { get; set; }

        public string DataResultDisplay
        {
            get
            {
                switch (DataResult)
                {
                    case ScormDataResultEnum.NotStarted:
                        return "Not Started";
                    default:
                        return DataResult.ToString();
                }
            }
        }

        public virtual Enrollment Enrollment { get; set; }
        public virtual Lesson Lesson { get; set; }
    }
}
