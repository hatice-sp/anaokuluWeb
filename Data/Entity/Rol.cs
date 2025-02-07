using System;
using System.Collections.Generic;

#nullable disable

namespace anaokuluWeb.Data.Entity
{
    public partial class Rol
    {
        public Rol()
        {
            Kullanicis = new HashSet<Kullanici>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }

        public virtual ICollection<Kullanici> Kullanicis { get; set; }
    }
}
