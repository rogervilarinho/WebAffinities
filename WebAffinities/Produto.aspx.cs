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
                //VERIFICA SE O LAYOUT DO PRODUTO CONSULTADO JÁ EXISTE
                var layout = BOAffinities.LayoutArquivoDetalhe.ListarLayoutArquivoDetalhe(1);
                DataTable dtt = new DataTable();
                ViewState.Add("LAYOUT", dtt);
                if (layout.Count() > 0)
                {
                    //JOGA OS VALORES NO DATA TABLE
                    SetarListaParaDataTable(layout, dtt);
                }

                AtualizarDataGrid(dtt);
                
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

        private void SetarListaParaDataTable(IEnumerable<DAOAffinities.TB_LAYOUT_ARQUIVO_DETALHE> layout, DataTable dtt)
        {
            SetarColunasDataTable(dtt);
            foreach (var item in layout.OrderBy(x => x.ID_HIERARQUIA))
            {
                DataRow dr = dtt.NewRow();
                dr["FIXO"] = item.DES_FIXO;
                dr["CAMPO"] = item.DES_CAMPO;
                dr["TAMANHO"] = item.NUM_TAMANHO;
                dr["INICIO"] = item.NUM_INICIO;
                dr["FIM"] = item.NUM_FIM;
                dr["ACEITAVEL"] = item.DES_DADO_ACEITAVEL;
                dr["HIERARQUIA"] = item.ID_HIERARQUIA;
                dr["TIPO"] = item.ID_TIPO;
                dr["OBRIGATORIO"] = item.ID_OBRIGATORIO;
                //TRATA VALORES NULOS PARA SETAR O MENOS -1
                dr["VALIDACAO"] = item.ID_VALICAO.HasValue ? item.ID_VALICAO : -1;
                dr["LISTA"] = item.ID_LISTA.HasValue ? item.ID_LISTA : -1;
                dr["IDDETALHE"] = item.ID_LAYOUT_ARQUIVO_DETALHE;
                dtt.Rows.Add(dr);
            }

            SetarDataTableViewState(dtt);
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
                SetarPosicoesGRID(sender, true, "ddlListaValores");
            }
        }

        private void SetarPosicoesGRID(object sender)
        {
            SetarPosicoesGRID(sender, false, string.Empty);
        }
        private void SetarPosicoesGRID(object sender, bool setarFoco, string controle)
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
            if(setarFoco)
            SetarFocusCampoTipo(sender, controle);
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
            SetarColunasDataTable(dtt);

            for (int i = 0; i < 1; i++)
            {
                DataRow dr = dtt.NewRow();
                SetarValoresLinha(dr);
                dtt.Rows.Add(dr);
            }

            AtualizarDataGrid(dtt);
        }

        private void SetarColunasDataTable(DataTable dtt)
        {
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
            dtt.Columns.Add(new DataColumn("IDDETALHE"));
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

            //PEGA A LISTA ORDENADA
            
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
                Label lblLayoutArquivoDetalheId = (Label)row.FindControl("lblLayoutArquivoDetalheId");

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
                if(!String.IsNullOrEmpty(lblLayoutArquivoDetalheId.Text))
                dtt.Rows[row.RowIndex]["IDDETALHE"] = Convert.ToInt32(lblLayoutArquivoDetalheId.Text);
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

            //REMOVE A LISTA DA BASE
            Label lblLayoutArquivoDetalheId = (Label)row.FindControl("lblLayoutArquivoDetalheId");

            if (!String.IsNullOrEmpty(lblLayoutArquivoDetalheId.Text))
            {
                BOAffinities.LayoutArquivoDetalhe.DeletarLayoutArquivoDetalhe(Convert.ToInt32(lblLayoutArquivoDetalheId.Text));
            }

            //PEGA O DATATABLE E INSERE A LINHA
            DataTable dtt = RecuperarDataTableViewState();

            dtt.Rows.RemoveAt(row.RowIndex);

            //ATUALIZAR GRID
            AtualizarDataGrid(dtt);

            //ATUALIZA A NUMERACAO
            SetarPosicoesGRID(sender);

            //GRAVA TODA A TABELA NOVAMNETE.
            GravarGridBD();
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

        protected void btnGravarLayout_Click(object sender, EventArgs e)
        {
            GravarGridBD();
        }

        private void GravarGridBD()
        {
            List<DAOAffinities.TB_LAYOUT_ARQUIVO_DETALHE> layout = new List<DAOAffinities.TB_LAYOUT_ARQUIVO_DETALHE>();
            //CAPTURA OS VALORES E GRAVA O LAYOUT
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
                Label lblLayoutArquivoDetalheId = (Label)row.FindControl("lblLayoutArquivoDetalheId");

                int layoutArquivoId = 1;
                string desFixo = tbxLinha.Text.ToUpper();
                string desCampo = tbxCampo.Text.ToUpper();
                int hierarquiaId = Convert.ToInt32(ddlHierarquia.SelectedValue);
                int tipoId = Convert.ToInt32(ddlTipo.SelectedValue);
                int obrigatorioId = Convert.ToInt32(ddlObrigatorio.SelectedValue);
                int tamanho = Convert.ToInt32(tbxTamanho.Text);
                int inicio = Convert.ToInt32(tbxInicio.Text);
                int fim = Convert.ToInt32(tbxFim.Text);
                int? listaValoresId = Convert.ToInt32(ddlListaValores.SelectedValue);
                if (listaValoresId == -1) listaValoresId = null;
                int? listaValidacaoId = Convert.ToInt32(ddlListaValidacao.SelectedValue);
                if (listaValidacaoId == -1) listaValidacaoId = null;
                string valorPadrao = tbxValorPadrao.Text.ToUpper();
                int layoutArquivoDetalheId = 0;
                if (!String.IsNullOrEmpty(lblLayoutArquivoDetalheId.Text))
                    layoutArquivoDetalheId = Convert.ToInt32(lblLayoutArquivoDetalheId.Text);

                layout.Add(new DAOAffinities.TB_LAYOUT_ARQUIVO_DETALHE()
                {
                    ID_LAYOUT_ARQUIVO = layoutArquivoId,
                    DES_FIXO = desFixo,
                    DES_CAMPO = desCampo,
                    ID_HIERARQUIA = hierarquiaId,
                    ID_TIPO = tipoId,
                    ID_OBRIGATORIO = obrigatorioId,
                    NUM_TAMANHO = tamanho,
                    NUM_INICIO = inicio,
                    NUM_FIM = fim,
                    ID_LISTA = listaValoresId,
                    ID_VALICAO = listaValidacaoId,
                    DES_DADO_ACEITAVEL = valorPadrao,
                    ID_LAYOUT_ARQUIVO_DETALHE = layoutArquivoDetalheId
                });
            }

            BOAffinities.LayoutArquivoDetalhe.GravarLayoutArquivoDetalhe(layout);
        }
    }
}