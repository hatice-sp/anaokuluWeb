using anaokuluWeb.Models;
using anaokuluWeb.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArrayToPdf;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;

namespace anaokuluWeb.Controllers
{
    public class OgrenciController : Controller
    {
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public OgrenciController(IWebHostEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }
        private readonly AnaokuluContext _context = new AnaokuluContext();
        public IActionResult Index()
        {
            var listAll = _context.Ogrencis
                .Include(x => x.Sinif)
                .Include(x => x.Veli)
                .Where(x => !x.SilinmisMi.Value)
                      .ToList();

            var model = new OgrenciIndexViewModel();

            List<OgrenciInfo> newList = new List<OgrenciInfo>();

            foreach (var item in listAll)
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

        [HttpPost]
        public IActionResult Index(OgrenciIndexViewModel ogrencis)
        {
            var listAll = _context.Ogrencis
                .Include(x => x.Sinif)
                .Include(x => x.Veli)
                //.Where(x => !x.SilinmisMi.Value && x.Ad.Contains(ogrencis.AdAra) && x.SoyAd.Contains(ogrencis.SoyAdAra))
                .Where(x => !x.SilinmisMi.Value).AsQueryable();
            //  .ToList();

            if (!string.IsNullOrWhiteSpace(ogrencis.AdAra))
            {
                listAll = listAll.Where(x => x.Ad.Contains(ogrencis.AdAra.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(ogrencis.SoyAdAra))
            {
                listAll = listAll.Where(x => x.SoyAd.Contains(ogrencis.SoyAdAra.Trim()));
            }

            var model = new OgrenciIndexViewModel();

            List<OgrenciInfo> newList = new List<OgrenciInfo>();

            foreach (var item in listAll)
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

        public IActionResult Pdf(string ad="", string soyad="")
        {
            var listAll = _context.Ogrencis
                .Include(x => x.Sinif)
                .Include(x => x.Veli)
                //.Where(x => !x.SilinmisMi.Value && x.Ad.Contains(ogrencis.AdAra) && x.SoyAd.Contains(ogrencis.SoyAdAra))
                .Where(x => !x.SilinmisMi.Value).AsQueryable();
            //  .ToList();

            if (!string.IsNullOrWhiteSpace(ad))
            {
                listAll = listAll.Where(x => x.Ad.Contains(ad));
            }

            if (!string.IsNullOrWhiteSpace(soyad))
            {
                listAll = listAll.Where(x => x.SoyAd.Contains(soyad));
            }


            byte[] pdf = listAll.ToList().ToPdf();
            return File(pdf, "application/pdf", "ogrenciler.pdf");
        }

        // GET: Kullanicis/Create
        public IActionResult Create()
        {
            var model = new OgrenciInfo();
            model.Siniflar = new SelectList(GetSinifs(), nameof(SiniflarInfo.Id), nameof(SiniflarInfo.Adi));
            model.Veliler = new SelectList(GetVelis(), nameof(VelilerInfo.Id), nameof(VelilerInfo.Adi));

            return View(model);
        }
        public IEnumerable<SiniflarInfo> GetSinifs()
        {
            // hard coded list for demo. 
            // You may replace with real data from database to create Employee objects
            return _context.Sinifs.Select(x => new SiniflarInfo { Id = x.Id, Adi = x.Adi }).ToList();
        }

        public IEnumerable<VelilerInfo> GetVelis()
        {
            // hard coded list for demo. 
            // You may replace with real data from database to create Employee objects
            return _context.Kullanicis.Select(x => new VelilerInfo { Id = x.Id, Adi = x.KullaniciAdi }).ToList();
        }


        // POST: Kullanicis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OgrenciInfo ogrencis)
        {
            if (ModelState.IsValid)
            {
                var entity = new Ogrenci();
                entity.Id = ogrencis.Id;
                entity.Ad = ogrencis.Ad;
                entity.SoyAd = ogrencis.SoyAd;
                entity.SinifId = ogrencis.SinifId;
                entity.VeliId = ogrencis.VeliId;
                entity.DogumTarihi = ogrencis.DogumTarihi;
                entity.SilinmisMi = false;

                _context.Add(entity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ogrencis);
        }

        // GET: Kullanicis/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var data = await _context.Ogrencis.FindAsync(id);
            if (data == null)
            {
                return NotFound();
            }

            var model = new OgrenciInfo();
            model.Ad = data.Ad;
            model.SoyAd = data.SoyAd;
            model.DogumTarihi = data.DogumTarihi;
            model.Siniflar = new SelectList(GetSinifs(), nameof(SiniflarInfo.Id), nameof(SiniflarInfo.Adi));
            model.Veliler = new SelectList(GetVelis(), nameof(VelilerInfo.Id), nameof(VelilerInfo.Adi));
            model.VeliId = data.VeliId;
            model.SinifId = data.SinifId;
            return View(model);
        }

        // POST: Kullanicis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OgrenciInfo ogrencis)
        {
            if (id != ogrencis.Id && ogrencis.Ad == null)
            {
                return NotFound();
            }
            var updateData = await _context.Ogrencis.FindAsync(id);
            if (ModelState.IsValid)
            {
                try
                {
                    updateData.Ad = ogrencis.Ad;
                    updateData.SoyAd = ogrencis.SoyAd;
                    updateData.DogumTarihi = ogrencis.DogumTarihi;
                    updateData.SinifId = ogrencis.SinifId;
                    updateData.VeliId = ogrencis.VeliId;

                    _context.Update(updateData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OgrencissExists(ogrencis.Id) && OgrencissExists(ogrencis.Ad))
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
            return View(ogrencis);
        }
        // GET: Kullanicis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var data = await _context.Ogrencis.FirstOrDefaultAsync(m => m.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            var model = new OgrenciInfo();
            model.Ad = data.Ad;
            model.SoyAd = data.SoyAd;
            model.DogumTarihi = data.DogumTarihi;

            model.Siniflar = new SelectList(GetSinifs(), nameof(Rol.Id), nameof(Rol.Ad));
            model.Veli = data.Veli;
            model.VeliId = data.VeliId;

            return View(model);
        }

        // POST: Kullanicis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var data = await _context.Ogrencis.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }
            try
            {
                _context.Ogrencis.Remove(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool OgrencissExists(int id)
        {
            return _context.Ogrencis.Any(e => e.Id == id);
        }
        private bool OgrencissExists(string ad)
        {
            return _context.Ogrencis.Any(e => e.Ad == ad);
        }

        public IActionResult Import()
        {
            var model = new OgrenciImportModel();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Import(OgrenciImportModel ogrenciImportModel)
        {
            var model = new OgrenciImportModel();

            if (ogrenciImportModel.file != null)
            {
                //Create a Folder.
                string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Save the uploaded Excel file.
                string fileName = Path.GetFileName(ogrenciImportModel.file.FileName);
                string filePath = Path.Combine(path, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    ogrenciImportModel.file.CopyTo(stream);
                }

                //Read the connection string for the Excel file.
                string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'"; ;
                DataTable dt = new DataTable();
                conString = string.Format(conString, filePath);

                using (OleDbConnection connExcel = new OleDbConnection(conString))
                {
                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                        {
                            cmdExcel.Connection = connExcel;

                            //Get the name of First Sheet.
                            connExcel.Open();
                            DataTable dtExcelSchema;
                            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                            connExcel.Close();

                            //Read Data from First Sheet.
                            connExcel.Open();
                            cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                            odaExcel.SelectCommand = cmdExcel;
                            odaExcel.Fill(dt);
                            connExcel.Close();
                        }
                    }
                }

                //Insert the Data read from the Excel file to Database Table.
                conString = this.Configuration.GetConnectionString("AnaOkuluDatabase");
                using (SqlConnection con = new SqlConnection(conString))
                {
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                    {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.Ogrenci";

                        //[OPTIONAL]: Map the Excel columns with that of the database table.
                        sqlBulkCopy.ColumnMappings.Add("Id", "Id");
                        sqlBulkCopy.ColumnMappings.Add("Ad", "Ad");
                        sqlBulkCopy.ColumnMappings.Add("Soyad", "SoyAd");
                        sqlBulkCopy.ColumnMappings.Add("DogumTarihi", "DogumTarihi");
                        sqlBulkCopy.ColumnMappings.Add("VeliId", "VeliId");
                        sqlBulkCopy.ColumnMappings.Add("SinifId", "SinifId");
                        sqlBulkCopy.ColumnMappings.Add("SilinmisMi", "SilinmisMi");


                        con.Open();
                        sqlBulkCopy.WriteToServer(dt);
                        con.Close();
                    }
                }
            }

            return View(model);
        }



    }
}
