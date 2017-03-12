using DigitalOfficePro.Html5PointSdk;
using Ionic.Zip;
using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Contents;
using Lms.Domain.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lms.Domain.Services.Scorms
{
    public class ScormServiceImpl : IScormService
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        protected readonly IUnitOfWork unitOfWork;

        private ScormServiceImpl()
        {

        }

        public ScormServiceImpl(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Scorm GetScormById(string scomrId, string companyId)
        {
            return unitOfWork.ScormRepository.GetAll().SingleOrDefault(x => x.Id == scomrId && x.CompanyId == companyId);
        }

        public IEnumerable<Scorm> LoadAllScorms(string companyId)
        {
            return unitOfWork.ScormRepository.GetAll().Where(x => x.IsDeleted == false && x.CompanyId == companyId).ToList();
        }

        public IEnumerable<Scorm> LoadPublishedScorms(string companyId)
        {
            return unitOfWork.ScormRepository.GetAll().Where(x => x.IsDeleted == false && x.CompanyId == companyId && x.IsPublished).ToList();
        }

        private int GetNumberOfScorm(string companyId)
        {
            return unitOfWork.ScormRepository.GetAll().Where(x => x.CompanyId == companyId && !x.IsDeleted).Count();
        }

        public void UploadPowerPoint(string userId, string companyId, string name, string description, string webPath, string physicalPath, string fileName, byte[] pptFile)
        {
            try
            {
                var company = unitOfWork.CompanyRepository.GetById(companyId);
                var scormNum = GetNumberOfScorm(companyId) + 1;

                if (company.IsTrial && scormNum > 2)
                {
                    throw new ScormException("You can upload only one scorm file under the trial version");
                }

                //Create powerpoint to html5 converter object.
                PresentationConverter presentationConverter = new PresentationConverter();
                //Initialize library using userName and productKey in case of redistributable license.
                //presentationConverter.InitLibrary("userName", "productKey");


                //Update conversion settings
                presentationConverter.Settings.CreateDirectoryForOutput = false;

                //Output settings 
                presentationConverter.Settings.Output.AdvanceOnMouseClick = true;
                presentationConverter.Settings.Output.BackgroundColor = -16777216;//ARGB of black.
                presentationConverter.Settings.Output.EmbedFonts = true;
                presentationConverter.Settings.Output.FitToWindow = true;
                presentationConverter.Settings.Output.IncludeHiddenSlides = true;
                presentationConverter.Settings.Output.WindowScale = 100;
                presentationConverter.Settings.CreateDirectoryForOutput = false;

                //For LMS output
                presentationConverter.Settings.Lms = new LmsSettings();
                presentationConverter.Settings.Lms.LmsType = LmsType.Scorm2NdEdition2004;
                presentationConverter.Settings.Lms.CourseTitle = name;
                presentationConverter.Settings.Lms.Description = description;
                
                //For thumbnail generation
                //presentationConverter.Settings.Thumbnail = new ThumbnailSettings();
                //presentationConverter.Settings.Thumbnail.Format = ThumbnailImageFormat.Jpg;
                //presentationConverter.Settings.Thumbnail.Scale = 10;

                //To add company logo
                //presentationConverter.Settings.Logo = new LogoSettings();
                //presentationConverter.Settings.Logo.Left = 10;
                //presentationConverter.Settings.Logo.Top = 10;
                //presentationConverter.Settings.Logo.Width = 200;
                //presentationConverter.Settings.Logo.Height = 100;
                //presentationConverter.Settings.Logo.Hyperlink = "http://www.digitalofficepro.com";
                //presentationConverter.Settings.Logo.ImagePath = @"C:\Users\user\Documents\Logo.png";

                //For custom player
                //presentationConverter.Settings.Player = new PlayerSettings();
                //presentationConverter.Settings.Player.Path =@"C:\Program Files (x86)\DigitalOfficePro\HTML5PointSDK\Players\classic";


                //Save ppt file on temparay directory
                string tempPath = GetTemporaryDirectory();
                string tempFile = Path.Combine(tempPath, fileName);
                string randomPath = Path.GetRandomFileName();

                File.WriteAllBytes(tempFile, pptFile);                

                
                //Open persenattion
                presentationConverter.OpenPresentation(tempFile);
                logger.Info("PowerPoint presentation '{0}' is opened.", tempFile);
                string outputHtmlFile = Path.Combine(tempPath, randomPath);
                logger.Debug("SCORM file path: " + outputHtmlFile);
                logger.Debug("SCORM zip file: " + outputHtmlFile + ".zip");

                //HTML5 conversion
                presentationConverter.Convert(outputHtmlFile);

                logger.Info("Converted presentation '{0}' to '{1}'.", tempFile, outputHtmlFile);
                presentationConverter.ClosePresentation();
                logger.Debug("Presentation closed");

                //output file is PowerPoint2Scorm.zip
                
                // real path to save
                physicalPath = Path.Combine(physicalPath, randomPath);
                webPath = webPath + "/" + randomPath;
                Directory.CreateDirectory(physicalPath);

                // UNZIP
                using (ZipFile zip = ZipFile.Read(outputHtmlFile + ".zip"))
                {
                    zip.ExtractAll(physicalPath);
                }

                // DELETE
                Directory.Delete(tempPath, true);

                string manifestXml = File.ReadAllText(Path.Combine(physicalPath, "imsmanifest.xml"));

                unitOfWork.ScormRepository.Insert(new Scorm
                    {
                        CompanyId = companyId,
                        Description = description,
                        Name = name,
                        PhysicalPath = physicalPath,
                        UpdatedBy = userId,
                        UpdatedTs = DateTime.UtcNow,
                        WebPath = webPath,
                        ManifestXml = manifestXml
                    });

                unitOfWork.SaveChanges();
            } 
            catch (Exception ex)
            {
                try
                {
                    Directory.Delete(physicalPath, true);
                }
                catch (Exception)
                {

                }

                logger.Error(ex.ToString());
                throw ex;
            }

        }

        

        public void UploadScorm(string userId, string companyId, string name, string description, string webPath, string physicalPath, string fileName, byte[] zipFile)
        {
            try
            {
                var company = unitOfWork.CompanyRepository.GetById(companyId);
                var scormNum = GetNumberOfScorm(companyId) + 1;

                if (company.IsTrial && scormNum > 3)
                {
                    throw new ScormException("You can upload only three scorm files under the trial version");
                }

                string tempPath = GetTemporaryDirectory();
                string tempFile = Path.Combine(tempPath, fileName);

                File.WriteAllBytes(tempFile, zipFile);

                string randomPath = Path.GetRandomFileName();
                // real path to save
                physicalPath = Path.Combine(physicalPath, randomPath);
                webPath = webPath + "/" + randomPath;
                Directory.CreateDirectory(physicalPath);

                // UNZIP
                using (ZipFile zip = ZipFile.Read(tempFile))
                {
                    zip.ExtractAll(physicalPath);
                }

                // DELETE
                Directory.Delete(tempPath, true);

                string manifestXml = File.ReadAllText(Path.Combine(physicalPath, "imsmanifest.xml"));

                unitOfWork.ScormRepository.Insert(new Scorm
                    {
                        CompanyId = companyId,
                        Description = description,
                        Name = name,
                        PhysicalPath = physicalPath,
                        UpdatedBy = userId,
                        UpdatedTs = DateTime.UtcNow,
                        WebPath = webPath,
                        ManifestXml = manifestXml
                    });

                unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                try
                {
                    Directory.Delete(physicalPath, true);
                }
                catch (Exception)
                {

                }

                logger.Error(ex.ToString());
                throw ex;
            }
        }

        public void EditScorm(string userId, string companyId, string scormId, string name, string description)
        {
            var scorm = unitOfWork.ScormRepository.GetById(scormId);
            if (scorm.CompanyId == companyId)
            {
                scorm.EditScorm(name, description);
                unitOfWork.ScormRepository.Update(scorm);
                unitOfWork.SaveChanges();
            }
        }

        public void DeleteScorm(string companyId, string userId, string scormId)
        {
            var scorm = unitOfWork.ScormRepository.GetById(scormId);
            if (scorm.CompanyId == companyId)
            {
                
                scorm.DeleteScorm();
                unitOfWork.ScormRepository.Update(scorm);
                unitOfWork.SaveChanges();
            }
        }

        public void PublishScorm(string companyId, string userId, string scormId)
        {
            var scorm = unitOfWork.ScormRepository.GetById(scormId);
            if (scorm.CompanyId == companyId)
            {
                scorm.PublishScorm();
                unitOfWork.ScormRepository.Update(scorm);
                unitOfWork.SaveChanges();
            }
        }

        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            return tempDirectory;
        }
    }
}
