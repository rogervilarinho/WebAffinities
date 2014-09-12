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
                SetarPosicoesGRID(sender);
            }
        }

        private void SetarPosicoesGRID(object sender)
        {
            int posicao = 1;
            int hierarquia = 0;
            Dictionary<int, int> dicHierarquia = new Dictionary<int, int>();
            bool soma = false;
            foreach (GridViewRow row in gdvLayout.Rows)
            {

                //VERIFICA SE A HIEARQUIA JÁ FOI SETADA COMO UM, SE SIM, NÃO FAZ NADA, SE NÃO, SETA E COLOCA NA LISTA.
                TextBox tbxInicio = (TextBox)row.FindControl("tbxInicio");
                TextBox tbxTamanho = (TextBox)row.FindControl("tbxTamanho");
                TextBox tbxFim = (TextBox)row.FindControl("tbxFim");
                DropDownList ddlHierarquia = (DropDownList)row.FindControl("ddlHierarquia");
                hierarquia = Convert.ToInt32(ddlHierarquia.SelectedValue);
                
                if (!String.IsNullOrEmpty(tbxTamanho.Text))
                {
                    int tamanho = Convert.ToInt32(tbxTamanho.Text);
                    if (!dicHierarquia.ContainsKey(hierarquia))
                    {
                        dicHierarquia.Add(hierarquia, tamanho);
                        posicao = 1;
                        SetarPosicaoLinhaUm(row);
                        soma = false;
                    }
                    else
                    {
                        //quando houver a hierarquia no dicionario, seta a ultima posicao para que o proximo registro da hierarquia comece na posicao.
                        soma = true;
                        posicao = dicHierarquia[hierarquia];
                    }
                    
                    int fim = 0;

                    if (soma) posicao = posicao + 1;
                    tbxInicio.Text = posicao.ToString();

                    //FAZ O CALCULO DO INICIO E FIM COM BASE NOS CAMPOS INFORMADOR E ARMAZENA A POSIÇÃO NA VARIAVEL
                    fim = posicao + tamanho - 1;
                    posicao = fim;

                    tbxFim.Text = fim.ToString();
                    
                    dicHierarquia[hierarquia] = Convert.ToInt32(tbxFim.Text);
                }
            }

            SetarFocusCampoTipo(sender, "ddlListaValores");
        }

        public void SetarPosicaoLinhaUm()
        {
            SetarPosicaoLinhaUm(gdvLayout.Rows[0]);
        }
        public void SetarPosicaoLinhaUm(GridViewRow row)
        {
            TextBox tbx = (TextBox) row.FindControl("tbxInicio");
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
            dtt.Columns.Add(new DataColumn("HIERARQUIA"));
            dtt.Columns.Add(new DataColumn("TIPO"));
            dtt.Columns.Add(new DataColumn("OBRIGATORIO"));
            dtt.Columns.Add(new DataColumn("VALIDACAO"));
            dtt.Columns.Add(new DataColumn("LISTA"));

            for (int i = 0; i < 1; i++)
            {
                DataRow dr = dtt.NewRow();
                SetarValoresLinha(dr);
                dtt.Rows.Add(dr);
            }

            AtualizarDataGrid(dtt);
        }

        public void SetarValoresLinha(DataRow dr)
        {
            dr["FIXO"] = string.Empty;
            dr["HIERARQUIA"] = "1";
            dr["TIPO"] = "1";
            dr["OBRIGATORIO"] = "1";
            dr["VALIDACAO"] = "-1";
            dr["LISTA"] = "-1";
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

            //SETA O VALOR DEFAULT PARA OS CAMPOS DE DROPDOWN
            SetarValoresLinha(dr);

            //INSERE APÓS O CLICK DO GRID
            dtt.Rows.InsertAt(dr, row.RowIndex + 1);

            //ATUALIZAR GRID
            AtualizarDataGrid(dtt);

            //SETAR FOCUS ULTIMA LINHA
            SetarFocusUltimaLinha();
        }

        private void SetarFocusUltimaLinha()
        {
            TextBox tbx = (TextBox) gdvLayout.Rows[gdvLayout.Rows.Count - 1].FindControl("tbxLinha");
            tbx.Focus();
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
                DropDownList ddlHierarquia = (DropDownList)row.FindControl("ddlHierarquia");
                DropDownList ddlTipo = (DropDownList)row.FindControl("ddlTipo");
                DropDownList ddlObrigatorio = (DropDownList)row.FindControl("ddlObrigatorio");
                DropDownList ddlListaValidacao = (DropDownList)row.FindControl("ddlListaValidacao");
                DropDownList ddlListaValores = (DropDownList)row.FindControl("ddlListaValores");

                dtt.Rows[row.RowIndex]["FIXO"] = tbxLinha.Text;
                dtt.Rows[row.RowIndex]["CAMPO"] = tbxCampo.Text;
                dtt.Rows[row.RowIndex]["TAMANHO"] = tbxTamanho.Text;
                dtt.Rows[row.RowIndex]["INICIO"] = tbxInicio.Text;
                dtt.Rows[row.RowIndex]["FIM"] = tbxFim.Text;
                dtt.Rows[row.RowIndex]["ACEITAVEL"] = tbxValorPadrao.Text;
                dtt.Rows[row.RowIndex]["HIERARQUIA"] = ddlHierarquia.SelectedValue;
                dtt.Rows[row.RowIndex]["TIPO"] = ddlTipo.SelectedValue;
                dtt.Rows[row.RowIndex]["OBRIGATORIO"] = ddlObrigatorio.SelectedValue;
                dtt.Rows[row.RowIndex]["VALIDACAO"] = ddlListaValidacao.SelectedValue;
                dtt.Rows[row.RowIndex]["LISTA"] = ddlListaValores.SelectedValue;
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

        protected void ddlHierarquia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //QUANDO O DROPDOWN FOR ALTERADO REORDENA AS HIERARQUIAS COM AS POSIÇÕES.
            SetarPosicoesGRID(sender);

            //SETAR FOCUS PROXIMO CONTROLE
            SetarFocusCampoTipo(sender, "ddlTipo");
        }

        private void SetarFocusCampoTipo(object sender, string campo)
        {
            Control obj = (Control) sender;

            //LINHA DO GRID
            GridViewRow row = (GridViewRow) obj.Parent.Parent;

            //SETAR O FOCUS.
            if (row.FindControl(campo) != null)
            {
                Control ctl = (Control)row.FindControl(campo);
                ctl.Focus();
            }
        }
    }
}