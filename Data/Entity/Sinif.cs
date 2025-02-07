using System;
using System.Collections.Generic;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class Sinif
    {
        public Sinif()
        {
            Ogrencis = new HashSet<Ogrenci>();
        }

        public int Id { get; set; }
        public string Adi { get; set; }
        public int MaxYas { get; set; }
        public int MinYas { get; set; }

        public virtual ICollection<Ogrenci> Ogrencis { get; set; }
    }
}
