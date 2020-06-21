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
    [Route("api/championships/1/teams")]
    [ApiController]
    public class ChampionshipTeamController : ControllerBase
    {
        string message = "";
        [HttpGet("{id}")]
        public IActionResult GetChampionshipTeams(int idchampionship)
        {

            {

                using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17470;Integrated Security=True"))
                using (var com = new SqlCommand())
                {
                    com.Connection = con;
                    com.CommandText = "SELECT * FROM Championship_Team WHERE IdChampionship=@id";
                    com.Parameters.AddWithValue("id", idchampionship);
                    var command = com.CommandText;

                    con.Open();
                    var dr = com.ExecuteReader();



                    if (!dr.Read()) //jesli zapytanie NIC nie zwrocilo..
                    {
                        return BadRequest("Wrong request.");
                    }

                    while (dr.Read())
                    {
                        var team = new ChampionshipTeam();
                        team.IdChampionship = dr["IdChampionship"].ToString();
                        team.IdTeam = dr["IdTeam"].ToString();
                        team.Score = dr["Score"].ToString();

                        message = string.Concat(message, '\n', team.IdChampionship, ", ", team.IdTeam, ", ", team.Score);

                    }

                    dr.Close();

                }
                return Ok(message);
            }
        }
    }
}