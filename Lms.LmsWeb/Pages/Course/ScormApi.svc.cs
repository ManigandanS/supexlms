using Lms.Domain.Repositories;
using Lms.Domain.Services.Courses;
using Lms.Domain.Services.Scorms;
using Lms.Infrastructure.Repositories;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Lms.LmsWeb.Course
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ScormApi
    {
        readonly static Logger logger = LogManager.GetCurrentClassLogger();
        readonly ILessonService lessonService;
        readonly IUnitOfWork unitOfWork;

        public ScormApi()
        {
            this.unitOfWork = new UnitOfWork();
            this.lessonService = new LessonServiceImpl(unitOfWork);
        }

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool SetValue(string enid, string lsid, string param, string value)
        {
            logger.Debug("userId: " + System.Web.HttpContext.Current.User.Identity.Name);
            logger.Info("Scorm Version 2004 SET Request [enrollementId: " + enid + "], [lessonId: " + lsid + "], [param: " + param + "], [value: " + value + "]");

            lessonService.SetScormData(System.Web.HttpContext.Current.User.Identity.Name, lsid, enid, param, value);
            
            return true;
        }

        // this is api for scorm version 2004
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string GetValue(string enid, string lsid, string param)
        {
            logger.Info("Scorm Version 2004 GET Request [enrollementId: " + enid + "], [lessonId: " + lsid + "], [param: " + param + "]");

            var result = lessonService.GetScormData(System.Web.HttpContext.Current.User.Identity.Name, lsid, enid, param);
            logger.Debug(result);

            return result;

            /*
            //IList<LearnerScormData> dataList = scormDao.Select(where, sqlParams.ToArray(), null);
            LearnerScormData scormData = unitOfWork.LearnerScormDataRepository
                .Find(x => x.ScormId == sco_id && x.CourseRegistrationId == crg_id).FirstOrDefault();


            int interaction_count = 1, objective_count = 1, interaction_objective_count = 1;

            if (scormData != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(scormData.Data);

                foreach (XmlElement el in doc.GetElementsByTagName("element"))
                {
                    if (param == "cmi.interactions._count")
                    {
                        if (el.ChildNodes[0].InnerText.StartsWith("cmi.interactions.") && el.ChildNodes[0].InnerText.EndsWith(".id") && el.ChildNodes[0].InnerText.IndexOf("objectives") == -1) // name
                        {
                            interaction_count++;
                        }
                    }
                    else if (param == "cmi.objectives._count")
                    {
                        if (el.ChildNodes[0].InnerText.StartsWith("cmi.objectives.") && el.ChildNodes[0].InnerText.EndsWith(".id")) // name
                        {
                            objective_count++;
                        }
                    }
                    else if (param == "cmi.interactions.n.objectives._count ")
                    {
                        if (el.ChildNodes[0].InnerText.StartsWith("cmi.interactions.") && el.ChildNodes[0].InnerText.EndsWith(".id") && el.ChildNodes[0].InnerText.IndexOf("objectives") != -1) // name
                        {
                            objective_count++;
                        }
                    }
                    else
                    {
                        if (el.ChildNodes[0].InnerText == param) // name
                        {
                            json = el.ChildNodes[1].InnerText;
                        }
                    }
                }
            }
            else
            {
                json = "";
            }

            if (param == "cmi.interactions._count")
            {
                json = interaction_count.ToString();
            }
            else if (param == "cmi.objectives._count")
            {
                json = objective_count.ToString();
            }
            else if (param == "cmi.interactions.n.objectives._count")
            {
                json = interaction_objective_count.ToString();
            }

            logger.Info("Scorm Version 2004 GET Request Return: " + json);
            */
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void Commit(string enid, string lsid)
        {
            logger.Info("Scorm Version 1.2 Commit Request [enrollementId: " + enid + "], [lessonId: " + lsid + "]");

            lessonService.CommitScormData(System.Web.HttpContext.Current.User.Identity.Name, lsid, enid);
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public bool LmsSetValue(string enid, string lsid, string param, string value)
        {
            logger.Debug("userId: " + System.Web.HttpContext.Current.User.Identity.Name);
            logger.Info("Scorm Version 1.2 SET Request [enrollementId: " + enid + "], [lessonId: " + lsid + "], [param: " + param + "], [value: " + value + "]");

            lessonService.SetScormData(System.Web.HttpContext.Current.User.Identity.Name, lsid, enid, param, value);
            
            return true;
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public string LmsGetValue(string enid, string lsid, string param)
        {
            logger.Info("Scorm Version 1.2 GET Request [enrollementId: " + enid + "], [lessonId: " + lsid + "], [param: " + param + "]");

            var result = lessonService.GetScormData(System.Web.HttpContext.Current.User.Identity.Name, lsid, enid, param);
            logger.Debug(result);

            return result;

            /*
            LearnerScormData scormData = unitOfWork.LearnerScormDataRepository
                .Find(x => x.ScormId == sco_id && x.CourseRegistrationId == crg_id).FirstOrDefault();


            int interaction_count = 1, objective_count = 1, interaction_objective_count = 1;

            if (scormData != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(scormData.Data);

                foreach (XmlElement el in doc.GetElementsByTagName("element"))
                {
                    if (param == "cmi.interactions._count")
                    {
                        if (el.ChildNodes[0].InnerText.StartsWith("cmi.interactions.") && el.ChildNodes[0].InnerText.EndsWith(".id") && el.ChildNodes[0].InnerText.IndexOf("objectives") == -1) // name
                        {
                            interaction_count++;
                        }
                    }
                    else if (param == "cmi.objectives._count")
                    {
                        if (el.ChildNodes[0].InnerText.StartsWith("cmi.objectives.") && el.ChildNodes[0].InnerText.EndsWith(".id")) // name
                        {
                            objective_count++;
                        }
                    }
                    else if (param == "cmi.interactions.n.objectives._count ")
                    {
                        if (el.ChildNodes[0].InnerText.StartsWith("cmi.interactions.") && el.ChildNodes[0].InnerText.EndsWith(".id") && el.ChildNodes[0].InnerText.IndexOf("objectives") != -1) // name
                        {
                            objective_count++;
                        }
                    }
                    else
                    {
                        if (el.ChildNodes[0].InnerText == param) // name
                        {
                            json = el.ChildNodes[1].InnerText;
                        }
                    }
                }
            }
            else
            {
                json = "";
            }

            if (param == "cmi.interactions._count")
            {
                json = interaction_count.ToString();
            }
            else if (param == "cmi.objectives._count")
            {
                json = objective_count.ToString();
            }
            else if (param == "cmi.interactions.n.objectives._count")
            {
                json = interaction_objective_count.ToString();
            }

            logger.Info("Scorm Version 12 GET Request Return: " + json);
            */
        }

        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public void LmsCommit(string enid, string lsid)
        {
            logger.Info("Scorm Version 1.2 Commit Request [enrollementId: " + enid + "], [lessonId: " + lsid + "]");

            lessonService.CommitScormData(System.Web.HttpContext.Current.User.Identity.Name, lsid, enid);
        }
    }
}
