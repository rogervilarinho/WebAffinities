using DAOAffinities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOAffinities
{
    public static class Obrigatorio
    {
        public static IEnumerable<TB_OBRIGATORIO> ListarObrigatorio()
        {
            try
            {
                return DAOAffinities.Obrigatorio.ListarObrigatorio();
            }
            catch
            {
                throw;
            }
        }
    }
}
