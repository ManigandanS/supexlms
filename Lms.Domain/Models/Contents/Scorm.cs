using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Courses;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Models.Contents
{
    public class Scorm : Content
    {
        public Scorm()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Scorm(string companyId, string name, string description, string updatedBy, string webPath, string physicalPath, string manifestXml)
        {
            this.CompanyId = companyId;
            this.Name = name;
            this.Description = description;
            this.UpdatedTs = DateTime.UtcNow;
            this.UpdatedBy = updatedBy;
            this.WebPath = webPath;
            this.PhysicalPath = physicalPath;
            this.ManifestXml = manifestXml;
        }

        public void DeleteScorm()
        {
            if (!this.Lessons.SelectMany(x => x.LessonData).Any())
                Directory.Delete(this.PhysicalPath, true);

            IsDeleted = true;
            UpdatedTs = DateTime.UtcNow;
        }

        public void PublishScorm()
        {
            IsPublished = true;
            UpdatedTs = DateTime.UtcNow;
        }

        public void EditScorm(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }


        public long GetNumberOfEnroll()
        {
            return Lessons.SelectMany(x => x.LessonData).Count();
        }


        public string ManifestXml { get; set; }
        public string WebPath { get; set; }
        public string PhysicalPath { get; set; }

        

        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
