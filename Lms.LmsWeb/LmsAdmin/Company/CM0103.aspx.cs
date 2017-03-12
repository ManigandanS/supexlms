using Lms.Domain.Models.Exceptions;
using Lms.LmsWeb.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Company
{
    public partial class CM0103 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();
        protected string groupId;

        protected void Page_Load(object sender, EventArgs e)
        {
            groupId = Request.QueryString["grid"];

            if (!IsPostBack)
            {
                var group = GroupService.GetGroupById(SessionVariable.Current.Company.Id, groupId);
                if (group != null)
                {
                    Name.Text = group.Name;
                    Description.Text = group.Description;
                }
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    GroupService.EditGroup(
                        SessionVariable.Current.Company.Id, groupId, Name.Text, Description.Text);
                }
                catch (CompanyException cex)
                {
                    logger.Info(cex.ToString());
                }
            }

            Response.Redirect("CM0101");

        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            GroupService.DeleteGroup(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, groupId);

            Response.Redirect("CM0101");
        }
    }
}