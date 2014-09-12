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
    }
}
