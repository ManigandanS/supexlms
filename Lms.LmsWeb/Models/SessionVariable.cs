using Lms.Domain.Models.Companies;
using Lms.Domain.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lms.LmsWeb.Models
{
    public class SessionVariable
    {

        // Gets the current session.
        public static SessionVariable Current
        {
            get
            {
                var session = (SessionVariable)HttpContext.Current.Session["__MySession__"];
                if (session == null)
                {
                    session = new SessionVariable();
                    HttpContext.Current.Session["__MySession__"] = session;
                }
                return session;
            }
        }

        public User User { get; set; }
        public Company Company { get; set; }
        public List<UserGroup> UserGroups { get; set; }
    }
}