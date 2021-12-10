using ApiSfCuim.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ApiSfCuim.Controllers
{
    
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
   
    public class ReferenceController : ControllerBase
    {
        private readonly SigimacContext _context;

        public ReferenceController (SigimacContext context)
        {
            _context = context;
        }
        
        [HttpGet("{reference}")]
        

        public async Task <ActionResult<Reference>> GetReferences (int? reference)
        {
            var rEference = await _context.References.FindAsync(reference);

            if (rEference == null)
            {
                return Ok();
            }

            return Ok(rEference);
        }




    }
}
