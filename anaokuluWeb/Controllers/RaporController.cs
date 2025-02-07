using anaokuluWeb.Data.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace anaokuluWeb.Controllers
{
    public class RaporController : Controller
    {
        private IConfiguration Configuration;
        private readonly AnaokuluContext _context = new AnaokuluContext();
        public RaporController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        public void ExecuteStoredProcedure()
        {
            //var parameterReturn = new SqlParameter
            //{
            //    ParameterName = "ReturnValue",
            //    //SqlDbType = System.Data.SqlDbType.Int,
            //    Direction = System.Data.ParameterDirection.Output,
            //};
            //var sadsadsa = _context.Database
            //    .ExecuteSqlRaw("EXEC @returnValue = [dbo].[AdminKullanicilarınOgrencileri]", parameterReturn);

            //var returnValue = parameterReturn.Value;


            //var conString = this.Configuration.GetConnectionString("AnaOkuluDatabase");
            //using (SqlConnection conn = new SqlConnection(conString))
            //using (SqlCommand cmd = conn.CreateCommand())
            //{
            //    cmd.CommandText = "AdminKullanicilarınOgrencileri";
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    //cmd.Parameters.AddWithValue("SeqName", "SeqNameValue");

            //    // @ReturnVal could be any name
            //    var returnParameter = cmd.Parameters.Add("@ReturnVal", SqlDbType.Int);
            //    returnParameter.Direction = ParameterDirection.ReturnValue;

            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //    var result = returnParameter.Value;
            //    var rtn = cmd.Parameters["@RETURN_VALUE"].Value;
            //}


            //byte[] pdf = listAll.ToList().ToPdf();
            //return File(pdf, "application/pdf", "ogrenciler.pdf");
        }

        //public void ExecuteStoredProcedure()
        //{
        //    var  conString = this.Configuration.GetConnectionString("AnaOkuluDatabase");
        //    using (var connection = new SqlConnection(conString))
        //    {
        //        using (var command = new SqlCommand("StoredProcedureName", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            // Add parameters if needed
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

    }
}
