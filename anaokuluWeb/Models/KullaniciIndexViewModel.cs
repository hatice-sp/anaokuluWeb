using anaokuluWeb.Data.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anaokuluWeb.Models
{
    public class KullaniciIndexViewModel
    {
        public List<KullaniciInfo> KullaniciList { get; set; }
    }

    public class KullaniciInfo{
        public int Id { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Tckimlik { get; set; }
        public int? Telefon { get; set; }
        public int RolId { get; set; }
        public string RolAdi { get; set; }
        public SelectList Roller { get; set; }

    }

}
