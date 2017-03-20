using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Users;
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
    public partial class AC0003 : AdminPage
    {
        User user;

        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            string userId = Request.QueryString["acid"];
            user = UserService.GetUserById(SessionVariable.Current.Company.Id, userId);

            if (!IsPostBack)
            {
                Group.DataValueField = "Id";
                Group.DataTextField = "Name";
                Group.DataSource = GroupService.LoadAllGroups(SessionVariable.Current.Company.Id);
                Group.DataBind();

                Role.DataValueField = "Id";
                Role.DataTextField = "Name";
                Role.DataSource = UserService.LoadActiveRolls(SessionVariable.Current.Company.Id);
                Role.DataBind();

                if (user != null)
                {
                    Email.Text = user.Email;
                    FirstName.Text = user.DecryptedFirstName;
                    LastName.Text = user.DecryptedLastName;
                    UserType.SelectedValue = ((int)user.UserType).ToString();

                    foreach (ListItem li in Group.Items)
                    {
                        if (user.UserGroups.Any(x => x.GroupId == li.Value))
                            li.Selected = true;
                    }

                    foreach (ListItem li in Role.Items)
                    {
                        if (user.UserRoles.Any(x => x.RoleId == li.Value))
                            li.Selected = true;
                    }

                    EnrollRepeater.DataSource = user.Enrollments.ToList();
                    EnrollRepeater.DataBind();

                    ManagerRepeater.DataSource = user.UserManagers.ToList();
                    ManagerRepeater.DataBind();

                    CertificateRepeater.DataSource = user.UserCertificates.ToList();
                    CertificateRepeater.DataBind();
                }
            }
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {

        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            try
            {
                UserService.DeleteUser(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, user.Id);
            }
            catch (UserException uex)
            {
                logger.Info(uex.ToString());
                CustomValidator1.IsValid = false;
                CustomValidator1.ErrorMessage = uex.Message;
                return;
            }

            Response.Redirect("AC0001");
        }
    }
}