using anaokuluWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace anaokuluWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration Configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration _configuration)
        {
            _logger = logger;
            Configuration = _configuration;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult RestoreDatabase(string databaseName, string backupPath)
        {
            //RestoreDatabase("test", @"C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Backup\test.bak");

            backupPath = @"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\Backup\test.bak";

            databaseName = "anaokulu";

            string commandText = $@"USE [master];
                ALTER DATABASE [{databaseName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE [{databaseName}] FROM DISK = N'{backupPath}' WITH FILE = 1, NOUNLOAD, REPLACE, STATS = 5;
                ALTER DATABASE [{databaseName}] SET MULTI_USER;";

            string conString = this.Configuration.GetConnectionString("AnaOkuluDatabase");
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                connection.InfoMessage += Connection_InfoMessage;
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult BackupDatabase(string databaseName = "", string backupPath= "")
        {
            backupPath= @"C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\Backup\test.bak";

            databaseName = "anaokulu";
            string commandText = $@"BACKUP DATABASE [{databaseName}] TO DISK = N'{backupPath}' WITH NOFORMAT, INIT, NAME = N'{databaseName}-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
            string conString = this.Configuration.GetConnectionString("AnaOkuluDatabase");            
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                connection.InfoMessage += Connection_InfoMessage;
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = commandText;
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        private void Connection_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            //Console.WriteLine(e.Message);
        }
    }
}
