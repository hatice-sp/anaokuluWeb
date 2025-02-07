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
    public class KullaniciController : Controller
    {
        private readonly AnaokuluContext _context = new AnaokuluContext();

        public IActionResult Index()
        {
            var listAll = _context.Kullanicis
                    .Include(x => x.Rol)
                    .ToList();
            var model = new KullaniciIndexViewModel();

            List<KullaniciInfo> newList = new List<KullaniciInfo>();
            foreach (var item in listAll)
            {
                KullaniciInfo listItem = new KullaniciInfo();
                listItem.Id = item.Id;
                listItem.KullaniciAdi = item.KullaniciAdi;
                listItem.Sifre = item.Sifre;
                listItem.Tckimlik = item.Tckimlik;
                listItem.Telefon = item.Telefon;
                listItem.RolAdi = item.Rol.Ad;
                //add your remaining fields
                newList.Add(listItem);
            }
            model.KullaniciList = newList;
            return View(model);
        }

        // GET: Kullanicis/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var Kullanicis = await _context.Kullanicis
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (Kullanicis == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(Kullanicis);
        //}

        // GET: Kullanicis/Create
        public IActionResult Create()
        {
            var model = new KullaniciInfo();
            model.Roller = new SelectList(GetRoles(), nameof(Rol.Id), nameof(Rol.Ad));

            return View(model);
        }
        public IEnumerable<Rol> GetRoles()
        {
            // hard coded list for demo. 
            // You may replace with real data from database to create Employee objects
            return _context.Rols.ToList();
        }

        // POST: Kullanicis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KullaniciInfo Kullanicis)
        {
            if (ModelState.IsValid)
            {
                var entity = new Kullanici();
                entity.RolId = Kullanicis.RolId;
                entity.KullaniciAdi = Kullanicis.KullaniciAdi;
                entity.Sifre = Kullanicis.Sifre;
                entity.Tckimlik = Kullanicis.Tckimlik;
                entity.Telefon = Kullanicis.Telefon;
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Kullanicis);
        }

        // GET: Kullanicis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           

            var data = await _context.Kullanicis.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            var model = new KullaniciInfo();
            model.KullaniciAdi = data.KullaniciAdi;
            model.Sifre = data.Sifre;
            model.Tckimlik = data.Tckimlik;
            
            model.Roller = new SelectList(GetRoles(), nameof(Rol.Id), nameof(Rol.Ad));
            model.Telefon = data.Telefon;
            model.RolId = data.RolId;

            return View(model);
        }

        // POST: Kullanicis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  KullaniciInfo Kullanicis)
        {
            if (id != Kullanicis.Id && Kullanicis.KullaniciAdi == null)
            {
                return NotFound();
            }

            var updateData = await _context.Kullanicis.FindAsync(id);
            //var updateData = new Kullanici();
            if (ModelState.IsValid)
            {
                try
                {
                    updateData.KullaniciAdi = Kullanicis.KullaniciAdi;
                    updateData.RolId = Kullanicis.RolId;
                    updateData.Telefon = Kullanicis.Telefon;
                    updateData.Sifre = Kullanicis.Sifre;
                    updateData.Tckimlik = Kullanicis.Tckimlik;

                    _context.Update(updateData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KullanicisExists(Kullanicis.Id) && KullanicisExists(Kullanicis.KullaniciAdi))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Kullanicis);
        }



        // GET: Kullanicis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data= await _context.Kullanicis.FirstOrDefaultAsync(m => m.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            var model = new KullaniciInfo();
            model.KullaniciAdi = data.KullaniciAdi;
            model.Sifre = data.Sifre;
            model.Tckimlik = data.Tckimlik;

            model.Roller = new SelectList(GetRoles(), nameof(Rol.Id), nameof(Rol.Ad));
            model.Telefon = data.Telefon;
            model.RolId = data.RolId;

            return View(model);
        }

        // POST: Kullanicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _context.Kullanicis.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            try
            {
                _context.Kullanicis.Remove(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool KullanicisExists(int id)
        {
            return _context.Kullanicis.Any(e => e.Id == id);
        }
        private bool KullanicisExists(string KullaniciAdi)
        {
            return _context.Kullanicis.Any(e => e.KullaniciAdi == KullaniciAdi);
        }
    }
}
