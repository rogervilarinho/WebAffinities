using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOAffinities
{
    public static class Validacao
    {
        public static IEnumerable<TB_VALIDACAO> ListarValidacao()
        {
            using (AffinitiesEntities db = new AffinitiesEntities())
            {
                var lista = db.TB_VALIDACAO.ToList();
                lista.Insert(0, new TB_VALIDACAO { ID_VALIDACAO = -1, NOM_VALIDACAO = "- SELECIONE -" });
                return lista;
            }
        }
    }
}
