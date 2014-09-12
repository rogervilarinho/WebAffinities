using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOAffinities
{
    public static class Obrigatorio
    {
        public static IEnumerable<TB_OBRIGATORIO> ListarObrigatorio()
        {
            try
            {
                using (AffinitiesEntities db = new AffinitiesEntities())
                {
                    return db.TB_OBRIGATORIO.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
