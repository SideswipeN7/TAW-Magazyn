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

    public partial class Artykul_w_transakcji
    {
        public int idArt_w_trans { get; set; }
        public decimal Cena { get; set; }
        public int idTransakcji { get; set; }
        public int idArtykulu { get; set; }
    
        public virtual Artykul Artykuly { get; set; }
        [JsonIgnore]
        public virtual Transakcja Transakcje { get; set; }
    }
}