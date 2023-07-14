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
    public class EditModel : PageModel
    {
        public Student student = new Student();
        public String errorMessage = "";
        public String successMessage = "";
        private readonly IDatabaseConnection databaseConnection;
        public EditModel(IDatabaseConnection db)
        {
            databaseConnection = db;
        }
        public async void OnGet()
        {
            //successMessage = "onGet ";
            //String stid = Request.Query["stid"];
            Microsoft.Extensions.Primitives.StringValues stringValues1 = Request.Query["stid"];
            Int32 stid = Convert.ToInt32(stringValues1);
            try
            {
                //successMessage += "inside try ";
                using MySqlConnection conn = databaseConnection.GetConnection();
                //successMessage += "conn opened ";
                    String sql = @"SET FOREIGN_KEY_CHECKS=0;
                                 SELECT s_personal.stid,clid,first_name,last_name, dob,father_name, s_address, phone, hostel, dept,admission_date
                                 FROM s_personal
                                 LEFT JOIN s_college
                                 ON s_college.stid=s_personal.stid WHERE s_personal.stid=@stid;";
                    
                    student = await conn.QuerySingleAsync<Student>(sql, new { stid = stid });
                
            }
            catch (Exception ex)
            {
                errorMessage += ("#1 " + ex.Message + " #1");

            }
        }
        public async void OnPost()
        {
            try
            {
                successMessage += "inside try ";
                using MySqlConnection conn = databaseConnection.GetConnection();
                
                    successMessage += "conn open ";
                    String sql = @"SET FOREIGN_KEY_CHECKS=0;
                                UPDATE s_personal,s_college
                                SET s_personal.first_name=@first_name, s_personal.last_name=@last_name, s_personal.dob=@dob, s_personal.father_name=@father_name, s_personal.s_address=@s_address, s_personal.phone=@phone,
                                s_college.hostel=@hostel, s_college.dept=@dept, s_college.admission_date=@admission_date
                                WHERE s_college.stid=s_personal.stid AND s_personal.stid=@stid;";
                    
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
                errorMessage += (ex.Message + " $2");
                return;
            }
            successMessage += "Client Updated Correctly";
            Response.Redirect("/Clients/Index");
        }
    }
}
