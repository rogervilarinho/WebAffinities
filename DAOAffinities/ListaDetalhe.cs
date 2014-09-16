using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOAffinities
{
    public static class ListaDetalhe
    {
        public static IEnumerable<TB_LISTA_DETALHE> ListarListaDetalhe(int listaId)
        {
            try
            {
                using (AffinitiesEntities db = new AffinitiesEntities())
                {
                    var lista = db.TB_LISTA_DETALHE.Where(x => x.ID_LISTA.Equals(listaId)).ToList();
                    return lista;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
