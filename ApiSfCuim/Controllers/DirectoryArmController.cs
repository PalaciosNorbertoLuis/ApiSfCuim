using ApiSfCuim.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiSfCuim.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DirectoryArmController : ControllerBase
    {

        [HttpGet("{Arg}")]
        
        public IActionResult Get(string Arg)
        {
            return Ok(DirectoryArm.Search(Arg));
        }





        [HttpPost]
        
        public IActionResult Post(string Arg)
        {
            return Ok(DirectoryArm.Directorios(Arg));
        }

    }
}
