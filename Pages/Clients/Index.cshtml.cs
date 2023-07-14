using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Dapper;
using studentInj.DataClass;
using Microsoft.AspNetCore.Components;

namespace studentInj.Pages.Clients
{
	public class IndexModel : PageModel
    {
        public List<Student> ListStudent = new List<Student>();
        private readonly IDatabaseConnection databaseConnection;
        public IndexModel(IDatabaseConnection db)
        {
            databaseConnection = db;
        }
        public async Task OnGet()
        {
            try
            {

                using MySqlConnection conn = databaseConnection.GetConnection();
              
                    String sql = @"SELECT s_personal.stid,clid,first_name,last_name, dob,father_name, s_address, phone, hostel, dept,admission_date
                    FROM s_personal
                    LEFT JOIN s_college
                    ON s_personal.stid=s_college.stid
                    ORDER BY stid;";
                    ListStudent = (List<Student>)await conn.QueryAsync<Student>(sql);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }

    public class Student
    {
        
        public Int32 stid;
        public Int32 clid;
        public String first_name = null!;
        public String last_name = null!;
        public String dob = null!;
        public String father_name = null!;
        public String s_address = null!;
        public String phone = null!;
        public String hostel = null!;
        public String dept = null!;
        public String admission_date = null!;
    }
}
