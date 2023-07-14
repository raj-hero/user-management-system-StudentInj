using studentInj.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Dapper;

namespace studentInj.Pages.Clients
{
	public class CreateModel : PageModel
    {
        public Student student = new Student();
        public String errorMessage = "";
        public String successMessage = "";
        private readonly IDatabaseConnection databaseConnection;
        public CreateModel(IDatabaseConnection db)
        {
            databaseConnection = db;
        }
        public void OnGet()
        {
        }
        public async Task OnPost()
        {
            try
            {
                using MySqlConnection conn = databaseConnection.GetConnection();
                
                    String sql =@"INSERT INTO s_personal
                                (stid, first_name,last_name, dob,father_name, s_address, phone)VALUES
                                (@stid, @first_name,@last_name, @dob,@father_name, @s_address, @phone);
                                INSERT INTO s_college
                                (clid,stid, hostel, dept,admission_date)VALUES
                                (@clid,@stid, @hostel, @dept,@admission_date);";
                   
                    await conn.ExecuteAsync(sql, new
                    {
                        stid = Request.Form["stid"],
                        clid = Request.Form["clid"],
                        first_name = Request.Form["first_name"],
                        last_name = Request.Form["last_name"],
                        dob = Request.Form["dob"],
                        father_name = Request.Form["father_name"],
                        s_address = Request.Form["s_address"],
                        phone = Request.Form["phone"],
                        hostel = Request.Form["hostel"],
                        dept = Request.Form["dept"],
                        admission_date = Request.Form["admission_date"]
                    });
                
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            student.stid = 0;student.clid = 0;
            student.first_name = ""; student.last_name = ""; student.dob = ""; student.father_name = "";
            student.s_address = ""; student.phone = ""; student.hostel = ""; student.dept = ""; student.admission_date = "";
            successMessage = "NewClient Added Correctly";
            Response.Redirect("/Clients/Index");
        }
    }
}
