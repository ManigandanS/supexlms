using Lms.Domain.Services.Companies;
using Lms.Domain.Services.Scorms;
using Lms.LmsWeb.Models;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.LmsAdmin.Account
{
    public partial class AC0101 : AdminPage
    {
        static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RoleRepeater.DataSource = UserService.LoadActiveRolls(SessionVariable.Current.Company.Id);
                RoleRepeater.DataBind();
            }
        }

        protected void DelBtn_Command(object sender, CommandEventArgs e)
        {
            try
            {
                string roleId = e.CommandArgument as string;
                UserService.DeleteRole(SessionVariable.Current.Company.Id, SessionVariable.Current.User.Id, roleId);
            }
            catch (DbEntityValidationException devex)
            {
                foreach (var eve in devex.EntityValidationErrors)
                {
                    logger.Error("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Error("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }

            Response.Redirect(Request.RawUrl);
        }
    }
}