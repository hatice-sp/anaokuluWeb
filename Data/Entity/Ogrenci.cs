using System;
using System.Collections.Generic;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class Ogrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string SoyAd { get; set; }
        public DateTime DogumTarihi { get; set; }
        public int VeliId { get; set; }
        public int SinifId { get; set; }
        public bool? SilinmisMi { get; set; }

        public virtual Sinif Sinif { get; set; }
        public virtual Kullanici Veli { get; set; }
    }
}
