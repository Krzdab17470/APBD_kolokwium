using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Kolokwium.Controllers
{
    [ApiController]
    [Route("api/application")]
    //http://localhost:49234/api/application

    public class MyControllerAll2 : ControllerBase
    {

        string message = "";
        [HttpGet]
        //https://localhost:44362/api/students/1059
        //https://localhost:44362/api/students/1059;DROP%20TABLE%20STUDENT

        public IActionResult GetStudents(string id)
        {


            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17470;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;


             
                com.CommandText = "SELECT * FROM Student";
               
                var command = com.CommandText;



                con.Open();
                var dr = com.ExecuteReader();



                if (!dr.Read()) //jesli zapytanie NIC nie zwrocilo..
                {
                    return BadRequest("Wrong request.");
                }

                while (dr.Read())
                {
                    var st = new Models.Student1();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.IdEnrollment = dr["IdEnrollment"].ToString();
                    message = string.Concat(message, '\n', st.IndexNumber, ", ", st.FirstName, ", ", st.LastName, ", ", st.BirthDate, ", ", st.IdEnrollment);

                }

                dr.Close();

            }
            return Ok(message);
        }
    }
}