using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOAffinities
{
    public static class Tipo
    {
        public static IEnumerable<TB_TIPO> ListarTipo()
        {
            using (AffinitiesEntities db = new AffinitiesEntities())
            {
                return db.TB_TIPO.ToList();
            }
        }
    }
}
