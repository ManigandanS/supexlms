using Lms.Domain.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lms.WebApp.Models
{
    public class BasePage : System.Web.UI.Page
    {
        [Inject]
        public IUnitOfWork UnitOfWork { get; set; }
    }
}