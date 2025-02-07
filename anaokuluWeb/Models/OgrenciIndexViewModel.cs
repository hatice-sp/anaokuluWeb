using anaokuluWeb.Data.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anaokuluWeb.Models
{
    public class OgrenciIndexViewModel
    {
        public string AdAra { get; set; }
        public string SoyAdAra { get; set; }
        public List<OgrenciInfo> OgrenciList { get; set; }
    }

    public class OgrenciInfo
    {
        public int Id { get; set; }
        public bool? SilinmisMi { get; set; }
        public Sinif Sinif { get; set; }
        public string Ad { get; set; }
        public string SoyAd { get; set; }
        public int SinifId { get; set; }
        public int VeliId { get; set; }
        public Kullanici Veli { get; set; }
        public string SinifAdi { get; set; }
        public string VeliAdi { get; set; }
        public DateTime DogumTarihi { get; set; }

        public SelectList Siniflar { get; set; }
        public SelectList Veliler { get; set; }
    }

    public class VelilerInfo
    {
        public int Id { get; set; }
        public string Adi { get; set; }
    }

    public class SiniflarInfo
    {
        public int Id { get; set; }
        public string Adi { get; set; }
    }

    public class OgrenciImportModel
    {
        public IFormFile file { get; set; }
    }

}

