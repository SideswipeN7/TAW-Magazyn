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
    
    public partial class Pracownik
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pracownik()
        {
            this.Transakcje = new HashSet<Transakcja>();
        }
    
        public int idPracownika { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public int Wiek { get; set; }
        public string Login { get; set; }
        public string Haslo { get; set; }
        public int Sudo { get; set; }
        public int idAdresu { get; set; }
    
<<<<<<< HEAD
        public  Adres Ksiazka_adresow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public  ICollection<Transakcja> Transakcje { get; set; }
=======
        public Adres Ksiazka_adresow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Transakcja> Transakcje { get; set; }
>>>>>>> Branch-Revan
    }
}
