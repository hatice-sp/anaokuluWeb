using System;
using System.Collections.Generic;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class LogOgrenci
    {
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public string Islem { get; set; }
        public DateTime DegisiklikTarihi { get; set; }
    }
}
