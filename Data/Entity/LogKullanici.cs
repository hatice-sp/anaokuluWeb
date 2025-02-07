using System;
using System.Collections.Generic;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class LogKullanici
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Tckimlik { get; set; }
        public int Telefon { get; set; }
        public int RolId { get; set; }
        public string Islem { get; set; }
        public DateTime DegisiklikTarihi { get; set; }
    }
}
