using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Dapper;
using studentInj.DataClass;

namespace studentInj.Pages.Clients
{
	public class DeleteModel : PageModel
    {
        private readonly IDatabaseConnection databaseConnection;
        public DeleteModel(IDatabaseConnection db)
        {
            databaseConnection = db;
        }
        public async Task OnGet()
        {
            try
            {
                Microsoft.Extensions.Primitives.StringValues stringValues1 = Request.Query["stid"];
                Int32 stid = Convert.ToInt32(stringValues1);

                using MySqlConnection conn = databaseConnection.GetConnection();
                String sql = @"SET FOREIGN_KEY_CHECKS=0;
                  DELETE s_personal,s_college
                  FROM s_personal
                  LEFT JOIN s_college ON s_college.stid=s_personal.stid
                  WHERE s_personal.stid=@stid;
                  SET FOREIGN_KEY_CHECKS=1;";

                await conn.ExecuteAsync(sql, new { stid = @stid });

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
                return;
            }
            Response.Redirect("/Clients/Index");
        }
    }
}
