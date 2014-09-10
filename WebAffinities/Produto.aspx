<%@ Page Title="Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="WebAffinities.Produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="stylizedEndosso" class="myform">
        <h1>Produto:</h1>
        <p>Preencha os dados para validação de um arquivo para o produto em questão.</p>
        <div style="width: 100%">
            <fieldset>
                <legend>Produto</legend>
                <div style="width: 100%">
                    <div class="editor-label">
                        <label>Nome do Produto: </label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbxProduto" runat="server" Width="400px" Text="RESIDENCIAL AFFINITY"></asp:TextBox>
                    </div>
                    <div class="editor-label">
                        <label>Código do Produto:</label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbxCodigoProduto" runat="server" Width="50px" Text="525"></asp:TextBox>
                    </div>
                    <div class="editor-field">
                         <asp:Button ID="btnCriarLayout" runat="server" Text="Criar Layout" OnClick="btnCriarLayout_Click" />
                    </div>
                </div>
                <div style="clear: both; width: 100%;">
                    <asp:GridView ID="gdvLayout" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="FIXO" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxLinha" runat="server" Text='<%# Bind("FIXO") %>' Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="40px" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                         <%--   <asp:TemplateField HeaderText="ORDEM" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxOrdem" runat="server" Text='<%# Bind("ORDEM") %>' Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="40px" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="CAMPO" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxCampo" runat="server" Width="100px" Style="text-align: left" Text='<%# Bind("CAMPO") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HIERARQUIA" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlHierarquia" runat="server" Width="100%">
                                        <asp:ListItem Value="0" Selected="True">APÓLICE</asp:ListItem>
                                        <asp:ListItem Value="1">ITEM</asp:ListItem>
                                        <asp:ListItem Value="2">SEGURADO</asp:ListItem>
                                        <asp:ListItem Value="3">COBERTURA</asp:ListItem>
                                        <asp:ListItem Value="4">COBRANÇA</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TIPO" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlTipo" runat="server" Width="100%">
                                        <asp:ListItem Value="1">TEXTO</asp:ListItem>
                                        <asp:ListItem Value="0">NÚMERICO</asp:ListItem>
                                        <asp:ListItem Value="2">MONETÁRIO</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="90px" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OBRIGATÓRIO?">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlObrigatorio" runat="server" Width="80px">
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="0">Não</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="80px" />
                                <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TAMANHO" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxTamanho" runat="server" Text='<%# Bind("TAMANHO") %>' Width="50px" AutoPostBack="True" OnTextChanged="tbxTamanho_TextChanged"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INICIO" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxInicio" runat="server" Text='<%# Bind("INICIO") %>' Width="30px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FIM" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxFim" runat="server" Text='<%# Bind("FIM") %>' Width="30px" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="30px" />
                                <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <%-- <asp:TemplateField HeaderText="COMPLETAR?" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlAutoCompletar" runat="server" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlAutoCompletar_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Sim</asp:ListItem>
                                        <asp:ListItem Value="0" Selected="True">Não</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CARACTER" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxCaracter" runat="server" Text="" Width="50px" MaxLength="1" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DIREÇÃO" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlDirecao" runat="server" Width="100%" Enabled="false">
                                        <asp:ListItem Value="1">Esquerda</asp:ListItem>
                                        <asp:ListItem Value="0">Direita</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="90px" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="LISTA DE VALORES?">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlListaValores" runat="server" Width="100%">
                                        <asp:ListItem Value="0" Selected="True">- SELECIONE -</asp:ListItem>
                                        <asp:ListItem Value="1">ATIVIDADES</asp:ListItem>
                                        <asp:ListItem Value="2">SEXO</asp:ListItem>
                                        <asp:ListItem Value="3">TIPO DE RISCO</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="110px" />
                                <ItemStyle Width="110px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VALIDAÇÃO?">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ddlListaValidacao" runat="server" Width="100%">
                                        <asp:ListItem Value="0" Selected="True">- SELECIONE -</asp:ListItem>
                                        <asp:ListItem Value="1">DATA</asp:ListItem>
                                        <asp:ListItem Value="2">CPF</asp:ListItem>
                                        <asp:ListItem Value="3">CNPJ</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="100px" />
                                <ItemStyle Width="100px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DADO ACEITÁVEL?" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxValorPadrao" runat="server" Width="90px" Text='<%# Bind("ACEITAVEL") %>'></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="90px" />
                                <ItemStyle Width="90px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAdicionarLinha" ImageUrl="~/Images/1410369120_Add.png" runat="server" Width="15px" Height="15px" AlternateText="Adicionar Linha" OnClick="imgAdicionarLinha_Click" />
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="15px" />
                                <ItemStyle Width="15px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgRemoverLinha" ImageUrl="~/Images/1410369134_Delete.png" runat="server" Width="15px" Height="15px" AlternateText="Adicionar Linha" OnClick="imgRemoverLinha_Click" />
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="15px" />
                                <ItemStyle Width="15px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </fieldset>
        </div>
        <div>
            <%--            @Html.ActionLink("Voltar", "Index", "Home")--%>
        </div>
    </div>
</asp:Content>
