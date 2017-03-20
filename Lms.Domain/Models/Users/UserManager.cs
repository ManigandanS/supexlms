using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Users
{
    public class UserManager
    {
        public UserManager()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(128)]
        public string Id { get; set; }

        [StringLength(128)]
        [Required]
        public string UserId { get; set; }

        [StringLength(128)]
        [Required]
        public string ManagerId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ManagerId")]
        public virtual User Manager { get; set; }
    }
}
