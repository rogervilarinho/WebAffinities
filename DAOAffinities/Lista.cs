using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOAffinities
{
    public static class Lista
    {
        public static IEnumerable<TB_LISTA> ListarLista()
        {
            try
            {
                using (AffinitiesEntities db = new AffinitiesEntities())
                {
                    var lista = db.TB_LISTA.ToList();
                    lista.Insert(0, new TB_LISTA() { ID_LISTA = -1, NOM_LISTA = "- SELECIONE -" });
                    return lista;
                }
            }
            catch
            {
                throw;
            }
        }

        public static string PegarNomeLista(int listaId)
        {
            using (AffinitiesEntities db = new AffinitiesEntities())
            {
                return db.TB_LISTA.Where(x => x.ID_LISTA.Equals(listaId)).Select(x => x.NOM_LISTA).First().ToString();
            }
        }
    }
}
