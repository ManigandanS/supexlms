using Lms.Domain.Models.Exceptions;
using Lms.Domain.Services.Scorms;
using Lms.Domain.Services.Users;
using Lms.LmsWeb.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Account
{
    public partial class AC0001 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Repeater1.DataSource = UserService.LoadAllUsers(SessionVariable.Current.Company.Id);
                Repeater1.DataBind();
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {

        }
    }
}