using System;
using System.Collections.Generic;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class Kullanici
    {
        public Kullanici()
        {
            Ogrencis = new HashSet<Ogrenci>();
        }

        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Tckimlik { get; set; }
        public int? Telefon { get; set; }
        public int RolId { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual ICollection<Ogrenci> Ogrencis { get; set; }
    }
}
