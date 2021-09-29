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
    public class ReferenceArmController : ControllerBase
    {
        [HttpPost]
        //[Route("created")]


        public IActionResult Post(string Arg)
        {
            return Ok(ReferenceArm.Directorios(Arg));
        }

    }
}
