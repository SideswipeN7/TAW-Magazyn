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
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public partial class Transakcja
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transakcja()
        {
            this.Artykuly_w_transakcji = new HashSet<Artykul_w_transakcji>();
        }
    
        public int idTransakcji { get; set; }
        public System.DateTime Data { get; set; }
        public int idKlienta { get; set; }
        public int idPracownika { get; set; }
        public int idDostawcy { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Artykul_w_transakcji> Artykuly_w_transakcji { get; set; }
        public  Dostawca Dostawcy { get; set; }
        public  Klient Klienci { get; set; }
        [JsonIgnore]
        public  Pracownik Pracownicy { get; set; }
    }
}