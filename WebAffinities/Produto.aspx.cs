using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAffinities
{
    public partial class Produto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dtt = new DataTable();
            dtt.Columns.Add(new DataColumn("ORDEM"));
            dtt.Columns.Add(new DataColumn("HIERARQUIA"));
            dtt.Columns.Add(new DataColumn("TIPO"));
            dtt.Columns.Add(new DataColumn("OBRIGATORIO"));
            for (int i = 1; i < 10; i++)
            {
                DataRow dr = dtt.NewRow();
                dtt.Rows.Add(dr);
                dr["ORDEM"] = i.ToString();
                dr["HIERARQUIA"] = "APÓLICE";
                dr["TIPO"] = "TEXTO";
                dr["OBRIGATORIO"] = true;
            }

            gdvLayout.DataSource = dtt;
            gdvLayout.DataBind();
        }
    }
}