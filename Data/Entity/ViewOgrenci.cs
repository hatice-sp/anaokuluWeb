using System;
using System.Collections.Generic;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class ViewOgrenci
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string SoyAd { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Veli { get; set; }
        public int? VeliIletisim { get; set; }
        public string Sinifi { get; set; }
    }
}
