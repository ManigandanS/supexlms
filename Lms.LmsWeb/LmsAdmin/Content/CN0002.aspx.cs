using Lms.Domain.Models.Exceptions;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Content
{
    public partial class CN0002 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile && IsValid)
            {
                BinaryReader b = new BinaryReader(FileUpload1.PostedFile.InputStream);
                byte[] binData = b.ReadBytes(FileUpload1.PostedFile.ContentLength);

                string webPath = "data";
                string physicalPath = Server.MapPath("/data");

                logger.Debug("SessionVariable.Current: " + SessionVariable.Current.User);

                logger.Info("new SCORM create: [name: {0}], [desc: {1}], [type: {2}], [user: {3}], [company: {4}]",
                    Name.Text, Description.Text, Type.SelectedValue, 
                    SessionVariable.Current.User.Id, SessionVariable.Current.Company.Id);


                try
                {
                    if (Type.SelectedValue == "SCORM")
                    {
                        ScormService.UploadScorm(SessionVariable.Current.User.Id, SessionVariable.Current.Company.Id,
                            Name.Text, Description.Text, webPath, physicalPath, FileUpload1.PostedFile.FileName, binData);
                    }

                    if (Type.SelectedValue == "PPT")
                    {
                        ScormService.UploadPowerPoint(SessionVariable.Current.User.Id, SessionVariable.Current.Company.Id,
                            Name.Text, Description.Text, webPath, physicalPath, FileUpload1.PostedFile.FileName, binData);
                    }
                }
                catch (ScormException sex)
                {

                }

                Response.Redirect("CN0001");
            }
        }
    }
}