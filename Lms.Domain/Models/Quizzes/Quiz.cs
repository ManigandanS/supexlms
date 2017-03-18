using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Quizzes
{
    public class Quiz
    {
        public Quiz()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Quiz(string userId, string title, string description, float passPercent)
            : this()
        {
            this.Title = title;
            this.Description = description;
            this.UpdatedBy = userId;
            this.PassPercent = passPercent;
            this.CreatedTs = DateTime.UtcNow;
            this.UpdatedTs = DateTime.UtcNow;
            this.MaxTake = int.MaxValue;
        }

        public void Delete()
        {
            this.IsDeleted = true;
            this.UpdatedTs = DateTime.UtcNow;
        }

        

        public void AddQuestion(QuizQuestion quizQuestion)
        {
            if (this.QuizQuestions == null)
                this.QuizQuestions = new List<QuizQuestion>();

            int maxOrder = this.QuizQuestions.Count(x => !x.IsDeleted);
            quizQuestion.Order = maxOrder + 1;
            this.QuizQuestions.Add(quizQuestion);
        }

        public void UpdateQuiz(string name, string description, float passPercent)
        {
            this.Title = name;
            this.Description = description;
            this.PassPercent = passPercent;
        }

        public IEnumerable<QuizQuestion> LoadActiveQuizQuestions()
        {
            return this.QuizQuestions.Where(x => !x.IsDeleted).OrderBy(x => x.Order);
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public string Title { get; set; }

        public float PassPercent { get; set; }

        public int MaxTake { get; set; }

        public DateTime CreatedTs { get; set; }

        public DateTime UpdatedTs { get; set; }

        [Required]
        [StringLength(128)]
        public string UpdatedBy { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<QuizQuestion> QuizQuestions { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User Updater { get; set; }

    }
}
