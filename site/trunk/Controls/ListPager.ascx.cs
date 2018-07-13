using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace kpfw
{
    public partial class ListPager : System.Web.UI.UserControl
    {
        public event EventHandler PageNumberChange;
        public int PageSize
        {
            get
            {
                if (ViewState["PageSize"] == null)
                    return 10;
                else return (int)ViewState["PageSize"];
            }
            set
            {
                ViewState["PageSize"] = value;
            }
        }
        public int MaxPages
        {
            get
            {
                if (ViewState["MaxPages"] == null)
                    return 30;
                else return (int)ViewState["MaxPages"];
            }
            set
            {
                ViewState["MaxPages"] = value;
            }
        }
        
        private int TotalPages
        {
            get
            {
                if (ViewState["TotalPages"] is int)
                    return (int)ViewState["TotalPages"];
                
                return 1;
            }
            set
            {
                ViewState["TotalPages"] = value;
            }
        }
        
        private int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] is int)
                    return (int)ViewState["PageNumber"];

                return 1;
            }
            set
            {
                ViewState["PageNumber"] = value;
            }
        }
        
        public List<T> ProcessList<T>(List<T> t)
        {
            if (t == null)
                return t;

            TotalPages = (t.Count + PageSize - 1) / PageSize;
            if (TotalPages <= 1)
                return t;

            List<T> tOut = new List<T>();

            if (PageNumber > TotalPages)
            {
                PageNumber = 1;
            }

            for (int n = (PageNumber - 1) * PageSize; n < Math.Min(PageNumber * PageSize, t.Count); n++)
                tOut.Add(t[n]);

            return tOut;
        }
        
        protected void Page_PreRender(object sender, EventArgs e)
        {
            Content.Visible = (TotalPages > 1);

            if (Content.Visible)
            {
                DataTable t = new DataTable();
                t.Columns.Add("Number");
                t.Columns.Add("Text");
                t.Columns.Add("Active", typeof(bool));
                t.Columns.Add("NotActive", typeof(bool));

                int startPage = 1;
                int endPage = TotalPages;

                if (TotalPages > MaxPages)
                {
                    startPage = PageNumber - (MaxPages / 2);
                    if (startPage < 1)
                        startPage = 1;
                    endPage = startPage + MaxPages - 1;
                    if (endPage > TotalPages)
                    {
                        endPage = TotalPages;
                        startPage = endPage - MaxPages + 1;
                        if (startPage < 1)
                            startPage = 1;
                    }
                }

                for (int n = startPage; n <= endPage; n++)
                {
                    DataRow r = t.NewRow();
                    r["Number"] = n.ToString();

                    r["Text"] = n.ToString();
                    if (n == startPage && startPage > 1)
						r["Text"] = "<<";
					if (n == endPage && endPage < TotalPages)
						r["Text"] = ">>";                        

                    r["Active"] = (n == PageNumber);
                    r["NotActive"] = (n != PageNumber);
                    t.Rows.Add(r);
                }

                Pages.DataSource = t;
                Pages.DataBind();
            }
        }
        
        protected void Pages_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            int n = int.Parse((string)e.CommandArgument);
            PageNumber = n;

            PageNumberChange.Invoke(sender, e);
        }
    }
}