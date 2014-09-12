using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOAffinities
{
    public static class Hierarquia
    {
        public static IEnumerable<TB_HIERARQUIA> ListarHierarquia()
        {
            using (AffinitiesEntities db = new AffinitiesEntities())
            {
                return db.TB_HIERARQUIA.ToList();
            }
        }
    }
}
