<%@ Page Title="Página Inicial" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAffinities._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Aplicação para construção de Layouts de arquivos de Affinities.</h2>
            </hgroup>
            <p>
                Esta aplicação permite que os layouts de produtos que possuem arquivos de textos sejam construídos ou importados.
                A partir da construção de um layout de produto<mark>você pode criar layouts de arquivos de origem</mark>para geração de arquivos no layout desejado.
                A aplicação trabalha com os formatos<mark>.txt, .xml e .dat.</mark>
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>Nesta aplicação você pode:</h3>
    <ol class="round">
        <li class="one">
            <h5>Layout de Produto</h5>
            Baseando-se em um armado você pode criar ou importar o layout de um produto existente.
        </li>
        <li class="two">
            <h5>Layout de Origem</h5>
            Você pode construir um layout para que o sistema leia os arquivos de origem e deixem no formato do produto desejado.
        </li>
        <li class="three">
            <h5>Validações</h5>
            Você pode executar a validação de arquivo gerado.
        </li>
    </ol>
</asp:Content>
