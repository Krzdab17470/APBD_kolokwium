using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_Kolokwium.Requests
{
    public class AddPlayerToTeamRequest
    {
        [Required]
        public string idTeam { get; set; }
        [Required]
        public string idPlayer { get; set; }

        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string age { get; set; }

        [Required]
        public string numOnShirt { get; set; }

        [Required]
        public string comment { get; set; }

    }
}
