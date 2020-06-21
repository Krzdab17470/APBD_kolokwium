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

                try
                {
                    //1. Sprawdzamy czy wiek jest odpowiedni:
                    com.CommandText = "SELECT MaxAge FROM Team WHERE IdTeam=@idteam";
                    com.Parameters.AddWithValue("idteam", playerTeam.IdTeam);
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
                    com.Parameters.AddWithValue("idplayer", playerTeam.IdPlayer);
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
                    com.Parameters.AddWithValue("idplayer", playerTeam.IdPlayer);
                    dr = com.ExecuteReader();

                    if (dr.Read()) //jesli zapytanie COŚ zwrocilo..
                    {
                        dr.Close();
                        tran.Rollback(); //wycofujemy transakcje
                        return BadRequest("Gracz już jest przypisany do druzyny!");  //musimy zwrocic blad
                    }
                    dr.Close();

                    //4. Przypisujemy gracza do druzyny
                    com.CommandText = "INSERT INTO Player_Team (IdPlayer,IdTeam,NumOnShirt,Comment) VALUES (@idplayer,@idteam,@numonshirt,@comment)";
                    com.Parameters.AddWithValue("idplayer", playerTeam.IdPlayer);
                    com.Parameters.AddWithValue("idteam", playerTeam.IdTeam);
                    com.Parameters.AddWithValue("numonshirt", playerTeam.NumOnShirt);
                    com.Parameters.AddWithValue("comment", playerTeam.Comment);

                    com.ExecuteNonQuery();
                    tran.Commit();
                    dr.Close();
                }catch (SqlException exc)
                {
                    tran.Rollback();
                    return BadRequest("Powazny blad podczas przetwarzania.");
                }

                return Ok("Gracz dodany do druzyny.");
            }
                
            
        }


    }
}