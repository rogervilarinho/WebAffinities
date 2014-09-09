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
            if (!IsPostBack)
            {
                DataTable dtt = new DataTable();
                dtt.Columns.Add(new DataColumn("ORDEM"));
                dtt.Columns.Add(new DataColumn("HIERARQUIA"));
                dtt.Columns.Add(new DataColumn("TIPO"));
                dtt.Columns.Add(new DataColumn("OBRIGATORIO", typeof(Boolean)));
                dtt.Columns.Add(new DataColumn("TAMANHO"));
                dtt.Columns.Add(new DataColumn("INICIO"));
                dtt.Columns.Add(new DataColumn("FIM"));
                for (int i = 1; i < 10; i++)
                {
                    DataRow dr = dtt.NewRow();
                    dtt.Rows.Add(dr);
                    dr["ORDEM"] = i.ToString();
                    dr["HIERARQUIA"] = "APÓLICE";
                    dr["TIPO"] = "TEXTO";
                    dr["OBRIGATORIO"] = true;
                    dr["TAMANHO"] = i.ToString();
                    dr["INICIO"] = i;
                    dr["FIM"] = i + i;
                }

                gdvLayout.DataSource = dtt;
                gdvLayout.DataBind();
            }
        }

        protected void ddlAutoCompletar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (DropDownList)sender;
            
            //PEGA A LINHA DO GRID ALTERADA
            GridViewRow row = (GridViewRow) ddl.Parent.Parent;

            //PEGA O INDEX ALTERADO
            int index = row.RowIndex;

            TextBox tbx = (TextBox) gdvLayout.Rows[index].FindControl("tbxCaracter");
            DropDownList ddlDireacao = (DropDownList)gdvLayout.Rows[index].FindControl("ddlDirecao");

            if (ddl.SelectedValue.Equals("1"))
            {
                tbx.Enabled = true;
                ddlDireacao.Enabled = true;
                tbx.Focus();
            }
            else
            {
                tbx.Enabled = false;
                ddlDireacao.Enabled = false;
                ddlDireacao.SelectedIndex = 0;
                tbx.Text = string.Empty;
            }
        }
    }
}