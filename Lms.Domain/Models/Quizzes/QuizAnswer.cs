using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Quizzes
{
    public class QuizAnswer
    {
        public QuizAnswer()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool IsRight { get; set; }

        public bool IsDeleted { get; set; }

        public int Order { get; set; }

        [Required]
        [StringLength(128)]
        public string QuizQuestionId { get; set; }

        public virtual QuizQuestion QuizQuestion { get; set; }
    }
}
