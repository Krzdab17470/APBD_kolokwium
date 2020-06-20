using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD_Kolokwium.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace APBD_Kolokwium.Controllers
{
    [ApiController]
    [Route("api/application")]
    //http://localhost:49234/api/application

    public class MyController : ControllerBase
    {
        /*
        [HttpGet]
        public string Get_Costam()
        {
            return "Kowal, Mal, Andrzej";
        }*/


        /*    
         *    //http://localhost:49234/api/application/2
       [HttpGet("{id}")]
        public IActionResult Get_Costam(int id)
        {
            if (id == 1)
            {
                return Ok("Kowal");
            }
            else
            {
                return NotFound("Nie znaleziono takiego ID.");
            }


        }*/

        string message = "";
        [HttpGet]

        public IActionResult GetStudents(string orderBy)
        {

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17470;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "SELECT * FROM Student";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    var st = new Student();
                    st.IndexNumber = dr["IndexNumber"].ToString();
                    st.FirstName = dr["FirstName"].ToString();
                    st.LastName = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.IdEnrollment = dr["IdEnrollment"].ToString();
                    message = string.Concat(message, '\n', st.IndexNumber, ", ", st.FirstName, ", ", st.LastName, ", ", st.BirthDate, ", ", st.IdEnrollment);
                }
            }
            return Ok(message);

        }
    }
}