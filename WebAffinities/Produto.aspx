<%@ Page Title="Produtos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Produto.aspx.cs" Inherits="WebAffinities.Produto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="stylizedEndosso" class="myform">
        <h1>Produtos:</h1>
        <p>Preencha os dados para criação de um novo produto.</p>
        <div style="width: 100%">
            <fieldset>
                <legend>Produto</legend>
                <div style="width: 100%">
                    <div class="editor-label">
                        <label>Nome do Produto: </label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbxProduto" runat="server" Width="400px"></asp:TextBox>
                    </div>
                    <div class="editor-label">
                        <label>Código do Produto:</label>
                    </div>
                    <div class="editor-field">
                        <asp:TextBox ID="tbxCodigoProduto" runat="server" Width="50px"></asp:TextBox>
                    </div>
                </div>
                <div style="clear: both; width: 100%;">
                    <asp:GridView ID="gdvLayout" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="ORDEM" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxOrdem" runat="server" Text='<%# Bind("ORDEM") %>' Width="40px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="40px" />
                                <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HIERARQUIA" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxHierarquia" runat="server" Text='<%# Bind("HIERARQUIA") %>' Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="70px" />
                                <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TIPO" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxTipo" runat="server" Text='<%# Bind("TIPO") %>' Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="10px" Font-Strikeout="False" HorizontalAlign="Center" Width="50px" />
                                <ItemStyle Width="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
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
