using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Quizzes
{
    public enum QuizQuestionTypeEnum
    {
        SingleSelect,
        MultiSelect,
        Keyword
    }

    public class QuizQuestion
    {
        public QuizQuestion()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public QuizQuestion(string quizId, string title, List<QuizAnswer> selectAnswers, QuizQuestionTypeEnum type)
            : this()
        {
            this.QuizId = quizId;
            this.Title = title;
            this.Type = type;
            this.CreatedTs = DateTime.UtcNow;
            this.UpdatedTs = DateTime.UtcNow;

            this.QuizAnswers = new List<QuizAnswer>();
            foreach (var answer in selectAnswers)
            {
                answer.QuizQuestionId = this.Id;
                this.QuizAnswers.Add(answer);
            }
        }

        public QuizQuestion(string quizId, string title, string keywordAnswer, QuizQuestionTypeEnum type) : this()
        {
            this.QuizId = quizId;
            this.Title = title;
            this.Type = type;
            this.CreatedTs = DateTime.UtcNow;
            this.UpdatedTs = DateTime.UtcNow;

            this.QuizAnswers = new List<QuizAnswer>();
            this.QuizAnswers.Add(new QuizAnswer()
                {
                    IsDeleted = false,
                    IsRight = true,
                    QuizQuestionId = this.Id,
                    Title = keywordAnswer,
                    Order = 1
                });
        }


        public void Update(string questionTitle, List<QuizAnswer> answers, QuizQuestionTypeEnum type)
        {
            this.Title = questionTitle;
            this.UpdatedTs = DateTime.UtcNow;
            this.Type = type;

            foreach (var item in QuizAnswers)
            {
                item.IsDeleted = true;
            }

            foreach (var answer in answers)
            {
                answer.QuizQuestionId = this.Id;
                this.QuizAnswers.Add(answer);
            }
        }

        public void Update(string questionTitle, string keywordAnswer, QuizQuestionTypeEnum type)
        {
            this.Title = questionTitle;
            this.UpdatedTs = DateTime.UtcNow;
            this.Type = type;

            foreach (var item in QuizAnswers)
            {
                item.IsDeleted = true;
            }

            this.QuizAnswers.Add(new QuizAnswer()
            {
                IsDeleted = false,
                IsRight = true,
                QuizQuestionId = this.Id,
                Title = keywordAnswer,
                Order = 1
            });
        }

        public IEnumerable<QuizAnswer> LoadActiveQuizAnswer()
        {
            return this.QuizAnswers.Where(x => !x.IsDeleted).OrderBy(x => x.Order);
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsDeleted { get; set; }

        public QuizQuestionTypeEnum Type { get; set; }

        public int Order { get; set; }

        public DateTime CreatedTs { get; set; }
        public DateTime UpdatedTs { get; set; }

        public string TypeDisplay
        {
            get
            {
                switch (Type)
                {
                    case QuizQuestionTypeEnum.MultiSelect:
                        return "Multi Select";
                    case QuizQuestionTypeEnum.SingleSelect:
                        return "Single Select";
                    default:
                        return Type.ToString();
                }
            }
        }

        [Required]
        [StringLength(128)]
        public string QuizId { get; set; }

        public virtual Quiz Quiz { get; set; }
        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; }
    }
}
