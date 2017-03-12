using Lms.LmsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.UserControls
{
    public partial class KeywordAnswer : BaseUserControl
    {
        protected string quizId, questionId;

        public string Answer
        {
            get { return UserAnswerText.Text; }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Initialize(string quizId, string questionId)
        {
            this.quizId = quizId;
            this.questionId = questionId;
        }
    }
}