using DAOAffinities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOAffinities
{
    public static class Tipo
    {
        public static IEnumerable<TB_TIPO> ListarHierarquia()
        {
            try
            {
                return DAOAffinities.Tipo.ListarTipo();
            }
            catch
            {
                throw;
            }
        }
    }
}
