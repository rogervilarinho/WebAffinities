using DAOAffinities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BOAffinities
{
    public static class Validacao
    {
        public static IEnumerable<TB_VALIDACAO> ListarValidacao()
        {
            try
            {
                return DAOAffinities.Validacao.ListarValidacao();
            }
            catch
            {
                throw;
            }
        }
    }
}
