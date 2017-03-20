using Lms.Domain.Models.Courses;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Account
{
    public partial class AC0005 : AdminPage
    {
        protected string accountId;
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            accountId = Request.QueryString["acid"];
            SearchControl1.SearchButtonClick += new EventHandler(SearchBtn_Click);

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            accountId = Request.QueryString["acid"];

            Repeater1.DataSource = UserService.FindManagersByName(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, SearchControl1.Keyword);
            Repeater1.DataBind();
        }

        protected void AddBtn_Command(object sender, CommandEventArgs e)
        {
            string managerId = e.CommandArgument as string;

            UserService.AddManager(SessionVariable.Current.Company.Id, accountId, managerId);

            Response.Redirect("AC0003?acid=" + accountId);
        }
    }
}