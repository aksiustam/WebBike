//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBike.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bike
    {
        public int Id { get; set; }
        public Nullable<int> TipId { get; set; }
        public Nullable<int> MarkaId { get; set; }
        public Nullable<int> ResimId { get; set; }
        public string Ad { get; set; }
        public Nullable<int> Fiyat { get; set; }
        public Nullable<int> Skor { get; set; }
        public Nullable<int> Tarih { get; set; }
        public Nullable<int> Hiz { get; set; }
    
        public virtual Marka Marka { get; set; }
        public virtual Tip Tip { get; set; }
    }
}
