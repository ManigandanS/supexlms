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

namespace Lms.LmsWeb.LmsAdmin.Quiz
{
    public partial class QZ0002 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            QuizService.CreateQuiz(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, 
                    NameTextBox.Text, DescriptionTextBox.Text, float.Parse(PassPercentTextBox.Text));

            Response.Redirect("QZ0001");
        }
    }
}