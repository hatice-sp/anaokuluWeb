using anaokuluWeb.Data.Entity;
using anaokuluWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace anaokuluWeb.Controllers
{
    public class VeliController : Controller
    {
        private readonly AnaokuluContext _context = new AnaokuluContext();

        public IActionResult Index()
        {
            var ActiveUserId=Convert.ToInt32(((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type.Contains("sid")).Value.Trim());

            var listMyStudent = _context.Ogrencis
                .Include(x=>x.Sinif)
                .Include(x=>x.Veli)
                .Where(x=>x.VeliId == ActiveUserId)
                    .ToList();

            var model = new OgrenciIndexViewModel();

            List<OgrenciInfo> newList = new List<OgrenciInfo>();

            foreach (var item in listMyStudent)
            {
                OgrenciInfo listItem = new OgrenciInfo();

                listItem.Id = item.Id;
                listItem.Ad = item.Ad;
                listItem.SoyAd = item.SoyAd;
                listItem.DogumTarihi = item.DogumTarihi;
                listItem.VeliId = item.VeliId;
                listItem.SinifId = item.SinifId;
                listItem.SilinmisMi = item.SilinmisMi;
                listItem.SinifAdi = item.Sinif.Adi;
                listItem.VeliAdi = item.Veli.KullaniciAdi;
                newList.Add(listItem);
            }

            model.OgrenciList = newList;
            return View(model);
        }
      
        }
    }
