//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.Model

{ 
    public partial class Artykul_w_transakcji
    {
        public int idArt_w_trans { get; set; }
        public decimal Cena { get; set; }
        public int idTransakcji { get; set; }
        public int idArtykulu { get; set; }
    
        public  Artykul Artykuly { get; set; }
        public  Transakcja Transakcje { get; set; }
    }
}
