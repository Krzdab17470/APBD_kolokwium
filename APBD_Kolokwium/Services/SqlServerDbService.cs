using APBD_Kolokwium.Models;
using APBD_Kolokwium.Requests;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Kolokwium.Services
{
    public class SqlServerDbService : IApplicationDbService
    {
        public PlayerTeam AddPlayerToTeam(AddPlayerToTeamRequest request)
        {
            throw new NotImplementedException();
        }

        public ChampionshipTeam getChampionshipTeams(int idChampionship)
        {
            throw new NotImplementedException();
        }
    }
}
