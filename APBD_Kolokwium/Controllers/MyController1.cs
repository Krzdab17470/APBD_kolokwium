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

    public class MyController1 : ControllerBase
    {
        //3.1:
        /*
                [HttpGet]
                public string Get_Costam()
                {
                    return "Kowal, Mal, Andrzej";
                }*/

        //3.2:
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

        /*        //4.2:
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
                        //ExecuteReader() - w przypadku SELECT..
                        //ExecuteNonQuery() - gdy nie oczekujemy odpowiedzi..
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

                }*/

        
        //ZAD 4.3 + 4.4+ 4.5
         string message = "";
        [HttpGet("{id}")]
        //https://localhost:44362/api/students/1059
        //https://localhost:44362/api/students/1059;DROP%20TABLE%20STUDENT

        public IActionResult GetStudents(string id)
        {


            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17470;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;



               
                com.CommandText = "SELECT * FROM Student WHERE IdEnrollment=@id";
                com.Parameters.AddWithValue("id", id);
                var command = com.CommandText;
                


                con.Open();
                var dr = com.ExecuteReader();



                if (!dr.Read()) //jesli zapytanie NIC nie zwrocilo..
                {
                    return BadRequest("Wrong request.");
                }

                while (dr.Read())
                {
                    var st = new Student1();
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