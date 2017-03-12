using Lms.Domain.Models.Certificates;
using Lms.Domain.Models.Companies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Courses
{
    public enum CourseLocationEnum
    {
        Online,
        Offline,
        Both
    }

    public enum CourseTypeEnum
    {
        Intenral,
        External
    }

    public enum CourseAccessEnum
    {
        InternalUsersOnly,
        ExtenralUsersOnly,
        BothUsers
    }

    public class Course
    {
        

        public Course()
        {
            this.Id = Guid.NewGuid().ToString();
        }


        public Course(string companyId, string name, string descrpition,
            string courseType, string courseLocation, string courseAccess)
            : this()
        {
            this.CompanyId = companyId;
            this.Name = name;
            this.Description = descrpition;
            this.CourseLocation = (CourseLocationEnum)(Convert.ToInt32(courseLocation));
            this.CourseType = (CourseTypeEnum)(Convert.ToInt32(courseType));
            this.CourseAccess = (CourseAccessEnum)(Convert.ToInt32(courseAccess));
            this.UpdatedTs = DateTime.UtcNow;
        }

        public Course(string companyId, string name, string descrpition,
            string courseType, string courseLocation, string courseAccess, string certificateId)
            : this()
        {
            this.CompanyId = companyId;
            this.Name = name;
            this.Description = descrpition;
            this.CourseLocation = (CourseLocationEnum)(Convert.ToInt32(courseLocation));
            this.CourseType = (CourseTypeEnum)(Convert.ToInt32(courseType));
            this.CourseAccess = (CourseAccessEnum)(Convert.ToInt32(courseAccess));
            this.UpdatedTs = DateTime.UtcNow;
            this.CertificateId = certificateId;
        }

        public void Update(string courseName, string courseDescription, string courseType, string courseLocation, string courseAccess)
        {
            this.Name = courseName;
            this.Description = courseDescription;
            this.CourseLocation = (CourseLocationEnum)(Convert.ToInt32(courseLocation));
            this.CourseType = (CourseTypeEnum)(Convert.ToInt32(courseType));
            this.CourseAccess = (CourseAccessEnum)(Convert.ToInt32(courseAccess));
            this.UpdatedTs = DateTime.UtcNow;
        }

        public void Update(string courseName, string courseDescription, string courseType, string courseLocation, string courseAccess, string certificateId)
        {
            this.Name = courseName;
            this.Description = courseDescription;
            this.CourseLocation = (CourseLocationEnum)(Convert.ToInt32(courseLocation));
            this.CourseType = (CourseTypeEnum)(Convert.ToInt32(courseType));
            this.CourseAccess = (CourseAccessEnum)(Convert.ToInt32(courseAccess));
            this.UpdatedTs = DateTime.UtcNow;
            this.CertificateId = certificateId;
        }

        public void Unpublish()
        {
            this.IsPublished = false;
        }

        public void Publish()
        {
            this.IsPublished = true;
            this.PublishedTs = DateTime.UtcNow;
        }

        public List<Lesson> LoadActiveLessons()
        {
            return this.Lessons.Where(x => !x.IsDeleted).OrderBy(x => x.Order).ToList();
        }

        public List<Session> LoadActiveSessions()
        {
            return this.Sessions.Where(x => !x.IsDeleted).ToList(); // && x.EnrollStart <= DateTime.UtcNow && x.EnrollEnd >= DateTime.UtcNow).ToList();
        }

        public void AddSession(string sessionName, string description, double? cost, DateTime sessionStartDate, DateTime sessionEndDate, DateTime enrollmentStartDate, DateTime enrollmentEndDate)
        {
            if (this.Sessions == null)
                this.Sessions = new List<Session>();

            Session session = new Session(sessionName, description, cost, sessionStartDate, sessionEndDate, enrollmentStartDate, enrollmentEndDate);
            this.Sessions.Add(session);
        }

        private int GetNewLessonOrder()
        {
            return this.Lessons.Count(x => !x.IsDeleted) + 1;
        }

        public void AddScormLesson(string scormId, string name, string description)
        {
            if (this.Lessons == null)
                this.Lessons = new List<Lesson>();

            Lesson lesson = new Lesson(this.Id, LessonTypeEnum.Content, scormId, name, description);
            lesson.Order = GetNewLessonOrder();
            this.Lessons.Add(lesson);
        }

        public void AddQuizLesson(string quizId, string name, string description)
        {
            if (this.Lessons == null)
                this.Lessons = new List<Lesson>();

            Lesson lesson = new Lesson(this.Id, LessonTypeEnum.Quiz, quizId, name, description);
            lesson.Order = GetNewLessonOrder();
            this.Lessons.Add(lesson);
        }

        public void AddExternalLesson(string name, string description)
        {
            Lesson lesson = new Lesson(this.Id, LessonTypeEnum.External, null, name, description);
            lesson.Order = GetNewLessonOrder();
            this.Lessons.Add(lesson);
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedTs { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedTs { get; set; }

        public string CertificateId { get; set; }

        public CourseLocationEnum CourseLocation { get; set; }
        public CourseTypeEnum CourseType { get; set; }
        public CourseAccessEnum CourseAccess { get; set; }

        public string CourseAccessDisplay
        {
            get
            {
                switch (CourseAccess)
                {
                    case CourseAccessEnum.InternalUsersOnly:
                        return "Internal Users Only";
                    case CourseAccessEnum.ExtenralUsersOnly:
                        return "External Users Only";
                    case CourseAccessEnum.BothUsers:
                        return "Both Users";
                    default:
                        return "N/A";
                }
            }
        }


        public virtual Company Company { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<Session> Sessions { get; set; }
        public virtual Certificate Certificate { get; set; }
    }
}
