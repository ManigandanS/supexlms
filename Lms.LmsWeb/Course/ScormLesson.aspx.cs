using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;

namespace Lms.LmsWeb.Course
{
    public partial class ScormLesson : SecurePage
    {
        protected string enrollmentId, lessonId;
        protected string scormUrl;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            enrollmentId = Request.QueryString["enid"];
            lessonId = Request.QueryString["lsid"];

            var lesson = LessonService.GetLessonById(lessonId, enrollmentId);
            logger.Debug(lesson.Scorm.ManifestXml);
            var xmlDoc = XDocument.Parse(lesson.Scorm.ManifestXml);
            XNamespace ns = xmlDoc.Root.GetDefaultNamespace();
            foreach (XElement element in xmlDoc.Descendants(ns + "resource"))
            {
                var href = element.Attribute("href");
                logger.Debug("element: {0}, attribute: {1}, ns: {2}", element.Name, href, ns);

                if (href != null)
                {
                    scormUrl = string.Format("/{0}/{1}", lesson.Scorm.WebPath, href.Value);
                    break;
                }
            }

            logger.Info(scormUrl);
 
        }
    }
}