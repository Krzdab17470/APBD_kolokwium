using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD_Kolokwium.Models;
using APBD_Kolokwium.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Kolokwium.Controllers
{
    [Route("api/teams/3/players")]
    [ApiController]
    public class AddPlayerToTeamController : ControllerBase
    {

        public IActionResult GetChampionshipTeams(AddPlayerToTeamRequest request)
        {

            var playerTeam = new PlayerTeam();
            playerTeam.IdPlayer = request.idTeam;
            playerTeam.IdTeam = request.idTeam;
            playerTeam.NumOnShirt = request.numOnShirt;
            playerTeam.Comment = request.comment;

            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s17470;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                var tran = con.BeginTransaction();

                //1. Sprawdzamy czy wiek jest odpowiedni:
                com.CommandText = "SELECT MaxAge FROM Team WHERE IdTeam=@idteam";
                com.Parameters.AddWithValue("idteam", request.idTeam);
                com.Transaction = tran;
                var dr = com.ExecuteReader();

                //1.1 Sprawdzamy czy druzyna istnieje:
                if (!dr.Read()) //jesli zapytanie NIC nie zwrocilo..
                {
                    dr.Close();
                    tran.Rollback(); //wycofujemy transakcje
                    return BadRequest("Drużyna nie istnieje.");  //musimy zwrocic blad
                }

                int maxAge = (int)dr["MaxAge"];
                if (Int32.Parse(request.age) > maxAge)
                {
                    dr.Close();
                    tran.Rollback(); //wycofujemy transakcje
                    return BadRequest("Zawodnik jest za stary do tej drużyny.");  //musimy zwrocic blad
                }
                dr.Close();

                //2. Sprawdzamy czy podany gracz istnieje w bazie
                com.CommandText = "SELECT IdPlayer FROM Player WHERE IdPlayer=@idplayer";
                com.Parameters.AddWithValue("idplayer", request.idPlayer);
                dr = com.ExecuteReader();
                if (!dr.Read()) //jesli zapytanie NIC nie zwrocilo..
                {
                    dr.Close();
                    tran.Rollback(); //wycofujemy transakcje
                    return BadRequest("Gracz nie istnieje.");  //musimy zwrocic blad
                }
                dr.Close();

                //3. Sprawdzamy czy podany gracz jest juz moze przypisany do druzyny (jakiejkolwiek)
                com.CommandText = "SELECT IdPlayer FROM Player_Team WHERE IdPlayer=@idplayer";
                com.Parameters.AddWithValue("idplayer", request.idPlayer);
                dr = com.ExecuteReader();

                if (dr.Read()) //jesli zapytanie COŚ zwrocilo..
                {
                    dr.Close();
                    tran.Rollback(); //wycofujemy transakcje
                    return BadRequest("Gracz już jest przypisany do druzyny!");  //musimy zwrocic blad
                }
                dr.Close();

                //4. Przypisujemy gracza do druzyny
                com.CommandText = "INSERT INTO Player_Team (IdPlayer,IdTeam,NumOnShirt,Comment) VALUES (" + IdEnrollment + ",1," + idStudies + ", '2021-09-10')";
                com.Parameters.AddWithValue("index2", request.IndexNumber);

                com.ExecuteNonQuery();
                tran.Commit();
                dr.Close();
            }
                
            
        }


    }
}