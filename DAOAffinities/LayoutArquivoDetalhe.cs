using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOAffinities
{
    public static class LayoutArquivoDetalhe
    {
        public static IEnumerable<TB_LAYOUT_ARQUIVO_DETALHE> ListarLayoutArquivoDetalhe(int LayoutId)
        {
            using (AffinitiesEntities db = new AffinitiesEntities())
            {
                return db.TB_LAYOUT_ARQUIVO_DETALHE.Where(x => x.ID_LAYOUT_ARQUIVO == LayoutId).ToList();   
            }
        }

        public static void GravarLayoutArquivoDetalhe(IEnumerable<TB_LAYOUT_ARQUIVO_DETALHE> layout)
        {
            try
            {
                using (AffinitiesEntities db = new AffinitiesEntities())
                {
                    foreach (var item in layout)
                    {
                        if (item.ID_LAYOUT_ARQUIVO_DETALHE > 0)
                        {
                            db.Entry(item).State = System.Data.EntityState.Modified;
                        }
                        else
                        {
                            db.TB_LAYOUT_ARQUIVO_DETALHE.Add(item);
                        }
                    }

                    db.SaveChanges();
                }
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
                using (AffinitiesEntities db = new AffinitiesEntities())
                {
                    db.TB_LAYOUT_ARQUIVO_DETALHE.Remove(db.TB_LAYOUT_ARQUIVO_DETALHE.Where(x => x.ID_LAYOUT_ARQUIVO_DETALHE == layoutArquivoDetalheId).First());
                    db.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
