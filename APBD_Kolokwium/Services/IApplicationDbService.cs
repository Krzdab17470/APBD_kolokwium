using APBD_Kolokwium.Models;
using APBD_Kolokwium.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Kolokwium.Services
{
    //INTERFACE
    public interface IApplicationDbService
    {
        ChampionshipTeam getChampionshipTeams(int idChampionship);

        PlayerTeam AddPlayerToTeam(AddPlayerToTeamRequest request);


    }
}
