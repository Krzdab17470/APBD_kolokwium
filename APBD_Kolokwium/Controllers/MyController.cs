using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Kolokwium.Controllers
{
    [ApiController]
    [Route("api/application")]
    
    public class MyController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }


    }
}