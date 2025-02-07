using anaokuluWeb.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anaokuluWeb.Models
{
   
        public class VeliIndexViewModel
        {
            public List<VeliInfo> VeliList { get; set; }
        }

        public class VeliInfo
        {
            public int Id { get; set; }
            public string KullaniciAdi { get; set; }
            public string Sifre { get; set; }
            public string Tckimlik { get; set; }
            public int? Telefon { get; set; }
            public int RolId { get; set; }
            public string RolAdi { get; set; }
        

        }

    }

