//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAOAffinities
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_LAYOUT_ARQUIVO_DETALHE
    {
        public int ID_LAYOUT_ARQUIVO_DETALHE { get; set; }
        public int ID_LAYOUT_ARQUIVO { get; set; }
        public string DES_FIXO { get; set; }
        public string DES_CAMPO { get; set; }
        public int ID_HIERARQUIA { get; set; }
        public int ID_TIPO { get; set; }
        public int ID_OBRIGATORIO { get; set; }
        public int NUM_TAMANHO { get; set; }
        public int NUM_INICIO { get; set; }
        public int NUM_FIM { get; set; }
        public int ID_LISTA { get; set; }
        public int ID_VALICAO { get; set; }
        public string DES_DADO_ACEITAVEL { get; set; }
    
        public virtual TB_HIERARQUIA TB_HIERARQUIA { get; set; }
        public virtual TB_VALIDACAO TB_VALIDACAO { get; set; }
        public virtual TB_LISTA TB_LISTA { get; set; }
        public virtual TB_TIPO TB_TIPO { get; set; }
        public virtual TB_LAYOUT_ARQUIVO TB_LAYOUT_ARQUIVO { get; set; }
    }
}
