//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebServer.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Artykul
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Artykul()
        {
            this.Artykuly_w_transakcji = new HashSet<Artykul_w_transakcji>();
        }
    
        public int idArtykulu { get; set; }
        public string Nazwa { get; set; }
        public int Ilosc { get; set; }
        public decimal Cena { get; set; }
        public int idKategorii { get; set; }
    
        public  Kategoria Kategorie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Artykul_w_transakcji> Artykuly_w_transakcji { get; set; }
    }
}
