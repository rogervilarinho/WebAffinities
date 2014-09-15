using DAOAffinities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BOAffinities
{
    public static class ValidacaoArquivo
    {
        public static void ValidarArquivo(int layoutArquivoId, string nameFile)
        {
            try
            {
                //STRING BUILDER QUE ARMAZENARÁ TODOS OS ERROS
                StringBuilder sb = new StringBuilder();

                //VERIFICA A LEITURA DO ARQUIVO
                using (StreamReader sr = new StreamReader(String.Concat(AppDomain.CurrentDomain.BaseDirectory, @"731BENOITPFII110920140001.txt")))
                {
                    //BUSCA O LAYOUT DO ARQUIVO
                    var layout = DAOAffinities.LayoutArquivoDetalhe.ListarLayoutArquivoDetalhe(layoutArquivoId);

                    //SEPARA OS FIXOS
                    var fixos = layout.Select(x => x.DES_FIXO).Distinct();

                    //CONTADOR DA LINHAS
                    int auxLinha = 1;

                    //FAZ UM FOREACH EM TODOS OS FIXO E VERIFICA SE O FIXO É IGUAL A LINHA DO ARQUIVO
                    while (!sr.EndOfStream)
                    {
                        //LÊ A LINHA
                        var linha = sr.ReadLine();

                        //VARIAVEL PARA ARMAZENAR SE A LINHA TEM FIXO
                        bool hasfixo = false;

                        //VERIFICA SE A LINHA TEM ALGUM DOS FIXOS
                        foreach (var fixo in fixos)
                        {
                            //COMPARA O TAMANHO DO FIXO COM A STRING DA LINHA
                            if (linha.Substring(0, fixo.Length).ToUpper().Equals(fixo.ToUpper()))
                            {
                                //ESSA LINHA É DO FIXO EM QUESTÃO.
                                IniciarValidacao(linha, layout.Where(x => x.DES_FIXO.ToUpper().Equals(fixo.ToUpper())));
                                hasfixo = true;
                                break;
                            }
                        }

                        if (!hasfixo)
                        {
                            sb.AppendLine(String.Concat("A linha ", auxLinha, " não possui um fixo cadastrado no layout em questão!"));
                        }
                    }
                    
                }
            }
            catch
            {
                throw;
            }
        }

        private static void IniciarValidacao(string linha, IEnumerable<TB_LAYOUT_ARQUIVO_DETALHE> enumerable)
        {
            try
            {
                var tamanhoLinha = enumerable.Max(x => x.NUM_FIM);

                //VERIFICA SE A LINHA TEM O TAMANHO CORRETO
                if (tamanhoLinha == linha.Length)
                {
                    //COMEÇA AS VALIDAÇÕES
                    foreach (var campo in enumerable)
                    {
                        //VERIFICA SEPARA A SUBSTRING QUE SERÁ VALIDADA
                        string sub = linha.Substring(campo.NUM_INICIO - 1, campo.NUM_TAMANHO);

                        //VERIFICA SE O CAMPO TEM ALGUM DADO OBRIGATÓRIO
                        if (!String.IsNullOrEmpty(campo.DES_DADO_ACEITAVEL))
                        {
                            //QUEBRA A LINHA COM PONTO E VIRGULA
                            List<string> lstAceitavel = new List<string>();
                            lstAceitavel.AddRange(campo.DES_DADO_ACEITAVEL.Split(';'));

                            if (!lstAceitavel.Contains(sub))
                            {
                                //NÃO CONTÉM O DADO ACEITÁVEL
                            }
                        }

                        //SE O NÚMERO É INTEIRO VERIFICA SE DA PARA CONVERTER PARA INT
                        if (campo.ID_TIPO.Equals(2))
                        {
                            try
                            {
                                Int32.Parse(sub);
                            }
                            catch (FormatException)
                            {
                                //ERRO DE FORMATO (PODE SER STRING)
                            }
                            catch (ArgumentNullException)
                            {
                                //VALOR EM BRANCO
                            }
                        }
                    }
                }
                else
                {

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
