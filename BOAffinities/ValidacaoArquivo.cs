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
        public static StringBuilder ValidarArquivo(int layoutArquivoId, string nameFile)
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
                                IniciarValidacao(linha, layout.Where(x => x.DES_FIXO.ToUpper().Equals(fixo.ToUpper())), sb, auxLinha);
                                hasfixo = true;
                                break;
                            }
                        }

                        if (!hasfixo)
                        {
                            //sb.Append(String.Concat("Linha ", auxLinha, " : A linha não possui um fixo cadastrado no layout em questão! <br />"));
                            InserirMensagem(sb, String.Concat("A linha não possui um fixo cadastrado no layout em questão"), auxLinha);
                        }

                        auxLinha = auxLinha + 1;
                    }

                }

                return sb;
            }
            catch
            {
                throw;
            }
        }

        public static void InserirMensagem(StringBuilder sb, string mensagem, int auxLinha)
        {
            sb.Append(String.Concat("[Linha ", auxLinha, "]: ", mensagem, "! <br />").ToUpper());
        }

        public static void InserirMensagem(StringBuilder sb, string mensagem, TB_LAYOUT_ARQUIVO_DETALHE campo, int auxLinha, string sub)
        {
            sb.Append(String.Concat("[Linha ", auxLinha, " - Posição ", campo.NUM_INICIO, " - ", campo.NUM_FIM, " (", campo.DES_CAMPO, ")]: ", mensagem, "! <br />").ToUpper());
        }

        private static StringBuilder IniciarValidacao(string linha, IEnumerable<TB_LAYOUT_ARQUIVO_DETALHE> enumerable, StringBuilder sb, int auxLinha)
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

                        if (campo.ID_OBRIGATORIO.Equals(1))
                        {
                            //VERIFICA SE O CAMPO TEM ALGUM DADO OBRIGATÓRIO
                            if (!String.IsNullOrEmpty(campo.DES_DADO_ACEITAVEL))
                            {
                                //QUEBRA A LINHA COM PONTO E VIRGULA
                                List<string> lstAceitavel = new List<string>();
                                lstAceitavel.AddRange(campo.DES_DADO_ACEITAVEL.Split(';'));

                                if (!lstAceitavel.Contains(sub.Trim()))
                                {
                                    //sb.Append(String.Concat("Linha ", auxLinha, " : A linha possui um valor não aceito ", sub, " na lista informada como para o campo em questão (", campo.DES_DADO_ACEITAVEL, ")! <br />"));
                                    InserirMensagem(sb, String.Concat("A linha possui um valor não aceito (", sub, ") na lista informada para o campo em questão (", campo.DES_DADO_ACEITAVEL, ")"), campo, auxLinha, sub);
                                }
                            }


                            //SE O NÚMERO É INTEIRO VERIFICA SE DA PARA CONVERTER PARA INT SÓ VERIFICA SE O CAMPO FOR OBRIGATÓRIO
                            if (campo.ID_TIPO.Equals(2))
                            {
                                try
                                {
                                    Int64.Parse(sub);
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
                            else if (campo.ID_TIPO.Equals(3))
                            {
                                try
                                {
                                    //VERIFICA SE TEM DUAS CASAS APÓS A VIRGULA
                                    if (sub.Split(',').Count() == 2 && sub.Split(',')[1].Length == 2)
                                    {
                                        Double.Parse(sub);
                                    }
                                    else
                                    {
                                        //ERRO NO FORMATO.
                                    }

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
                }
                else
                {
                    //sb.Append(String.Concat("Linha ", auxLinha, " : A linha possui ", linha.Length, " e a quantidade correta de posições é ", enumerable.Max(x => x.NUM_FIM), "! <br />"));
                    InserirMensagem(sb, String.Concat("A linha possui ", linha.Length, " e a quantidade correta de posições é ", enumerable.Max(x => x.NUM_FIM)), auxLinha);
                }

                return sb;
            }
            catch
            {
                throw;
            }
        }
    }
}
