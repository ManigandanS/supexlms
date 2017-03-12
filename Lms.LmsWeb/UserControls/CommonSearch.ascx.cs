using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lms.LmsWeb.UserControls
{
    public partial class CommonSearch : System.Web.UI.UserControl
    {
        public event EventHandler SearchButtonClick;

        public string Keyword
        {
            get { return SearchKeyword.Text; }
            set { SearchKeyword.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchButtonClick(sender, e);
        }
    }
}