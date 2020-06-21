using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_Kolokwium.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBD_Kolokwium.Controllers
{
    [Route("api/application3")]
    [ApiController]
    public class MyController3 : ControllerBase
    {

        private IApplicationDbService _service;

        public MyController3(IApplicationDbService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public IActionResult Getstudents(int enrollId)
        {
            return Ok(_service.GetStudents(enrollId));
        }
    }
}