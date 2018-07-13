using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw.Admin
{
    public partial class Pages : System.Web.UI.Page
    {
        protected int? PageId
        {
            get { return ViewState["PageId"] as int?; }
            set { ViewState["PageId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                grdPages.DataSource = ContentPage.List();
                grdPages.DataBind();
            }
        }

        protected void btnAddPage_Click(object sender, EventArgs e)
        {
            PageId = null;

            LoadDetails();

            ListPnl.Visible = false;
            ItemPnl.Visible = true;
        }

        protected void LoadDetails()
        {
            if (PageId == null)
            {
                txtName.Text = "";
                txtContent.Text = "";
                txtUrl.Text = "";
            }
            else
            {
                var cp = ContentPage.Get(PageId.Value);
                txtName.Text = cp.Name;
                txtUrl.Text = cp.Path;
                txtContent.Text = cp.Content;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (!IsValid)
                return;

            var cp = new ContentPage
            {
                Name = txtName.Text,
                Path = txtUrl.Text,
                Content = txtContent.Text
            };

            if (PageId == null)
            {
                cp.Add();
                PageId = cp.Id;
            }
            else
            {
                cp.Id = PageId.Value;
                cp.Set();
            }

            ListPnl.Visible = false;
            ItemPnl.Visible = true;

            grdPages.DataSource = ContentPage.List();
            grdPages.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            PageId = null;

            ListPnl.Visible = true;
            ItemPnl.Visible = false;

            grdPages.DataSource = ContentPage.List();
            grdPages.DataBind();
        }

        protected void grdPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            PageId = (int)grdPages.DataKeys[grdPages.SelectedIndex].Value;

            LoadDetails();

            ListPnl.Visible = false;
            ItemPnl.Visible = true;
        }
    }
}