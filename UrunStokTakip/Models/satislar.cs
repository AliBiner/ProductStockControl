//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UrunStokTakip.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class satislar
    {
        public int id { get; set; }
        public Nullable<int> urunID { get; set; }
        public Nullable<int> adet { get; set; }
        public Nullable<decimal> fiyat { get; set; }
        public Nullable<System.DateTime> tarih { get; set; }
        public string resim { get; set; }
        public Nullable<int> kullaniciID { get; set; }
    
        public virtual kullanici kullanici { get; set; }
    }
}
