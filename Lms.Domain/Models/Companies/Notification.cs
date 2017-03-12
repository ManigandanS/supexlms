using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Companies
{
    public class Notification
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public Notification(string companyId, string updaterId, string title, string details, DateTime startDate, DateTime endDate) : this()
        {
            this.CompanyId = companyId;
            this.UpdatedBy = updaterId;
            this.Title = title;
            this.Details = details;
            this.StartDate = startDate;
            this.EndDate = endDate.AddDays(1).AddMinutes(-1);
            this.UpdatedTs = DateTime.UtcNow;
        }

        public void UpdateNotification(string updaterId, string title, string details, DateTime startDate, DateTime endDate)
        {
            this.UpdatedBy = updaterId;
            this.Title = title;
            this.Details = details;
            this.StartDate = startDate;
            this.EndDate = endDate.AddDays(1).AddMinutes(-1);
            this.UpdatedTs = DateTime.UtcNow;
        }

        [Key]
        [StringLength(128)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Details { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime UpdatedTs { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [StringLength(128)]
        public string UpdatedBy { get; set; }

        [Required]
        [StringLength(128)]
        public string CompanyId { get; set; }

        public virtual Company Company { get; set; }
        [ForeignKey("UpdatedBy")]
        public virtual User User { get; set; }
    }
}
