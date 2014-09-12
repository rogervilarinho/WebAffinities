using DAOAffinities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOAffinities
{
    public static class LayoutArquivoDetalhe
    {
        public static IEnumerable<TB_LAYOUT_ARQUIVO_DETALHE> ListarLayoutArquivoDetalhe(int LayoutId)
        {
            try
            {
                return DAOAffinities.LayoutArquivoDetalhe.ListarLayoutArquivoDetalhe(LayoutId);
            }
            catch
            {
                throw;
            }
        }

        public static void GravarLayoutArquivoDetalhe(IEnumerable<TB_LAYOUT_ARQUIVO_DETALHE> layout)
        {
            try
            {
                DAOAffinities.LayoutArquivoDetalhe.GravarLayoutArquivoDetalhe(layout);
            }
            catch
            {
                throw;
            }
        }

        public static void DeletarLayoutArquivoDetalhe(int layoutArquivoDetalheId)
        {
            try
            {
                DAOAffinities.LayoutArquivoDetalhe.DeletarLayoutArquivoDetalhe(layoutArquivoDetalheId);
            }
            catch
            {
                throw;
            }
        }
    }
}
