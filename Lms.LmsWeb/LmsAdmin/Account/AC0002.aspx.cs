using Lms.Domain.Models.Exceptions;
using Lms.Domain.Models.Users;
using Lms.Domain.Services.Companies;
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Account
{
    public partial class AC0002 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
        }

        protected void CreateBtn_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    List<string> groups = new List<string>();
                    for (int i = 0; i < Group.Items.Count; i++)
                    {
                        if (Group.Items[i].Selected)
                        {
                            groups.Add(Group.Items[i].Value);
                        }

                    }

                    List<string> roles = new List<string>();
                    for (int i = 0; i < Role.Items.Count; i++)
                    {
                        if (Role.Items[i].Selected)
                        {
                            roles.Add(Role.Items[i].Value);
                        }

                    }


                    logger.Info("[updater: {0}] creates user [email: {1}, firstName: {2}, lastName: {3}, userType: {4}, userGroups: {5}]",
                        SessionVariable.Current.User.Id, Email.Text, FirstName.Text, LastName.Text,
                        (UserTypeEnum)Convert.ToInt32(UserType.SelectedValue), string.Join("...", groups));


                    UserService.RegisterTemporaryUser(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id,
                        Email.Text, TemporaryPassword.Text, FirstName.Text, LastName.Text,
                        (UserTypeEnum)Convert.ToInt32(UserType.SelectedValue), groups, roles);
                }
                catch (UserException uex)
                {
                    logger.Info(uex.ToString());
                    CustomValidator1.IsValid = false;
                    CustomValidator1.ErrorMessage = uex.Message;
                    return;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            Response.Redirect("AC0001");

        }
    }
}