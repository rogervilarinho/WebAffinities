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
                ViewState.Add("LAYOUT", dtt);
                //dtt.Columns.Add(new DataColumn("ORDEM"));
                //dtt.Columns.Add(new DataColumn("HIERARQUIA"));
                //dtt.Columns.Add(new DataColumn("TIPO"));
                //dtt.Columns.Add(new DataColumn("OBRIGATORIO", typeof(Boolean)));
                //dtt.Columns.Add(new DataColumn("TAMANHO"));
                //dtt.Columns.Add(new DataColumn("INICIO"));
                //dtt.Columns.Add(new DataColumn("FIM"));
                //for (int i = 1; i < 10; i++)
                //{
                //    DataRow dr = dtt.NewRow();
                //    dtt.Rows.Add(dr);
                //    dr["ORDEM"] = i.ToString();
                //    dr["HIERARQUIA"] = "APÓLICE";
                //    dr["TIPO"] = "TEXTO";
                //    dr["OBRIGATORIO"] = true;
                //    dr["TAMANHO"] = i.ToString();
                //    dr["INICIO"] = i;
                //    dr["FIM"] = i + i;
                //}

                //gdvLayout.DataSource = dtt;
                //gdvLayout.DataBind();
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

        /// <summary>
        /// ATUALIZA TODOS OS TAMANHOS QUANDO O USUÁRIO ALTERAR O CAMPO
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void tbxTamanho_TextChanged(object sender, EventArgs e)
        {           
            ////PEGA O VALOR INSERIDO NO TAMANHO
            TextBox tbx = (TextBox)sender;

            ////PEGA A LINHA DO GRID
            //GridViewRow row = (GridViewRow) tbx.Parent.Parent;

            //INICIA A POSIÇÃO DA PRIMEIRA LINHA
            SetarPosicaoLinhaUm();

            if (!String.IsNullOrEmpty(tbx.Text))
            {
                SetarPosicoesGRID();
            }
        }

        private void SetarPosicoesGRID()
        {
            int posicao = 1;
            foreach (GridViewRow row in gdvLayout.Rows)
            {
                TextBox tbxInicio = (TextBox)row.FindControl("tbxInicio");
                TextBox tbxTamanho = (TextBox)row.FindControl("tbxTamanho");
                TextBox tbxFim = (TextBox)row.FindControl("tbxFim");

                int tamanho = Convert.ToInt32(tbxTamanho.Text);
                int fim = 0;

                if (row.RowIndex > 0) posicao = posicao + 1;
                tbxInicio.Text = posicao.ToString();

                //FAZ O CALCULO DO INICIO E FIM COM BASE NOS CAMPOS INFORMADOR E ARMAZENA A POSIÇÃO NA VARIAVEL
                fim = posicao + tamanho - 1;
                posicao = fim;

                tbxFim.Text = fim.ToString();
            }
        }

        public void SetarPosicaoLinhaUm()
        {
            TextBox tbx = (TextBox)gdvLayout.Rows[0].FindControl("tbxInicio");
            tbx.Text = "1";
        }

        protected void btnCriarLayout_Click(object sender, EventArgs e)
        {
            DataTable dtt = RecuperarDataTableViewState();
            dtt.Columns.Add(new DataColumn("FIXO"));
            dtt.Columns.Add(new DataColumn("CAMPO"));
            dtt.Columns.Add(new DataColumn("TAMANHO"));
            dtt.Columns.Add(new DataColumn("INICIO"));
            dtt.Columns.Add(new DataColumn("FIM"));
            dtt.Columns.Add(new DataColumn("ACEITAVEL"));
            for (int i = 0; i < 1; i++)
            {
                DataRow dr = dtt.NewRow();
                dtt.Rows.Add(dr);
                dr["FIXO"] = string.Empty;
            }

            AtualizarDataGrid(dtt);
        }

        protected void imgAdicionarLinha_Click(object sender, ImageClickEventArgs e)
        {
            //GUARDA O GRID
            GridViewToDataTable();

            //ADICIONA UMA NOVA LINHA NO DATATABLE
            ImageButton imgButton = (ImageButton)sender;

            //PEGA A LINHA QUE TEVE O CLICK
            GridViewRow row = (GridViewRow)imgButton.Parent.Parent;

            //PEGA O DATATABLE E INSERE A LINHA
            DataTable dtt = RecuperarDataTableViewState();

            //CRIA A LINHA 
            DataRow dr = dtt.NewRow();

            //INSERE APÓS O CLICK DO GRID
            dtt.Rows.InsertAt(dr, row.RowIndex + 1);

            //ATUALIZAR GRID
            AtualizarDataGrid(dtt);
        }

        private void AtualizarDataGrid(DataTable dtt)
        {
            gdvLayout.DataSource = dtt;
            gdvLayout.DataBind();
        }

        public DataTable RecuperarDataTableViewState()
        {
            return (DataTable)ViewState["LAYOUT"];
        }

        public void GridViewToDataTable()
        {
            DataTable dtt = RecuperarDataTableViewState();
            foreach (GridViewRow row in gdvLayout.Rows)
            {
                TextBox tbxLinha = (TextBox)row.FindControl("tbxLinha");
                TextBox tbxCampo = (TextBox)row.FindControl("tbxCampo");
                TextBox tbxTamanho = (TextBox)row.FindControl("tbxTamanho");
                TextBox tbxInicio = (TextBox)row.FindControl("tbxInicio");
                TextBox tbxFim = (TextBox)row.FindControl("tbxFim");
                TextBox tbxValorPadrao = (TextBox)row.FindControl("tbxValorPadrao");

                dtt.Rows[row.RowIndex]["FIXO"] = tbxLinha.Text;
                dtt.Rows[row.RowIndex]["CAMPO"] = tbxCampo.Text;
                dtt.Rows[row.RowIndex]["TAMANHO"] = tbxTamanho.Text;
                dtt.Rows[row.RowIndex]["INICIO"] = tbxInicio.Text;
                dtt.Rows[row.RowIndex]["FIM"] = tbxFim.Text;
                dtt.Rows[row.RowIndex]["ACEITAVEL"] = tbxValorPadrao.Text;
            }

            AtualizarDataGrid(dtt);
            SetarDataTableViewState(dtt);
        }

        protected void imgRemoverLinha_Click(object sender, ImageClickEventArgs e)
        {
            //GUARDA O GRID
            GridViewToDataTable();

            //ADICIONA UMA NOVA LINHA NO DATATABLE
            ImageButton imgButton = (ImageButton)sender;

            //PEGA A LINHA QUE TEVE O CLICK
            GridViewRow row = (GridViewRow)imgButton.Parent.Parent;

            //PEGA O DATATABLE E INSERE A LINHA
            DataTable dtt = RecuperarDataTableViewState();

            dtt.Rows.RemoveAt(row.RowIndex);

            //ATUALIZAR GRID
            AtualizarDataGrid(dtt);
        }

        public void SetarDataTableViewState(DataTable dtt)
        {
            ViewState["LAYOUT"] = dtt;
        }
    }
}