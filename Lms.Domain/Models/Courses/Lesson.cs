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

namespace Lms.Domain.Models.Courses
{
    public enum LessonTypeEnum
    {
        Content,
        Quiz,
        Assignment,
        External
    }

    public class Lesson
    {
        public Lesson()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        
        public Lesson(string courseId, LessonTypeEnum lessonType, string contentId, string name, string description) : this()
        {
            this.CourseId = courseId;
            this.LessonType = lessonType;
            this.Name = name;
            this.Description = description;
            this.UpdatedTs = DateTime.UtcNow;
            switch (lessonType)
            {
                case LessonTypeEnum.Content:
                    ScormId = contentId;
                    break;
                case LessonTypeEnum.Quiz:
                    QuizId = contentId;
                    break;
                case LessonTypeEnum.Assignment:
                    break;
                default:
                    break;
            }
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string CourseId { get; set; }

        public int Order { get; set; }
        public bool IsDeleted { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedTs { get; set; }

        public LessonTypeEnum LessonType { get; set; }

        [StringLength(128)]
        public string ScormId { get; set; }

        [StringLength(128)]
        public string QuizId { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<ScormData> ScormData { get; set; }
        public virtual ICollection<QuizData> QuizData { get; set; }
        [ForeignKey("ScormId")]
        public virtual Scorm Scorm { get; set; }
        [ForeignKey("QuizId")]
        public virtual Quiz Quiz { get; set; }
    }
}
