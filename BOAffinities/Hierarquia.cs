using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAOAffinities;

namespace BOAffinities
{
    public class Hierarquia
    {
        public IEnumerable<TB_HIERARQUIA> ListarHierarquia()
        {
            return DAOAffinities.Hierarquia.ListarHierarquia();
        }
    }
}
