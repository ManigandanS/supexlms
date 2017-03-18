using Lms.Domain.Models.Contents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Scorms
{
    public interface IScormService
    {
        IEnumerable<Scorm> LoadAllScorms(string companyId);
        Scorm GetScormById(string scomrId, string companyId);
        void UploadPowerPoint(string userId, string companyId, string name, string description, string webPath, string physicalPath, string fileName, byte[] pptFile);
        void UploadScorm(string userId, string companyId, string name, string description, string webPath, string physicalPath, string fileName, byte[] zipFile);
        void EditScorm(string userId, string companyId, string scormId, string name, string description);
        void DeleteScorm(string companyId, string userId, string scormId);
    }
}
