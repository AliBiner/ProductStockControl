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
    
    public partial class urunler
    {
        public int id { get; set; }
        public string ad { get; set; }
        public string aciklama { get; set; }
        public Nullable<decimal> fiyat { get; set; }
        public Nullable<int> stok { get; set; }
        public Nullable<bool> populer { get; set; }
        public string resim { get; set; }
        public Nullable<int> kategoriID { get; set; }
    
        public virtual kategori kategori { get; set; }
    }
}